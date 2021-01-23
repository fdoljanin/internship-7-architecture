using System;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.ReportActions
{
    public class ReportArticleByCategoryAction:IAction
    {
        private readonly ArticleBillRepository _articleBillRepository;

        public ReportArticleByCategoryAction(ArticleBillRepository articleBillRepository)
        {
            _articleBillRepository = articleBillRepository;
        }
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Get article by category";
        public void Call()
        {
            var countGrouped = _articleBillRepository.GetCountByCategory();
            if (countGrouped.Count == 0)
            {
                MessageHelpers.NotAvailable("Nothing to show.");
                return;
            }

            Console.WriteLine("NAME\t\tCOUNT");
            foreach (var categoryCount in countGrouped)
            {
                Console.WriteLine($"{categoryCount.Name}\t\t{categoryCount.Count}");
            }

            Console.ReadLine();
        }
    }
}
