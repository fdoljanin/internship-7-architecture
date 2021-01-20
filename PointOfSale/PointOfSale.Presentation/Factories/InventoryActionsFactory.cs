using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Factories;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Actions.InventoryActions;

namespace PointOfSale.Presentation.Factories
{
    public static class InventoryActionsFactory
    {
        public static InventoryParentAction GetInventoryParentAction()
        {
            var actions = new List<IAction>
            {
                new InventoryModifyAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new ArticleStatusAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new ServiceStatusAction(RepositoryFactory.GetRepository<EmployeeRepository>()),
                new SubscriptionStatusAction(RepositoryFactory.GetRepository<SubscriptionBillRepository>()),
                new ExitMenuAction()
            };

            return new InventoryParentAction(actions);
        }
    }
}
