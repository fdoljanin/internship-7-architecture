using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class OfferCategoryHelpers
    {
        private readonly OfferRepository _offerRepository;
        private readonly OfferReadHelpers _offerReadHelper;

        public OfferCategoryHelpers(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
            _offerReadHelper = new OfferReadHelpers(offerRepository);
        }

        public int TryGetOfferId(bool needExisting, ICollection<OfferCategory> offersInCategory, ref bool doesContinue)
        {
            while (true)
            {
                var offerName = _offerReadHelper.TryGetName(false, ref doesContinue);
                if (!doesContinue) return default;
                var offerId = _offerRepository.FindByName(offerName).Id;
                if (offersInCategory.Any(oc => oc.OfferId == offerId) == needExisting)
                    return offerId;

                Console.WriteLine(needExisting ? $"Offer should be in category!" : "Offer is already there!");
            }
        }
    }
}
