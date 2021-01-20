using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Factories;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Actions.ReportActions;

namespace PointOfSale.Presentation.Factories
{
    public static class ReportActionsFactory
    {
        public static ReportParentAction GetReportParentAction()
        {
            var actions = new List<IAction>
            {
                new ReportByCategoryAction(RepositoryFactory.GetRepository<BillRepository>()),
                new ReportByOfferTypeAction(RepositoryFactory.GetRepository<BillRepository>()),
                new ReportArticleByCategoryAction(RepositoryFactory.GetRepository<ArticleBillRepository>()),
                new ReportActiveSubscriptions(RepositoryFactory.GetRepository<SubscriptionBillRepository>()),
                new ReportInventoryQuantity(RepositoryFactory.GetRepository<OfferRepository>()),
                new ReportTopSelling(RepositoryFactory.GetRepository<OfferRepository>()),
                new ReportYearProfit(RepositoryFactory.GetRepository<BillRepository>()),
                new ExitMenuAction()
            };

            return new ReportParentAction(actions);
        }
    }
}
