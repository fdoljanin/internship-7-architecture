using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class OfferReadHelpers
    {
        private readonly OfferRepository _offerRepository;

        public OfferReadHelpers(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public string TryGetName(bool needUnique, ref bool doesContinue)
        {
            while (true)
            {
                doesContinue = ReadHelpers.DoesContinue(out var name);
                if (!doesContinue) return null;
                var unique = _offerRepository.CheckUnique(name);
                if (unique == needUnique) return name;
                Console.WriteLine(needUnique ? "Name needs to be unique!" : "Product with that name does not exist!");
            }
        }
    }
}
