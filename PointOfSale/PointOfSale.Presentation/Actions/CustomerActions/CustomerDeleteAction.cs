using System;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.CustomerActions
{
    public class CustomerDeleteAction:IAction
    {
        private readonly CustomerRepository _customerRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete customer";

        public CustomerDeleteAction(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public void Call()
        {
            var doesContinue = true;
            var customerList = _customerRepository.GetAll();
            PrintHelpers.PrintPersonList(customerList);
            if (customerList.Count == 0) return;

            Console.WriteLine("Enter index of customer you want to delete:");
            var customerToDelete = ReadHelpers.TryGetListMember(customerList, ref doesContinue);
            if (!doesContinue) return;

            if (!_customerRepository.CheckIsDeletable(customerToDelete.Id))
            {
                MessageHelpers.Error($"Customer {customerToDelete.FirstName} has subscriptions to pay!");
                Console.ReadLine();
                return;
            }

            if (!ReadHelpers.Confirm($"Do you want to delete customer {customerToDelete.FirstName}? (yes/no)"))
            {
                MessageHelpers.Success("Action cancelled.");
                Console.ReadLine();
                return;
            }

            _customerRepository.Delete(customerToDelete.Id);
            MessageHelpers.Success("Customer deleted!");
            Console.ReadLine();
        }
    }
}
