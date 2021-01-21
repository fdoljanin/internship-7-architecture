using System.Collections.Generic;
using PointOfSale.Domain.Factories;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Actions.CustomerActions;

namespace PointOfSale.Presentation.Factories
{
    public static class CustomerActionsFactory
    {
        public static CustomerParentAction GetCustomerParentAction()
        {
            var actions = new List<IAction>
            {
                new CustomerAddAction(RepositoryFactory.GetRepository<CustomerRepository>()),
                new CustomerEditAction(RepositoryFactory.GetRepository<CustomerRepository>()),
                new ExitMenuAction()
            };

            return new CustomerParentAction(actions);
        }
    }
}
