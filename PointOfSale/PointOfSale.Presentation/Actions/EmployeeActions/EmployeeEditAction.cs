using System;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.EmployeeActions
{
    public class EmployeeEditAction : IAction
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly UniqueReadHelpers _uniqueReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit Employee";

        public EmployeeEditAction(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _uniqueReadHelper = new UniqueReadHelpers(employeeRepository);
        }
        public void Call()
        {
            var employeeEdited = new Employee();
            var employeeList = _employeeRepository.GetAll();

            PrintHelpers.PrintPersonList(employeeList);
            var isNotBlank = true;
            Console.WriteLine("Enter index of employee you want to edit:");
            var employeeIndex = ReadHelpers.TryIntParse(ref isNotBlank, 1, employeeList.Count) - 1;
            if (!isNotBlank) return;

            var employeeToEdit = employeeList.ElementAt(employeeIndex);

            Console.WriteLine($"New pin, enter for default ({employeeToEdit.Pin}):");
            var newPin = _uniqueReadHelper.TryGetUniquePin(ref isNotBlank);
            employeeEdited.Pin = isNotBlank ? newPin : employeeToEdit.Pin;

            Console.WriteLine($"First name of the employee, enter for default ({employeeToEdit.FirstName}):");
            isNotBlank = ReadHelpers.DoesContinue(out var newFirstName);
            employeeEdited.FirstName = isNotBlank ? newFirstName : employeeToEdit.FirstName;

            Console.WriteLine($"Last name of the employee, enter for default ({employeeToEdit.LastName}):");
            isNotBlank = ReadHelpers.DoesContinue(out var newLastName);
            employeeEdited.LastName = isNotBlank ? newLastName : employeeToEdit.LastName;

            Console.WriteLine($"Working time of the employee, enter for default ({employeeToEdit.WorkStart} {employeeToEdit.WorkEnd}):");
            var newWorkTime = ReadHelpers.TryGetWorkingHours(0, 24, ref isNotBlank);
            employeeEdited.WorkStart = isNotBlank ? newWorkTime.start : employeeToEdit.WorkStart;
            employeeEdited.WorkEnd = isNotBlank ? newWorkTime.end : employeeToEdit.WorkEnd;

            _employeeRepository.Edit(employeeToEdit.Id, employeeEdited);
        }
    }
}