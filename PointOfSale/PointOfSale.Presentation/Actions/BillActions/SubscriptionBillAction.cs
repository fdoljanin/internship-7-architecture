using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.BillActions
{
    public class SubscriptionBillAction:IAction
    {
        private readonly PersonReadHelpers _customerReadHelper;
        private readonly BillRepository _billRepository;
        private readonly CustomerRepository _customerRepository;
        public string Label { get; set; } = "Add subscription bill";
        public SubscriptionBillAction(CustomerRepository customerRepository, BillRepository billRepository)
        {
            _billRepository = billRepository;
            _customerReadHelper = new PersonReadHelpers(customerRepository);
            _customerRepository = customerRepository;
        }

        public void Call()
        {
            var doesContinue = true;
            PrintHelpers.PrintPersonList(_customerRepository.GetAll());
            Console.WriteLine("Enter customer pin:");
            var pin = _customerReadHelper.TryGetPin(true, ref doesContinue);
            if (!doesContinue) return;

            var price = _billRepository.GetSubscriptionBill(pin);
            Console.WriteLine($"Price: {price}");
        }
    }
}
