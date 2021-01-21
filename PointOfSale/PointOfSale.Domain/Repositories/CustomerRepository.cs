using System.Collections.Generic;
using PointOfSale.Data.Entities;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories.Abstractions;

namespace PointOfSale.Domain.Repositories
{
    public class CustomerRepository : BaseRepository, IUniqueString
    {
        public CustomerRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public bool IsStringUnique(string pin)
        {
            return !DbContext.Customers.Any(c => c.Pin == pin);
        }

        public void Add(Customer customer)
        {
            DbContext.Customers.Add(customer);
            SaveChanges();
        }

        public void Edit(int id, Customer editedCustomer)
        {
            var customerDb = DbContext.Customers.First(c => c.Id == id);
            customerDb.Pin = editedCustomer.Pin;
            customerDb.FirstName = editedCustomer.FirstName;
            customerDb.LastName = editedCustomer.LastName;
            SaveChanges();
        }

        public ICollection<Customer> GetAll()
        {
            return DbContext.Customers.ToList();
        }

    }
}