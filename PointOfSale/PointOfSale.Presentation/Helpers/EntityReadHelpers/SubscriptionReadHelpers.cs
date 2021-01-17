using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class SubscriptionReadHelpers
    {
        private readonly SubscriptionBillRepository _subscriptionRepository;
        public SubscriptionReadHelpers(SubscriptionBillRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public Offer GetSubscription(ref bool doesContinue)
        {
            while (true)
            {
                doesContinue = ReadHelpers.DoesContinue(out var name);
                if (!doesContinue) return null;
                if (_subscriptionRepository.CheckUnique(name))
                {
                    Console.WriteLine("Subscription offer does not exist!");
                    continue;
                }

                var subscription = _subscriptionRepository.FindByName(name);

                if (subscription.Quantity > 0) return subscription;
                Console.WriteLine("Subscription is not currently available!");
            }
        }

        


    }
}
