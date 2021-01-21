using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Models;
using PointOfSale.Domain.Repositories.Abstractions;

namespace PointOfSale.Domain.Repositories
{
    public class OfferRepository : BaseRepository, IUniqueString
    {
        public OfferRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public bool IsStringUnique(string name)
        {
            return !DbContext.Offers.Any(o => o.Name.ToLower() == name.ToLower() && o.IsActive);
        }



        public void Add(Offer offer)
        {
            DbContext.Offers.Add(offer);
            SaveChanges();
        }

        public void Edit(int id, Offer editedOffer)
        {
            var offerDb = DbContext.Offers.Find(id);
            offerDb.Name = editedOffer.Name;
            offerDb.Price = editedOffer.Price;
            offerDb.Quantity = editedOffer.Quantity;
            SaveChanges();
        }

        public void Delete(int offerId)
        {
            var offerToDelete = DbContext.Offers.Find(offerId);
            offerToDelete.IsActive = false;
            SaveChanges();
        }


        public ICollection<Offer> GetAll()
        {
            return DbContext.Offers.Where(o=>o.IsActive).ToList(); //asnotracking
        }

        public ICollection<Offer> GetArticlesLessOrMore((int lowerBound, int upperBound) range)
        {
            return DbContext.Offers
                .Where(o => o.Type == OfferType.Item && o.IsActive)
                .Where(o => o.Quantity < range.upperBound && o.Quantity > range.lowerBound)
                .ToList();
        }


        public ICollection<TopSellingOffer> GetTopSell(int take)
        {
            var offersDb = DbContext.Offers.Where(o => o.IsActive);
            var offers = new List<TopSellingOffer>();

            var articles = offersDb.Where( o => o.Type == OfferType.Item)
                .Include(o => o.ArticleBills.Where(ab => !ab.Bill.Cancelled))
                .ToList()
                .Select(o => new TopSellingOffer
                {
                    Offer = o,
                    Quantity = o.ArticleBills.Sum(ab => ab.Quantity)
                });

            var services = offersDb.Where(o => o.Type == OfferType.Service)
                .Include(o => o.ServiceBills.Where(sb => !sb.Bill.Cancelled))
                .ToList()
                .Select(o => new TopSellingOffer
                {
                    Offer = o,
                    Quantity = o.ServiceBills.Count
                });

            var subscriptions = offersDb.Where(o => o.Type == OfferType.Rent)
                .Include(o => o.SubscriptionBills.Where(sb => !sb.Bill.Cancelled))
                .ToList()
                .Select(o => new TopSellingOffer
                {
                    Offer = o,
                    Quantity = o.SubscriptionBills.Count
                });

            offers.AddRange(articles);
            offers.AddRange(services);
            offers.AddRange(subscriptions);

            offers.Sort((x,y) => y.Quantity.CompareTo(x.Quantity));

            return offers.Take(take).ToList();
        }
    }
}