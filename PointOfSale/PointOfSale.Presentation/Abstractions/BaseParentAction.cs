﻿using System.Collections.Generic;
using PointOfSale.Presentation.Extensions;

namespace PointOfSale.Presentation.Abstractions
{
    public abstract class BaseParentAction : IAction
    {
        public int MenuIndex { get; set; }
        public string Label { get; set; }
        public IList<IAction> Actions { get; set; }

        protected BaseParentAction(IList<IAction> actions)
        {
            actions.SetActionIndexes();
            Actions = actions;
        }

        public virtual void Call()
        {
            Actions.PrintActionsAndCall();
        }

    }
}
