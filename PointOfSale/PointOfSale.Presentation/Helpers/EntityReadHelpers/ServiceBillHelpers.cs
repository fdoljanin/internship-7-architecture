using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class ServiceBillHelpers
    {
        private readonly EmployeeRepository _employeeRepository;

        public ServiceBillHelpers(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        private (DateTime serviceStart, ICollection<Employee> availableEmployees) 
            TryGetTime(int lengthInHours, ref bool doesContinue)
        {
            while (true)
            {
                Console.WriteLine("Enter starting time in format dd.mm.yyyy. hh:mm:");
                var input = ReadHelpers.TryGetInput(ref doesContinue);
                if (!doesContinue) return (default, default);

                var doesParse = DateTime.TryParse(input, out var date);
                if (!doesParse || date.Hour + lengthInHours > 23)
                {
                    Console.WriteLine("Enter valid date! Enter for quit");
                    continue;
                }
                
                var availableEmployees = _employeeRepository.GetAllAvailable(date, lengthInHours);

                if (availableEmployees.Count > 0) return (date, availableEmployees);

                Console.WriteLine("No available employees! Enter for quit");
            }
        }


        public ServiceBill TryGetServiceInfo(ref bool doesContinue)
        {
            Console.WriteLine("Enter service duration in hours:");
            var durationInHours = ReadHelpers.TryIntParse(ref doesContinue, 1, 23);
            if (!doesContinue) return null;

            var doesRestart = false;
            var (startTime, availableEmployees) = TryGetTime(durationInHours, ref doesRestart);
            if (!doesRestart) return TryGetServiceInfo(ref doesContinue);

            PrintHelpers.PrintPersonList(availableEmployees);

            Console.WriteLine("Enter employee index:");
            var employeeIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, availableEmployees.Count) - 1;

            if (!doesContinue) return null;
            var employee = availableEmployees.ElementAt(employeeIndex);

            var serviceBill = new ServiceBill()
            {
                EmployeeId = employee.Id,
                StartTime = startTime,
                Duration = durationInHours
            };

            return serviceBill;
        }
    }
}
