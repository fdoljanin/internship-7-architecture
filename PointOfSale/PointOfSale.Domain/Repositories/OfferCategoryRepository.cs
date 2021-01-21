using System;
using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;

namespace PointOfSale.Domain.Repositories
{
    public class OfferCategoryRepository : BaseRepository
    {
        public OfferCategoryRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public void Delete(int offerId, int categoryId)
        {
            var offerCategoryToDelete =
                DbContext.OfferCategories.First(oc => oc.OfferId == offerId && oc.CategoryId == categoryId);
            DbContext.OfferCategories.Remove(offerCategoryToDelete);

            SaveChanges();
        }

        public void Add(OfferCategory offerCategory)
        {
            DbContext.OfferCategories.Add(offerCategory);

            SaveChanges();
        }

        public ICollection<Offer> GetOfferList(int categoryId, bool doExistent)
        {
            return DbContext.Offers
                .Where(o => o.IsActive)
                .Where(o => o.OfferCategories
                    .Any(oc => oc.CategoryId == categoryId)
                            == doExistent)
                .ToList();
        }
    }
}