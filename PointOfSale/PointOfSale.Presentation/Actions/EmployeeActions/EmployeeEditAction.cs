using System;
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
            var isNotBlank = true;
            var employeeEdited = new Employee();
            var employeeList = _employeeRepository.GetAll();
            PrintHelpers.PrintPersonList(employeeList);

            Console.WriteLine("Enter index of employee you want to edit:");
            var employeeToEdit = ReadHelpers.TryGetListMember(employeeList, ref isNotBlank);

            Console.WriteLine($"New pin, enter for default ({employeeToEdit.Pin}):");
            var newPin = _uniqueReadHelper.TryGetUniquePin(ref isNotBlank);
            employeeEdited.Pin = isNotBlank ? newPin : employeeToEdit.Pin;

            Console.WriteLine($"First name of the employee, enter for default ({employeeToEdit.FirstName}):");
            var newFirstName = ReadHelpers.TryGetInput(ref isNotBlank);
            employeeEdited.FirstName = isNotBlank ? newFirstName : employeeToEdit.FirstName;

            Console.WriteLine($"Last name of the employee, enter for default ({employeeToEdit.LastName}):");
            var newLastName = ReadHelpers.TryGetInput(ref isNotBlank);
            employeeEdited.LastName = isNotBlank ? newLastName : employeeToEdit.LastName;

            Console.WriteLine($"Working time of the employee, enter for default ({employeeToEdit.WorkStart} {employeeToEdit.WorkEnd}):");
            var newWorkTime = ReadHelpers.TryGetWorkingHours(0, 24, ref isNotBlank);
            employeeEdited.WorkStart = isNotBlank ? newWorkTime.start : employeeToEdit.WorkStart;
            employeeEdited.WorkEnd = isNotBlank ? newWorkTime.end : employeeToEdit.WorkEnd;

            _employeeRepository.Edit(employeeToEdit.Id, employeeEdited);

            Console.WriteLine("Employee edited!");
            Console.ReadLine();
        }
    }
}