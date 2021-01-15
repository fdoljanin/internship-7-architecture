using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.OfferActions
{
    public class OfferDeleteAction:IAction
    {
        private readonly OfferRepository _offerRepository;
        public string Label { get; set; } = "Delete offer";

        public OfferDeleteAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            string message;
            var offerList = _offerRepository.GetAll();
            PrintHelpers.PrintOfferList(offerList);
            Console.WriteLine("");

            message = "Enter name of the offer to delete:";
            var doesContinue =
                OfferReadHelpers.TryGetName(message, _offerRepository, false, out var name);
            if (!doesContinue) return;

            if (ReadHelpers.Confirm($"Are you sure you want to delete {name}? (yes/no)"))
                _offerRepository.Delete(name);

        }
    }
}
