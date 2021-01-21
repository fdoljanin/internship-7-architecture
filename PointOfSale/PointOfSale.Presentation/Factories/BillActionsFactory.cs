using System.Collections.Generic;
using PointOfSale.Domain.Factories;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Actions.BillActions;

namespace PointOfSale.Presentation.Factories
{
    public static class BillActionsFactory
    {
        public static BillParentAction GetBillParentAction()
        {
            var actions = new List<IAction>
            {
                new OneOffBillAction
                (
                    RepositoryFactory.GetRepository<BillRepository>(),
                    RepositoryFactory.GetRepository<ArticleBillRepository>(),
                    RepositoryFactory.GetRepository<ServiceBillRepository>(),
                    RepositoryFactory.GetRepository<EmployeeRepository>()
                ),
                new ServiceBillAction(
                    RepositoryFactory.GetRepository<ServiceBillRepository>(),
                    RepositoryFactory.GetRepository<BillRepository>(),
                    RepositoryFactory.GetRepository<EmployeeRepository>()
                ),
                new SubscriptionBillAction(
                    RepositoryFactory.GetRepository<CustomerRepository>(),
                    RepositoryFactory.GetRepository<BillRepository>()
                ),
                new CancelBill(
                    RepositoryFactory.GetRepository<BillRepository>()
                ),
                new ExitMenuAction()
            };

            return new BillParentAction(actions);
        }
    }
}
