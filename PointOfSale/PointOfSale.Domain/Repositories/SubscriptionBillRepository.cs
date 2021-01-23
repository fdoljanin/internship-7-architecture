using System;
using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;

namespace PointOfSale.Domain.Repositories
{
    public class SubscriptionBillRepository:BaseRepository
    {
        public SubscriptionBillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public ICollection<Offer> GetAllAvailable()
        {
            return DbContext.Offers
                .Where(o => o.Type == OfferType.Rent && o.Quantity > 0 && o.IsActive).ToList();
        }

        public void AddSubscription(SubscriptionBill subscriptionBill)
        {
            DbContext.SubscriptionBills.Add(subscriptionBill);
            --subscriptionBill.Offer.Quantity;

            SaveChanges();
        }

        public ICollection<SubscriptionBill> GetActiveSubscriptions()
        {
            return DbContext.SubscriptionBills
                .Include(sb => sb.Offer)
                .Include(sb => sb.Customer)
                .Where(sb => sb.BillId == null)
                .ToList();
        }

        public void CancelSubscription(int subscriptionId)
        {
            var subscriptionToCancel = DbContext.SubscriptionBills
                .Include(sb => sb.Offer)
                .First(sb => sb.Id == subscriptionId);
            
            subscriptionToCancel.Offer.Quantity++;
            DbContext.SubscriptionBills.Remove(subscriptionToCancel);
            
            SaveChanges();
        }
    }
}