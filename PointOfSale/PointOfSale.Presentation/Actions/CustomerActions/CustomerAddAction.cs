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
    public class CustomerAddAction:IAction
    {
        private readonly CustomerRepository _customerRepository;
        private readonly UniqueReadHelpers _uniqueReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add Customer";

        public CustomerAddAction(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _uniqueReadHelper = new UniqueReadHelpers(customerRepository);
        }
        public void Call()
        {
            var doesContinue = true;
            Console.WriteLine("Enter customer pin:");
            var pin = _uniqueReadHelper.TryGetUniquePin(ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter first name of the customer:");
            doesContinue = ReadHelpers.DoesContinue(out var firstName);
            if (!doesContinue) return;

            Console.WriteLine("Enter last name of the customer:");
            doesContinue = ReadHelpers.DoesContinue(out var lastName);
            if (!doesContinue) return;

            _customerRepository.Add(
                new Customer()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Pin = pin
                });
        }
    }
}
