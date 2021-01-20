using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Presentation.Abstractions;

namespace PointOfSale.Presentation.Actions.InventoryActions
{
    public class InventoryParentAction : BaseParentAction
    {
        public InventoryParentAction(IList<IAction> actions) : base(actions)
        {
            Label = "Manage inventory";
        }

        public override void Call()
        {
            Console.WriteLine("Inventory management");
            base.Call();
        }
    }
}
