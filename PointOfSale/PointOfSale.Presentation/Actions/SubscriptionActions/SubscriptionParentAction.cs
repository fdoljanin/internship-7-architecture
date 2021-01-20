using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Presentation.Abstractions;

namespace PointOfSale.Presentation.Actions.SubscriptionActions
{
    public class SubscriptionParentAction : BaseParentAction
    {
        public SubscriptionParentAction(IList<IAction> actions) : base(actions)
        {
            Label = "Manage subscriptions";
        }

        public override void Call()
        {
            Console.WriteLine("Subscription management");
            base.Call();
        }
    }
}
