﻿using System;
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

        public ICollection<Employee> GetAllAvailable(DateTime start, int length)
        {
            var end = start.AddHours(length);
            return DbContext.Employees.Include(e => e.ServiceBills)
                .Where(e => e.WorkStart <= start.Hour && e.WorkEnd * 60 >= end.Hour*60 + end.Minute)
                .Where(e => e.ServiceBills.All(sb => sb.StartTime > end || 
                                                     start > sb.StartTime.AddHours(sb.Duration)))
                .ToList();
        }
    }
}