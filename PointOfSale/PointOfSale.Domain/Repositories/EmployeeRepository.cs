using System;
using System.Collections.Generic;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories.Abstractions;

namespace PointOfSale.Domain.Repositories
{
    public class EmployeeRepository : BaseRepository, IUniqueString
    {
        public EmployeeRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public bool IsStringUnique(string pin)
        {
            return !DbContext.Employees.Any(e => e.Pin == pin && !e.isRemoved);
        }

        public void Add(Employee employee)
        {
            DbContext.Employees.Add(employee);

            SaveChanges();
        }

        public void Edit(int id, Employee editedEmployee)
        {
            var employeeDb = DbContext.Employees.First(e => e.Id == id);
            employeeDb.Pin = editedEmployee.Pin;
            employeeDb.FirstName = editedEmployee.FirstName;
            employeeDb.LastName = editedEmployee.LastName;
            employeeDb.WorkStart = editedEmployee.WorkStart;
            employeeDb.WorkEnd = editedEmployee.WorkEnd;

            SaveChanges();
        }

        public ICollection<Employee> GetAll()
        {
            return DbContext.Employees.Where(e => !e.isRemoved).ToList();
        }


        public ICollection<Employee> GetAllAvailable(DateTime start, int duration)
        {
            var end = start.AddHours(duration);
            return DbContext.Employees
                .Include(e => e.ServiceBills)
                .Where(e => !e.isRemoved)
                .Where(e => e.WorkStart <= start.Hour && e.WorkEnd * 60 >= end.Hour*60 + end.Minute)
                .Where(e => e.ServiceBills.All(sb => sb.StartTime > end || 
                                                     start > sb.StartTime.AddHours(sb.Duration)))
                .ToList();
        }

        public void Delete(int employeeId)
        {
            var employeeDb = DbContext.Employees.Find(employeeId);
            employeeDb.isRemoved = true;
            
            SaveChanges();
        }
    }
}