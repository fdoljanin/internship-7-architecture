using System.Collections.Generic;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Extensions;

namespace PointOfSale.Presentation.Factories
{
    public static class MainMenuFactory
    {
        public static IList<IAction> GetMainMenuActions()
        {
            var actions = new List<IAction>
            {
                OfferActionsFactory.GetOfferParentAction(),
                CategoryActionsFactory.GetCategoryParentAction(),
                BillActionsFactory.GetBillParentAction(),
                SubscriptionActionsFactory.GetSubscriptionParentAction(),
                CustomerActionsFactory.GetCustomerParentAction(),
                EmployeeActionsFactory.GetEmployeeParentAction(),
                InventoryActionsFactory.GetInventoryParentAction(),
                ReportActionsFactory.GetReportParentAction(),
                new ExitMenuAction()
            };

            actions.SetActionIndexes();
            return actions;
        }
    }
}
