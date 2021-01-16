using System;
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
    }
}