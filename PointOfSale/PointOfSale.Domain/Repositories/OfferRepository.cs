using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PointOfSale.Domain.Repositories
{
    public class OfferRepository : BaseRepository
    {
        public OfferRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }
    }
}