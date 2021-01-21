using System.Collections.Generic;
using PointOfSale.Domain.Factories;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Actions.SubscriptionActions;

namespace PointOfSale.Presentation.Factories
{
    public static class SubscriptionActionsFactory
    {
        public static SubscriptionParentAction GetSubscriptionParentAction()
        {
            var actions = new List<IAction>
            {
                new SubscriptionAddAction(
                    RepositoryFactory.GetRepository<CustomerRepository>(),
                    RepositoryFactory.GetRepository<SubscriptionBillRepository>()
                ),
                new SubscriptionDeleteAction(RepositoryFactory.GetRepository<SubscriptionBillRepository>()),
                new ExitMenuAction()
            };

            return new SubscriptionParentAction(actions);
        }

    }
}
