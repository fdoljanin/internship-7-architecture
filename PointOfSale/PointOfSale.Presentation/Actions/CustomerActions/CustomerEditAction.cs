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

namespace PointOfSale.Presentation.Actions.CustomerActions
{
    public class CustomerEditAction : IAction
    {
        private readonly CustomerRepository _customerRepository;
        private readonly PersonReadHelpers _personReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit Customer";

        public CustomerEditAction(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _personReadHelper = new PersonReadHelpers(customerRepository);
        }
        public void Call()
        {
            var customerEdited = new Customer();
            PrintHelpers.PrintPersonList(_customerRepository.GetAll());
            var isNotBlank = true;
            Console.WriteLine("Enter pin of customer you want to edit:");
            var pin = _personReadHelper.TryGetPin(true, ref isNotBlank);
            if (!isNotBlank) return;

            var customerToEdit = _customerRepository.GetByPin(pin);

            Console.WriteLine($"New pin, enter for default ({customerToEdit.Pin}):");
            var newPin = _personReadHelper.TryGetPin(false, ref isNotBlank);
            customerEdited.Pin = isNotBlank ? newPin : customerToEdit.Pin;

            Console.WriteLine($"First name of the customer, enter for default ({customerToEdit.FirstName}):");
            isNotBlank = ReadHelpers.DoesContinue(out var newFirstName);
            customerEdited.FirstName = isNotBlank ? newFirstName : customerToEdit.FirstName;

            Console.WriteLine($"Last name of the customer, enter for default ({customerToEdit.LastName}):");
            isNotBlank = ReadHelpers.DoesContinue(out var newLastName);
            customerEdited.LastName = isNotBlank ? newLastName : customerToEdit.LastName;

            _customerRepository.Edit(customerToEdit.Id, customerEdited);
        }
    }
}