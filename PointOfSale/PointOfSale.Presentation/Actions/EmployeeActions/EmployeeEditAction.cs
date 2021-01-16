﻿using System;
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
    public class EmployeeEditAction : IAction
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly PersonReadHelpers _personReadHelper;
        public string Label { get; set; } = "Add Employee";

        public EmployeeEditAction(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _personReadHelper = new PersonReadHelpers(employeeRepository);
        }
        public void Call()
        {
            var employeeEdited = new Employee();
            PrintHelpers.PrintPersonList(_employeeRepository.GetAll());

            var isNotBlank = true;
            Console.WriteLine("Enter pin of employee you want to edit:");
            var pin = _personReadHelper.TryGetPin(true, ref isNotBlank);
            if (!isNotBlank) return;

            var employeeToEdit = _employeeRepository.GetByPin(pin);

            Console.WriteLine($"New pin, enter for default ({employeeToEdit.Pin}):");
            var newPin = _personReadHelper.TryGetPin(false, ref isNotBlank);
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