using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public static class OfferCategoryHelpers
    {
        public static bool TryGetOfferNameIfUnique(string message, OfferRepository offerRepository, bool needExisting,
            ICollection<OfferCategory> offersInCategory, out int offerId)
        {
            offerId = default;
            while (true)
            {
                var doesContinue = OfferReadHelpers.TryGetName(message, offerRepository, false, out var offerName);
                if (!doesContinue) return false;
                var findOfferId = offerRepository.FindByName(offerName).Id;
                offerId = findOfferId;
                if (offersInCategory.Any(oc => oc.OfferId == findOfferId) == needExisting)
                    return true;

                Console.WriteLine(needExisting ? $"Offer should be in category!" : "Offer is already there!");
            }
        }
    }
}
