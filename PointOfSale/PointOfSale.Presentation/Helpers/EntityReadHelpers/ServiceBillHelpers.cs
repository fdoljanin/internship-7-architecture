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
        public string TryGetService(ref bool doesContinue)
        {
            string serviceName;
            while (true)
            {
                Console.WriteLine("Enter service name:");
                doesContinue = ReadHelpers.DoesContinue(out var serviceNameOut);
                if (!doesContinue) return null;
                if (!_serviceBillRepository.DoesExist(serviceNameOut))
                {
                    Console.WriteLine("Service does not exist!");
                    continue;
                }

                serviceName = serviceNameOut;
                break;
            }

            Console.WriteLine("Enter service duration in hours");
            var lengthInHours = ReadHelpers.TryIntParse(ref doesContinue, 1);
            if (!doesContinue) return null;

            ICollection<Employee> availableEmployees;
            while (true)
            {
                Console.WriteLine("Enter starting time:");
                var doesParse = DateTime.TryParse(Console.ReadLine(), out var date);
                if (!doesParse)
                {
                    Console.WriteLine("Enter valid date!");
                    continue;
                }

                availableEmployees = _employeeRepository.GetAllAvailable(date, lengthInHours);

                if (availableEmployees.Count == 0)
                {
                    Console.WriteLine("No available employees!");
                    continue;
                }

                break;
            }

            PrintHelpers.PrintPersonList(availableEmployees);

            Console.WriteLine("Enter employee PIN:");
            string pin;
            while (true)
            {
                pin = Console.ReadLine().Trim();
                if (availableEmployees.Any(e => e.Pin == pin)) break;
                Console.WriteLine("PIN not valid!");
            }

            return "";
        }
    }
}
