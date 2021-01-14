using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PointOfSale.Domain.Repositories
{
    public class ServiceBillRepository : BaseRepository
    {
        public ServiceBillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }
    }
}