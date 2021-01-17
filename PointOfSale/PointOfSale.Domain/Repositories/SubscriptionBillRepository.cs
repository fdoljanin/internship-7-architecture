using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
            return DbContext.Offers.Where(o => o.Type == OfferType.Rent && o.Quantity > 0 && o.IsActive).ToList();
        }

        public  Offer FindByName(string name)
        {
            return DbContext.Offers.First(o => o.Type == OfferType.Rent && o.IsActive);
        }

        public bool CheckUnique(string name)
        {
            return !DbContext.Offers.Any(o => o.Type == OfferType.Rent && o.Name.ToLower() == name.ToLower() && o.IsActive);
        }

        public void AddSubscription(SubscriptionBill subscriptionBill)
        {
            DbContext.SubscriptionBills.Add(subscriptionBill);
            --subscriptionBill.Offer.Quantity;
            SaveChanges();
        }
    }
}