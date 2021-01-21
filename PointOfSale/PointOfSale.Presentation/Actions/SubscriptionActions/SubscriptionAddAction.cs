using System;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.SubscriptionActions
{
    public class SubscriptionAddAction : IAction
    {
        private readonly CustomerRepository _customerRepository;
        private readonly SubscriptionBillRepository _subscriptionBillRepository;


        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add Subscription";

        public SubscriptionAddAction(CustomerRepository customerRepository,
            SubscriptionBillRepository subscriptionBillRepository)
        {
            _customerRepository = customerRepository;
            _subscriptionBillRepository = subscriptionBillRepository;
        }

        public void Call()
        { 
            var doesContinue = true;
            var customerList = _customerRepository.GetAll();

            PrintHelpers.PrintPersonList(customerList);

            Console.WriteLine("Enter customer index:"); //if store is big, implement search by pin
            var customerIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, customerList.Count) - 1;
            if (!doesContinue) return;
            var customer = customerList.ElementAt(customerIndex);

            var subscriptionList = _subscriptionBillRepository.GetAllAvailable();
            PrintHelpers.PrintOfferList(subscriptionList);

            Console.WriteLine("Enter subscription index:");
            var subscriptionIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, subscriptionList.Count) -1;
            if (!doesContinue) return;

            var subscription = subscriptionList.ElementAt(subscriptionIndex);

            _subscriptionBillRepository.AddSubscription(
            new SubscriptionBill()
            {
                CustomerId = customer.Id,
                OfferId = subscription.Id,
                StartTime = DateTime.Now,
            });
        }
    }
}
