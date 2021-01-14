using PointOfSale.Data.Entities;

namespace PointOfSale.Domain.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly PointOfSaleDbContext DbContext;

        protected BaseRepository(PointOfSaleDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected void SaveChanges()
        {
            DbContext.SaveChanges();
        }
    }
}
