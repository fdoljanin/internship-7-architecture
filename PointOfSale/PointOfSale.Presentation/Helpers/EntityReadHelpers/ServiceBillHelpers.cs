using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class ServiceBillHelpers
    {
        private readonly ServiceBillRepository _serviceBillRepository;
        private readonly EmployeeRepository _employeeRepository;

        public ServiceBillHelpers(ServiceBillRepository serviceBillRepository, EmployeeRepository employeeRepository)
        {
            _serviceBillRepository = serviceBillRepository;
            _employeeRepository = employeeRepository;
        }

        private string TryGetServiceName(ref bool doesContinue)
        {
            while (true)
            {
                doesContinue = ReadHelpers.DoesContinue(out var serviceName);
                if (!doesContinue) return null;
                if (_serviceBillRepository.DoesExist(serviceName)) return serviceName;
                Console.WriteLine("Service does not exist!");
            }
        }

        private (DateTime serviceStart, ICollection<Employee> availableEmployees) 
            TryGetTime(int lengthInHours, ref bool doesContinue)
        {
            while (true)
            {
                Console.WriteLine("Enter starting time in format dd.mm.yyyy. hh:mm:");
                doesContinue = ReadHelpers.DoesContinue(out var input);
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

        private string GetEmployeePin(ICollection<Employee> employees)
        {
            while (true)
            {
                var pin = Console.ReadLine().Trim();
                if (employees.Any(e => e.Pin == pin)) return pin;
                Console.WriteLine("PIN not valid!");
            }
        }

        public ServiceBill TryGetService(ref bool doesContinue)
        {
            Console.WriteLine("Enter service name:");
            var serviceName = TryGetServiceName(ref doesContinue);
            if (!doesContinue) return null;

            Console.WriteLine("Enter service duration in hours:");
            var durationInHours = ReadHelpers.TryIntParse(ref doesContinue, 1, 23);
            if (!doesContinue) return null;

            var doesRestart = false;
            var (startTime, availableEmployees) = TryGetTime(durationInHours, ref doesRestart);
            if (!doesRestart) return TryGetService(ref doesContinue);

            PrintHelpers.PrintPersonList(availableEmployees);

            Console.WriteLine("Enter employee PIN:");
            var employeePin = GetEmployeePin(availableEmployees);

            var serviceBill = new ServiceBill()
            {
                OfferId = _serviceBillRepository.FindByName(serviceName).Id,
                EmployeeId = _employeeRepository.GetByPin(employeePin).Id,
                StartTime = startTime,
                Duration = durationInHours
            };

            return serviceBill;
        }
    }
}
