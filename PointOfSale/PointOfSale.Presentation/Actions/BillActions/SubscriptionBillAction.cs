using System;
using System.Linq;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.BillActions
{
    public class SubscriptionBillAction:IAction
    {
        private readonly BillRepository _billRepository;
        private readonly CustomerRepository _customerRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add subscription bill";
        public SubscriptionBillAction(CustomerRepository customerRepository, BillRepository billRepository)
        {
            _billRepository = billRepository;
            _customerRepository = customerRepository;
        }

        public void Call()
        {
            var doesContinue = true;
            var customerList = _customerRepository.GetAll();
            PrintHelpers.PrintPersonList(customerList);
            Console.WriteLine("Enter customer index:");
            var customerIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, customerList.Count) - 1;
            if (!doesContinue) return;

            var customer = customerList.ElementAt(customerIndex);
            var price = _billRepository.GetSubscriptionBill(customer.Id);
            Console.WriteLine($"Price: {price}");
            Console.ReadLine();
        }
    }
}
