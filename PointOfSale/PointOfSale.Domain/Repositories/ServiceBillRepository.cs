using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;

namespace PointOfSale.Domain.Repositories
{
    public class ServiceBillRepository : BaseRepository
    {
        public ServiceBillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public bool DoesExist(string name)
        {
            return DbContext.Offers.Any(o =>
                o.Type == OfferType.Service && o.Name.ToLower() == name.ToLower() && o.IsActive);
        }

        public Offer FindByName(string name)
        {
            return DbContext.Offers.First(o => o.Name == name && o.IsActive);
        }

        public ICollection<Offer> GetAll()
        {
            return DbContext.Offers.Where(o => o.Type == OfferType.Service && o.IsActive).ToList();
        }

        public void Add(ServiceBill serviceBill)
        {
            DbContext.ServiceBills.Add(serviceBill);
            SaveChanges();
        }
        public decimal GetPrice(int id)
        {
            return DbContext.Offers.Find(id).Price;
        }
    }
}