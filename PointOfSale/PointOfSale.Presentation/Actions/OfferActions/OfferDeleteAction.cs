using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Extensions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.OfferActions
{
    public class OfferDeleteAction:IAction
    {
        private readonly OfferRepository _offerRepository;
        private readonly OfferReadHelpers _offerReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete offer";

        public OfferDeleteAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
            _offerReadHelper = new OfferReadHelpers(offerRepository);
        }

        public void Call()
        {
            var doesContinue = true;
            var offerList = _offerRepository.GetAll();
            PrintHelpers.PrintOfferList(offerList);
            Console.WriteLine();

            Console.WriteLine("Enter name of the offer to delete:");
            var name = _offerReadHelper.TryGetName(false, ref doesContinue);
            if (!doesContinue) return;

            if (ReadHelpers.Confirm($"Are you sure you want to delete {name.Capitalize()}? (yes/no)"))
                _offerRepository.Delete(name);

        }
    }
}
