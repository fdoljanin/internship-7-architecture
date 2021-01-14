using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PointOfSale.Domain.Repositories
{
    public class SubscriptionBillRepository : BaseRepository
    {
        public SubscriptionBillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }
    }
}