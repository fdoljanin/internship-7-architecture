using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Factories;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Actions.OfferActions;

namespace PointOfSale.Presentation.Factories
{
    public static class OfferActionsFactory
    {
        public static OfferParentAction GetOfferParentAction()
        {
            var actions = new List<IAction>
            {
                new OfferAddAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new OfferEditAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new OfferDeleteAction(RepositoryFactory.GetRepository<OfferRepository>()),
                new ExitMenuAction()
            };
            
            return new OfferParentAction(actions);
        }
    }
}
