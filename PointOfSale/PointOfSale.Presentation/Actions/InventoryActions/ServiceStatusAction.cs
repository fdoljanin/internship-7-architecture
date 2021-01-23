using System;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.InventoryActions
{
    public class ServiceStatusAction:IAction
    {
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Service status";
        private readonly EmployeeRepository _employeeRepository;
        public ServiceStatusAction(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Call()
        {
            var doesContinue = true;
            while (doesContinue)
            {
                Console.WriteLine("Enter service duration in hours:");
                var duration = ReadHelpers.TryIntParse(ref doesContinue, 1, 23);
                if (!doesContinue) return;

                var input = ReadHelpers.TryGetInput(ref doesContinue);
                var doesParse = DateTime.TryParse(input, out var date);
                if (!doesParse || date.Hour + duration > 23)
                {
                    MessageHelpers.Error("Enter valid datetime!");
                    continue;
                }
                if (!doesContinue) return;

                var availableEmployees = _employeeRepository.GetAllAvailable(date, duration);
                Console.WriteLine("Available employees:");
                PrintHelpers.PrintPersonList(availableEmployees);

                Console.ReadLine();
            }
        }
    }
}
