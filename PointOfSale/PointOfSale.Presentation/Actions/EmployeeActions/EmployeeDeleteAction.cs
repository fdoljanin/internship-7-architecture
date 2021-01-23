using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.EmployeeActions
{
    public class EmployeeDeleteAction:IAction
    {
        private readonly EmployeeRepository _employeeRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete employee";

        public EmployeeDeleteAction(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void Call()
        {
            var isNotBlank = true;
            var employeeList = _employeeRepository.GetAll();
            PrintHelpers.PrintPersonList(employeeList);
            if (employeeList.Count == 0) return;

            Console.WriteLine("Enter index of employee you want to delete:");
            var employeeToDelete = ReadHelpers.TryGetListMember(employeeList, ref isNotBlank);
            if (!isNotBlank) return;

            if (!ReadHelpers.Confirm($"Do you want to delete employee {employeeToDelete.FirstName}? (yes/no)"))
            {
                MessageHelpers.Success("Action cancelled.");
                Console.ReadLine();
                return;
            }

            _employeeRepository.Delete(employeeToDelete.Id);
            MessageHelpers.Success("Employee deleted!");
            Console.ReadLine();
        }
    }
}
