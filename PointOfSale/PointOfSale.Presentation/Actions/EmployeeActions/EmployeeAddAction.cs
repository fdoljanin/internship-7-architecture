using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly PersonReadHelpers _personReadHelper;
        public string Label { get; set; } = "Add Employee";

        public EmployeeAddAction(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _personReadHelper = new PersonReadHelpers(employeeRepository);
        }
        public void Call()
        {
            var doesContinue = true;
            Console.WriteLine("Enter employee pin:");
            var pin = _personReadHelper.TryGetPin(false, ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter first name of the employee:");
            doesContinue = ReadHelpers.DoesContinue(out var firstName);
            if (!doesContinue) return;

            Console.WriteLine("Enter last name of the employee:");
            doesContinue = ReadHelpers.DoesContinue(out var lastName);
            if (!doesContinue) return;

            Console.WriteLine("Enter start (inclusive) and end (exclusive) of 24h work day in format hh hh:");
            var workTime = ReadHelpers.GetWorkingHours(0, 24, ref doesContinue);
            if (!doesContinue) return;


            _employeeRepository.Add(
                new Employee()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Pin = pin,
                    WorkStart = workTime.start,
                    WorkEnd = workTime.end
                });
        }
    }
}
