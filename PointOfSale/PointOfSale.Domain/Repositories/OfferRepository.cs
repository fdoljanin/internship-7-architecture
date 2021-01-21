using System;
using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Permissions;
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

        public void ChangeQuantity(int offerId, int newQuantity)
        {
            var offerToEdit = DbContext.Offers.Find(offerId);
            offerToEdit.Quantity = newQuantity;

            SaveChanges();
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
            var offers = new List<TopSellingOffer>();

            offers.AddRange( //implement interface, and query as a var 
                DbContext.ArticleBills
                    .Include(ab => ab.Offer)
                    .Where(ab => !ab.Bill.Cancelled)
                    .ToList()
                    .GroupBy(ab => ab.Offer)
                    .Select(g => new TopSellingOffer(){
                        Offer = g.Key,
                        Quantity = g.Sum(ab => ab.Quantity)
                    })
                    .ToList());

            offers.AddRange(
                DbContext.ServiceBills
                    .Include(ab => ab.Offer)
                    .Where(sb => !sb.Bill.Cancelled)
                    .ToList()
                    .GroupBy(sb => sb.Offer)
                    .Select(g => new TopSellingOffer(){
                        Offer = g.Key,
                        Quantity = g.Count()
                    })
                    .ToList());

            offers.AddRange(
                DbContext.SubscriptionBills
                    .Include(ab => ab.Offer)
                    .Where(sb => sb.Bill == null || !sb.Bill.Cancelled)
                    .ToList()
                    .GroupBy(sb => sb.Offer)
                    .Select(g => new TopSellingOffer(){
                       Offer = g.Key,
                       Quantity = g.Count()
                    })
                    .ToList());

            offers = offers.Where(o => o.Offer.IsActive).ToList();

            offers.Sort((x,y) => y.Quantity.CompareTo(x.Quantity));

            return offers.Take(take).ToList();
        }
    }
}