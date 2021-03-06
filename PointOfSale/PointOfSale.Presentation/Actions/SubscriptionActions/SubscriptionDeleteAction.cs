﻿using System;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.SubscriptionActions
{
    public class SubscriptionDeleteAction:IAction
    {
        private readonly SubscriptionBillRepository _subscriptionBillRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Cancel subscription";

        public SubscriptionDeleteAction(SubscriptionBillRepository subscriptionBillRepository)
        {
            _subscriptionBillRepository = subscriptionBillRepository;
        }
        public void Call()
        {
            var doesContinue = true;
            var activeSubscriptions = _subscriptionBillRepository.GetActiveSubscriptions();
            PrintHelpers.PrintSubscriptions(activeSubscriptions);
            if (activeSubscriptions.Count == 0) return;

            Console.WriteLine("Enter index of subscription you want to cancel:");
            var chosenSubscription = ReadHelpers.TryGetListMember(activeSubscriptions, ref doesContinue);
            if (!doesContinue) return;

            if (!ReadHelpers.Confirm("Are you sure you want to cancel subscription? (yes/no)"))
            {
                MessageHelpers.Success("Action stopped.");
                Console.ReadLine();
                return;
            }

            _subscriptionBillRepository.CancelSubscription(chosenSubscription.Id);

            MessageHelpers.Success("Cancelled!");
            Console.ReadLine();
        }
    }
}
