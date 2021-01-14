using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PointOfSale.Domain.Repositories
{
    public class ArticleBillRepository:BaseRepository
    {
        public ArticleBillRepository(PointOfSaleDbContext dbContext):base(dbContext)
        {
        }
    }
}
