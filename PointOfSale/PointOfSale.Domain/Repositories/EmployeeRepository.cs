using System.Collections.Generic;
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

            SaveChanges();
        }

        public void Edit(int id, Employee editedEmployee)
        {
            var employeeToEdit = DbContext.Employees.First(e => e.Id == id);
            employeeToEdit.Pin = editedEmployee.Pin;
            employeeToEdit.FirstName = editedEmployee.FirstName;
            employeeToEdit.LastName = editedEmployee.LastName;
            employeeToEdit.WorkStart = editedEmployee.WorkStart;
            employeeToEdit.WorkEnd = editedEmployee.WorkEnd;

            SaveChanges();
        }

        public ICollection<Employee> GetAll()
        {
            return DbContext.Employees.ToList();
        }

        public Employee GetByPin(string pin)
        {
            return DbContext.Employees.First(e => e.Pin == pin);
        }
    }
}