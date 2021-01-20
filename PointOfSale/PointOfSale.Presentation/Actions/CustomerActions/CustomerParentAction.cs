using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Presentation.Abstractions;

namespace PointOfSale.Presentation.Actions.CustomerActions
{
    public class CustomerParentAction : BaseParentAction
    {
        public CustomerParentAction(IList<IAction> actions) : base(actions)
        {
            Label = "Manage customers";
        }

        public override void Call()
        {
            Console.WriteLine("Customer management");
            base.Call();
        }
    }
}
