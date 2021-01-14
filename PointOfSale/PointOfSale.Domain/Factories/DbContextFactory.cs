using PointOfSale.Data.Entities;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PointOfSale.Domain.Factories
{
    public static class DbContextFactory
    {
        public static PointOfSaleDbContext GetPointOfSaleDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlServer(ConfigurationManager.ConnectionStrings["PointOfSale"].ConnectionString).Options;
            return new PointOfSaleDbContext(options);
        }
    }
}

