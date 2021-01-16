using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories.Abstractions;

namespace PointOfSale.Domain.Repositories
{
    public class EmployeeRepository : BaseRepository, IPersonRepository
    {
        public EmployeeRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public bool DoesPinExist(string pin)
        {
            return DbContext.Employees.Any(c => c.Pin == pin);
        }

        public void Add(Employee employee)
        {
            DbContext.Employees.Add(employee);
        }
    }
}