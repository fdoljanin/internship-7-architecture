using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.ReportActions
{
    public class ReportInventoryQuantity:IAction
    {
        private readonly OfferRepository _offerRepository;

        public ReportInventoryQuantity(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Get articles by quantity";

        public void Call()
        {
            var doesContinue = true;

            var range = ReadHelpers.TryGetLessOrMore(ref doesContinue);
            if (!doesContinue) return;

            var articles = _offerRepository.GetArticlesLessOrMore(range);
            PrintHelpers.PrintOfferList(articles);

            Console.ReadLine();
        }
    }
}
