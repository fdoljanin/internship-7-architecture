using System.Collections.Generic;
using PointOfSale.Domain.Factories;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Actions.CategoryActions;

namespace PointOfSale.Presentation.Factories
{
    public static class CategoryActionsFactory
    {
        public static CategoryParentAction GetCategoryParentAction()
        {
            var actions = new List<IAction>
            {
                new CategoryAddAction(RepositoryFactory.GetRepository<CategoryRepository>()),
                new CategoryEditAction(RepositoryFactory.GetRepository<CategoryRepository>()),
                new CategoryDeleteAction(RepositoryFactory.GetRepository<CategoryRepository>()),
                new OfferCategoryListAction(
                    RepositoryFactory.GetRepository<OfferCategoryRepository>(),
                    RepositoryFactory.GetRepository<CategoryRepository>()
                    ),
                new OfferCategoryAddAction(
                    RepositoryFactory.GetRepository<OfferCategoryRepository>(),
                    RepositoryFactory.GetRepository<CategoryRepository>()
                ),
                new OfferCategoryDeleteAction(
                    RepositoryFactory.GetRepository<OfferCategoryRepository>(),
                    RepositoryFactory.GetRepository<CategoryRepository>()
                ),
                new ExitMenuAction()
            };

            return new CategoryParentAction(actions);
        }
    }
}
