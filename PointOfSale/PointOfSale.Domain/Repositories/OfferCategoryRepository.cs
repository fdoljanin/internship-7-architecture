using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PointOfSale.Domain.Repositories
{
    public class OfferCategoryRepository : BaseRepository
    {
        public OfferCategoryRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }
    }
}