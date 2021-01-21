using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.InventoryActions
{
    class SubscriptionStatusAction : IAction
    {
        private readonly SubscriptionBillRepository _subscriptionRepository;

        public SubscriptionStatusAction(SubscriptionBillRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Subscription inventory status";
        public void Call()
        {
            var availableSubscriptions = _subscriptionRepository.GetAllAvailable();
            PrintHelpers.PrintOfferList(availableSubscriptions);

            Console.ReadLine();
        }
    }
}
