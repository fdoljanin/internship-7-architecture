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