using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Presentation.Abstractions;

namespace PointOfSale.Presentation.Actions.EmployeeActions
{
    public class EmployeeParentAction : BaseParentAction
    {
        public EmployeeParentAction(IList<IAction> actions) : base(actions)
        {
            Label = "Manage employees";
        }

        public override void Call()
        {
            Console.WriteLine("Employee management");
            base.Call();
        }
    }
}
