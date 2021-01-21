using System;
using System.Linq;
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
        private readonly UniqueReadHelpers _uniqueReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit Customer";

        public CustomerEditAction(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _uniqueReadHelper = new UniqueReadHelpers(customerRepository);
        }
        public void Call()
        {
            var customerEdited = new Customer();
            var customerList = _customerRepository.GetAll();

            PrintHelpers.PrintPersonList(customerList);
            var isNotBlank = true;
            Console.WriteLine("Enter index of customer you want to edit:");
            var customerIndex = ReadHelpers.TryIntParse(ref isNotBlank, 1, customerList.Count) - 1;
            if (!isNotBlank) return;

            var customerToEdit = customerList.ElementAt(customerIndex);

            Console.WriteLine($"New pin, enter for default ({customerToEdit.Pin}):");
            var newPin = _uniqueReadHelper.TryGetUniquePin(ref isNotBlank);
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