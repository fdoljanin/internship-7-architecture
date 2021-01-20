using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.ReportActions
{
    public class ReportActiveSubscriptions:IAction
    {
        private readonly SubscriptionBillRepository _subscriptionBillRepository;

        public ReportActiveSubscriptions(SubscriptionBillRepository subscriptionBillRepository)
        {
            _subscriptionBillRepository = subscriptionBillRepository;
        }
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Get active subscriptions";

        public void Call()
        {
            Console.WriteLine("Active subscriptions");
            var activeSubscriptions = _subscriptionBillRepository.GetActiveSubscriptions();
            PrintHelpers.PrintSubscriptions(activeSubscriptions);
        }
    }
}
