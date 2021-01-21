using System;
using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;

namespace PointOfSale.Domain.Repositories
{
    public class BillRepository : BaseRepository
    {
        public BillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(Bill bill)
        {
            DbContext.Bills.Add(bill);
            SaveChanges();
        }

        public void FinishBill(int billId, decimal cost, DateTime transactionDate)
        {
            var billToFinish = DbContext.Bills.Find(billId);
            billToFinish.Cost = cost;
            billToFinish.TransactionDate = transactionDate;
            
            SaveChanges();
        }

        public bool CheckIsArticleThere(int billId, ArticleBill articleBill)
        {
            var isDuplicate = DbContext.Bills
                .Include(b => b.ArticleBills)
                .First(b => b.Id == billId)
                .ArticleBills.Any(ab => ab.OfferId == articleBill.OfferId);

            if (!isDuplicate) return false;

            var articleBillDb = DbContext.ArticleBills.
                First(ab => ab.BillId == billId && ab.OfferId == articleBill.OfferId);
            articleBillDb.Quantity += articleBill.Quantity;

            var articleDb = DbContext.Offers.Find(articleBill.OfferId);
            articleDb.Quantity -= articleBill.Quantity;

            SaveChanges();
            return true;
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
                                               && b.TransactionDate >= range.start && b.TransactionDate <= range.end).ToList();
        }

        public ICollection<Bill> GetOfferTypeReport(OfferType offerType, (DateTime start, DateTime end) range)
        {
            Func<Bill, bool> dateFilter = b =>
                (b.TransactionDate >= range.start && b.TransactionDate <= range.end);
            Func<Bill, bool> typeFilter = default;

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

