using System;
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
            var subscriptionBill = new SubscriptionBill();

            var customerList = _customerRepository.GetAll();
            PrintHelpers.PrintPersonList(customerList);

            Console.WriteLine("Enter customer index:"); //if store is big, implement search by pin
            var customer = ReadHelpers.TryGetListMember(customerList, ref doesContinue);
            if (!doesContinue) return;

            var offerSubscriptionList = _subscriptionBillRepository.GetAllAvailable();
            PrintHelpers.PrintOfferList(offerSubscriptionList);

            Console.WriteLine("Enter offer index:");
            var offerSubscription = ReadHelpers.TryGetListMember(offerSubscriptionList, ref doesContinue);
            if (!doesContinue) return;

            subscriptionBill.OfferId = offerSubscription.Id;
            subscriptionBill.CustomerId = customer.Id;
            subscriptionBill.StartTime = DateTime.Now;

            _subscriptionBillRepository.AddSubscription(subscriptionBill);
        }
    }
}
