using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.InventoryActions
{
    public class InventoryModifyAction:IAction
    {
        private readonly OfferRepository _offerRepository;
        private readonly OfferReadHelpers _offerReadHelper;

        public InventoryModifyAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
            _offerReadHelper = new OfferReadHelpers(offerRepository);
        }
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Modify inventory";

        public void Call()
        {
            var doesContinue = true;
            var offers = _offerRepository.GetAll().Where(o => o.Type != OfferType.Service).ToList();

            PrintHelpers.PrintOfferList(offers);

            while (true)
            {
                var offerName = _offerReadHelper.TryGetName(false, ref doesContinue);
                if (!doesContinue) return;

                Console.WriteLine("Enter offer name:");
                var offer = _offerRepository.FindByName(offerName);
                if (offer.Type == OfferType.Service)
                {
                    Console.WriteLine("Services have no quantity!");
                    continue;
                }

                Console.WriteLine($"Enter new quantity (old: {offer.Quantity}");
                var newQuantity = ReadHelpers.TryIntParse(ref doesContinue, 0);
                if (!doesContinue) break;

                _offerRepository.ChangeQuantity(offer.Id, newQuantity);
            }
        }
    }
}
