using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
    }
}