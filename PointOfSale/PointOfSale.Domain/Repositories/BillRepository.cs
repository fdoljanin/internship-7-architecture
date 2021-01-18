using System;
using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;

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

            var originalArticle = DbContext.ArticleBills.
                First(ab => ab.BillId == billId && ab.OfferId == articleBill.OfferId);
            originalArticle.Quantity += articleBill.Quantity;
            
            SaveChanges();
            return true;
        }

        public decimal GetSubscriptionBill(string customerPin)
        {
            var customer = DbContext.Customers
                .Include(c => c.SubscriptionBills
                    .Where(sb => sb.BillId == null))
                .ThenInclude(sb => sb.Offer)
                .First(c => c.Pin == customerPin);
            var bill = new Bill();
            DbContext.Bills.Add(bill);

            SaveChanges();

            bill.Cost = 0;
            bill.TransactionDate = DateTime.Now;

            foreach (var subscription in customer.SubscriptionBills)
            {
                subscription.BillId = bill.Id;
                subscription.Offer.Quantity++;

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
    }
}

