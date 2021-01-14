using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PointOfSale.Domain.Repositories
{
    public class BillRepository : BaseRepository
    {
        public BillRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }
    }
}

