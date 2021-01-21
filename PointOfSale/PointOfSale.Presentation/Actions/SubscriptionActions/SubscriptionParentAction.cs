using System;
using System.Collections.Generic;
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
