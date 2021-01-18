using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.SubscriptionActions
{
    public class SubscriptionDeleteAction:IAction
    {
        private readonly SubscriptionBillRepository _subscriptionBillRepository;
        public string Label { get; set; } = "Cancel subscription";

        public SubscriptionDeleteAction(SubscriptionBillRepository subscriptionBillRepository)
        {
            _subscriptionBillRepository = subscriptionBillRepository;
        }
        public void Call()
        {
            var doesContinue = true;
            var activeSubscriptions = _subscriptionBillRepository.GetActiveSubscriptions();

            Console.WriteLine("Enter index of subscription you want to cancel:");
            PrintHelpers.PrintSubscriptions(activeSubscriptions);

            var chosenIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, activeSubscriptions.Count);
            if (!doesContinue) return;

            if (!ReadHelpers.Confirm($"Are you sure you want to cancel subscription number {chosenIndex}")) return;

            _subscriptionBillRepository.CancelSubscription(activeSubscriptions.ElementAt(chosenIndex - 1).Id);
            Console.WriteLine("Cancelled!");
        }
    }
}
