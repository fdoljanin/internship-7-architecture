using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PointOfSale.Domain.Repositories
{
    public class CategoryRepository : BaseRepository
    {
        public CategoryRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }
    }
}