using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Models;

namespace PointOfSale.Domain.Repositories
{
    public class ArticleBillRepository:BaseRepository
    {
        public ArticleBillRepository(PointOfSaleDbContext dbContext):base(dbContext)
        {
        }


        public void Add(ArticleBill articleBill)
        {
            DbContext.ArticleBills.Add(articleBill);
            DbContext.Offers.Find(articleBill.OfferId)
                .Quantity -= articleBill.Quantity;
            
            SaveChanges();
        }

        public ICollection<Offer> GetAllAvailable()
        {
            return DbContext.Offers
                .Where(o => o.Type == OfferType.Item && o.Quantity > 0 && o.IsActive)
                .ToList();
        }

        public bool CheckIsArticleThere(int billId, ArticleBill articleBill)
        {
            var isDuplicate = DbContext.ArticleBills
                .Any(ab => ab.BillId == billId && ab.OfferId == articleBill.OfferId);
            if (!isDuplicate) return false;

            var articleBillDb = DbContext.ArticleBills
                .First(ab => ab.BillId == billId && ab.OfferId == articleBill.OfferId);

            articleBillDb.Quantity += articleBill.Quantity;

            var articleDb = DbContext.Offers.Find(articleBill.OfferId);
            articleDb.Quantity -= articleBill.Quantity;

            SaveChanges();
            return true;
        }

        public ICollection<CountByCategory> GetCountByCategory()
        {
            var offerCategories = DbContext.OfferCategories
                .Where(oc => oc.Offer.Type == OfferType.Item)
                .Include(oc => oc.Category)
                .Include(oc => oc.Offer)
                .ThenInclude(o => o.ArticleBills.Where(ab => !ab.Bill.Cancelled))
                .ToList();

            return offerCategories.GroupBy(oc => oc.Category)
                .Select(g => new CountByCategory()
                {
                    Name = g.Key.Name,
                    Count = g.Sum(oc => oc.Offer.ArticleBills.Sum(ab => ab.Quantity))
                })
                .ToList();
        }

    }
}
