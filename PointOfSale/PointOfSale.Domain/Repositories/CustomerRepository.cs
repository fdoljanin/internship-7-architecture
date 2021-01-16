using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories.Abstractions;

namespace PointOfSale.Domain.Repositories
{
    public class CustomerRepository : BaseRepository, IPersonRepository
    {
        public CustomerRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public bool DoesPinExist(string pin)
        {
            return DbContext.Customers.Any(c => c.Pin == pin);
        }

        public void Add(Customer customer)
        {
            DbContext.Customers.Add(customer);
        }
    }
}