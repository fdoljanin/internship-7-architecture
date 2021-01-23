using System.Collections.Generic;
using PointOfSale.Data.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
            return !DbContext.Customers.Any(c => c.Pin == pin && !c.isRemoved);
        }

        public void Add(Customer customer)
        {
            DbContext.Customers.Add(customer);
            SaveChanges();
        }

        public void Edit(int id, Customer editedCustomer)
        {
            var customerDb = DbContext.Customers.First(c => c.Id == id && !c.isRemoved);
            customerDb.Pin = editedCustomer.Pin;
            customerDb.FirstName = editedCustomer.FirstName;
            customerDb.LastName = editedCustomer.LastName;
            SaveChanges();
        }

        public ICollection<Customer> GetAll()
        {
            return DbContext.Customers.Where(c => !c.isRemoved).ToList();
        }

        public bool CheckIsDeletable(int customerId)
        {
            var customerDb = DbContext.Customers
                .Include(c => c.SubscriptionBills)
                .First(c => c.Id == customerId);

            return customerDb.SubscriptionBills.All(sb => sb.BillId != null);
        }
        public void Delete(int customerId)
        {
            var customerDb = DbContext.Customers.Find(customerId);
            customerDb.isRemoved = true;

            SaveChanges();
        }

    }
}