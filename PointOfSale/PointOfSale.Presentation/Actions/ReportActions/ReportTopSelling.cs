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
    public class ReportTopSelling:IAction
    {
        private readonly OfferRepository _offerRepository;

        public ReportTopSelling(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Show top 10 selling";

        public void Call()
        {
            var topSelling = _offerRepository.GetTopSell(10);
            foreach (var topSell in topSelling)
            {
                topSell.Offer.Quantity = topSell.Quantity;
            }

            PrintHelpers.PrintOfferList(topSelling.Select(ts => ts.Offer).ToList());

            Console.ReadLine();
        }
    }
}
