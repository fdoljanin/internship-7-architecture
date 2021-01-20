using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Factories;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions.BillActions;

namespace PointOfSale.Presentation.Factories
{
    public static class BillActionsFactory
    {
        public static BillParentAction GetBillParentAction()
        {
            var billActions = new List<IAction>
            {
                new OneOffBillAction
                (
                    RepositoryFactory.GetRepository<BillRepository>(),
                    RepositoryFactory.GetRepository<ArticleBillRepository>(),
                    RepositoryFactory.GetRepository<ServiceBillRepository>(),
                    RepositoryFactory.GetRepository<EmployeeRepository>()
                 ),

            }
        }
    }
}
