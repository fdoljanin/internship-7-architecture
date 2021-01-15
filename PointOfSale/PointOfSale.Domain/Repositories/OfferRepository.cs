using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;

namespace PointOfSale.Domain.Repositories
{
    public class OfferRepository : BaseRepository
    {
        public OfferRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckUnique(string name)
        {
            return DbContext.Offers.Count(o => o.Name.ToLower() == name.ToLower() && o.IsActive == true)==0;
        }

        public Offer FindByName(string name)
        {
            return DbContext.Offers.Where(o => o.Name == name).ToList()[0];
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

        public void Delete(string name)
        {
            var offerToDelete = DbContext.Offers.First(o => o.Name.ToLower() == name.ToLower() && o.IsActive);
            offerToDelete.IsActive = false;
            SaveChanges();
        }

        public ICollection<Offer> GetAll()
        {
            return DbContext.Offers.Where(o=>o.IsActive).ToList();
        }
    }
}