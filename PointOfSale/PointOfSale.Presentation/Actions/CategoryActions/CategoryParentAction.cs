using System;
using System.Collections.Generic;
using PointOfSale.Presentation.Abstractions;

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class CategoryParentAction : BaseParentAction
    {
        public CategoryParentAction(IList<IAction> actions) : base(actions)
        {
            Label = "Manage categories";
        }

        public override void Call()
        {
            Console.WriteLine("Category management");
            base.Call();
        }
    }
}
