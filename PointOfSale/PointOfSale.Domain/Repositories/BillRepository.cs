using System;
using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;

namespace PointOfSale.Domain.Repositories
{
    public class BillRepository : BaseRepository
    {
        public BillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public Bill GetNewBill(BillType billType)
        {
            var newBill = new Bill();
            DbContext.Bills.Add(newBill);
            newBill.Type = billType;
            SaveChanges();
            return newBill;
        }

        public decimal FinishBillAndGetCost(int billId)
        {
            var billDb = DbContext.Bills
                .Include(b => b.ArticleBills)
                .ThenInclude(ab => ab.Offer)
                .Include(b => b.ServiceBills)
                .ThenInclude(sb => sb.Offer)
                .First(b => b.Id == billId);

            foreach (var articleBill in billDb.ArticleBills)
            {
                billDb.Cost += articleBill.Quantity * articleBill.Offer.Price;
            }

            foreach (var serviceBill in billDb.ServiceBills)
            {
                billDb.Cost += serviceBill.Duration * serviceBill.Offer.Price;
            }

            billDb.TransactionDate = DateTime.Now;

            if (billDb.Cost == 0)
            {
                DbContext.Bills.Remove(billDb);
            } 

            SaveChanges();

            return billDb.Cost;
        }

        public decimal GetSubscriptionBill(int customerId)
        {
            var customer = DbContext.Customers
                .Include(c => c.SubscriptionBills
                    .Where(sb => sb.BillId == null))
                .ThenInclude(sb => sb.Offer)
                .First(c => c.Id == customerId);
            var bill = new Bill();
            DbContext.Bills.Add(bill);

            SaveChanges();

            bill.Cost = 0;
            bill.TransactionDate = DateTime.Now;

            foreach (var subscription in customer.SubscriptionBills)
            {
                subscription.BillId = bill.Id;
                ++subscription.Offer.Quantity;

                bill.Cost += subscription.Offer.Price *
                            ((bill.TransactionDate.Year - subscription.StartTime.Year) * 12 +
                                bill.TransactionDate.Month - subscription.StartTime.Month + 1);
            }

            if (bill.Cost == 0)
            {
                DbContext.Bills.Remove(bill);
            }

            SaveChanges();
            return bill.Cost;
        }

        public ICollection<Bill> GetBills()
        {
            return DbContext.Bills.Where(b => !b.Cancelled).ToList();
        }

        public void CancelBill(int billId)
        {
            DbContext.Bills.Find(billId).Cancelled = true;
            
            SaveChanges();
        }

        public ICollection<Bill> GetCategoryReport(BillType billType, (DateTime start, DateTime end) range)
        {
            return DbContext.Bills
                .Where(b => b.Type == billType && !b.Cancelled 
                                               && b.TransactionDate >= range.start && b.TransactionDate <= range.end)
                .ToList();
        }

        public ICollection<Bill> GetOfferTypeReport(OfferType offerType, (DateTime start, DateTime end) range)
        {
            Expression<Func<Bill, bool>> dateFilter = b =>
                (b.TransactionDate >= range.start && b.TransactionDate <= range.end);
            Expression<Func<Bill, bool>> typeFilter = default;

            switch (offerType)
            {
                case OfferType.Service:
                    typeFilter = b => b.ServiceBills.Count > 0;
                    break;
                case OfferType.Item:
                    typeFilter = b => b.ArticleBills.Count > 0;
                    break;
                case OfferType.Rent:
                    typeFilter = b => b.SubscriptionBills.Count > 0;
                    break;
            }

            return DbContext.Bills
                .Include(b => b.ArticleBills)
                .Include(b => b.ServiceBills)
                .Include(b => b.SubscriptionBills)
                .Where(dateFilter)
                .Where(typeFilter)
                .Where(b => !b.Cancelled)
                .ToList();
        }

        public decimal GetYearProfit(int year)
        {
            return DbContext.Bills
                .Where(b => b.TransactionDate.Year == year && !b.Cancelled)
                .ToList()
                .Sum(b => b.Cost);
        }
    }
}

