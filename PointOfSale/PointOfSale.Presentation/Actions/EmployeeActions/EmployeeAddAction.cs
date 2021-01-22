using System;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.EmployeeActions
{
    public class EmployeeAddAction : IAction
    {
        private readonly EmployeeRepository _employeeRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add Employee";

        public EmployeeAddAction(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void Call()
        {
            var doesContinue = true;
            var employee = new Employee();
;
            Console.WriteLine("Enter employee pin:");
            employee.Pin = UniqueReadHelpers.TryGetUniquePin(_employeeRepository, ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter first name of the employee:");
            employee.FirstName = ReadHelpers.TryGetInput(ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter last name of the employee:");
            employee.LastName = ReadHelpers.TryGetInput(ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter start (inclusive) and end (exclusive) of 24h work day in format hh hh:");
            (employee.WorkStart, employee.WorkEnd) = ReadHelpers.TryGetWorkingHours(0, 24, ref doesContinue);
            if (!doesContinue) return;

            _employeeRepository.Add(employee);

            Console.WriteLine("Employee added!");
            Console.ReadLine();
        }
    }
}
