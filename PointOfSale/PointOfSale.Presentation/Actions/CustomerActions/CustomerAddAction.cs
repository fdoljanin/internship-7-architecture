using System;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.CustomerActions
{
    public class CustomerAddAction:IAction
    {
        private readonly CustomerRepository _customerRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add Customer";

        public CustomerAddAction(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public void Call()
        {
            var doesContinue = true;
            var customer = new Customer();

            Console.WriteLine("Enter customer pin:");
            customer.Pin = UniqueReadHelpers.TryGetUniquePin(_customerRepository, ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter first name of the customer:");
            customer.FirstName = ReadHelpers.TryGetInput(ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter last name of the customer:");
            customer.LastName = ReadHelpers.TryGetInput(ref doesContinue);
            if (!doesContinue) return;

            _customerRepository.Add(customer);

            MessageHelpers.Success("Customer added!");
            Console.ReadLine();
        }
    }
}
