using System.Collections.Generic;
using PointOfSale.Domain.Factories;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Actions.EmployeeActions;

namespace PointOfSale.Presentation.Factories
{
    public static class EmployeeActionsFactory
    {
        public static EmployeeParentAction GetEmployeeParentAction()
        {
            var actions = new List<IAction>
            {
                new EmployeeAddAction(RepositoryFactory.GetRepository<EmployeeRepository>()),
                new EmployeeEditAction(RepositoryFactory.GetRepository<EmployeeRepository>()),
                new EmployeeDeleteAction(RepositoryFactory.GetRepository<EmployeeRepository>()),
                new ExitMenuAction()
            };

            return new EmployeeParentAction(actions);
        }
    }
}
