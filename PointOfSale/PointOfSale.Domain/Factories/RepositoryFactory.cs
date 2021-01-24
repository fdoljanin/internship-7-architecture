using System;
using PointOfSale.Data.Entities;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Domain.Factories
{
    public static class RepositoryFactory
    {
        private static PointOfSaleDbContext _context { get; }

        static RepositoryFactory()
        {
            _context = DbContextFactory.GetPointOfSaleDbContext();
        }

        public static TRepository GetRepository<TRepository>() where TRepository : BaseRepository
        {
            return (TRepository)Activator.CreateInstance(typeof(TRepository), _context);
        }
    }
}