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

namespace PointOfSale.Presentation.Actions.SubscriptionActions
{
    public class SubscriptionAddAction : IAction
    {
        private readonly CustomerRepository _customerRepository;
        private readonly SubscriptionBillRepository _subscriptionBillRepository;

        private readonly SubscriptionReadHelpers _subscriptionReadHelper;
        private readonly PersonReadHelpers _customerReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add Subscription";

        public SubscriptionAddAction(CustomerRepository customerRepository,
            SubscriptionBillRepository subscriptionBillRepository)
        {
            _customerRepository = customerRepository;
            _subscriptionBillRepository = subscriptionBillRepository;
            _subscriptionReadHelper = new SubscriptionReadHelpers(subscriptionBillRepository);
            _customerReadHelper = new PersonReadHelpers(customerRepository);
        }

        public void Call()
        { 
            var doesContinue = true;

            PrintHelpers.PrintPersonList(_customerRepository.GetAll());

            Console.WriteLine("Enter customer PIN:");
            var customerPin = _customerReadHelper.TryGetPin(true, ref doesContinue);
            if (!doesContinue) return;
            var customer = _customerRepository.GetByPin(customerPin);

            PrintHelpers.PrintOfferList(_subscriptionBillRepository.GetAllAvailable());

            Console.WriteLine("Enter subscription name:");
            var chosenSubscription = _subscriptionReadHelper.GetSubscription(ref doesContinue);
            if (!doesContinue) return;
            
            _subscriptionBillRepository.AddSubscription(
            new SubscriptionBill()
            {
                CustomerId = customer.Id,
                OfferId = chosenSubscription.Id,
                StartTime = DateTime.Now,

            });
        }
    }
}
