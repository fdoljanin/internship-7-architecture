using System;
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
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit Customer";

        public CustomerEditAction(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public void Call()
        {
            var isNotBlank = true;
            var customerEdited = new Customer();
            var customerList = _customerRepository.GetAll();
            PrintHelpers.PrintPersonList(customerList);

            Console.WriteLine("Enter index of customer you want to edit:");
            var customerToEdit = ReadHelpers.TryGetListMember(customerList, ref isNotBlank);
            if (!isNotBlank) return;

            Console.WriteLine($"New pin, enter for default ({customerToEdit.Pin}):");
            var newPin = UniqueReadHelpers.TryGetUniquePin(_customerRepository ,ref isNotBlank);
            customerEdited.Pin = isNotBlank ? newPin : customerToEdit.Pin;

            Console.WriteLine($"First name of the customer, enter for default ({customerToEdit.FirstName}):");
            var newFirstName = ReadHelpers.TryGetInput(ref isNotBlank);
            customerEdited.FirstName = isNotBlank ? newFirstName : customerToEdit.FirstName;

            Console.WriteLine($"Last name of the customer, enter for default ({customerToEdit.LastName}):");
            var newLastName = ReadHelpers.TryGetInput(ref isNotBlank);
            customerEdited.LastName = isNotBlank ? newLastName : customerToEdit.LastName;

            _customerRepository.Edit(customerToEdit.Id, customerEdited);

            Console.WriteLine("Customer edited!");
            Console.ReadLine();
        }
    }
}