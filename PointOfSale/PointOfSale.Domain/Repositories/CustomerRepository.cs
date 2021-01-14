using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PointOfSale.Domain.Repositories
{
    public class CustomerRepository : BaseRepository
    {
        public CustomerRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }
    }
}