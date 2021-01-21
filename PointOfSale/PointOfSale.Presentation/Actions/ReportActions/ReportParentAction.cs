using System;
using System.Collections.Generic;
using PointOfSale.Presentation.Abstractions;

namespace PointOfSale.Presentation.Actions.ReportActions
{
    public class ReportParentAction : BaseParentAction
    {
        public ReportParentAction(IList<IAction> actions) : base(actions)
        {
            Label = "See reports";
        }

        public override void Call()
        {
            Console.WriteLine("Reports");
            base.Call();
        }
    }
}
