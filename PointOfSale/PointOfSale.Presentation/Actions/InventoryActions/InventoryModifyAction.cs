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

        public InventoryModifyAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
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
                Console.WriteLine("Enter offer index");
                var offerIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, offers.Count) - 1;
                if (!doesContinue) return;
                var offer = offers.ElementAt(offerIndex);

                Console.WriteLine($"Enter new quantity for {offer.Name} (old: {offer.Quantity})");
                var newQuantity = ReadHelpers.TryIntParse(ref doesContinue, 0);
                if (!doesContinue) break;

                _offerRepository.ChangeQuantity(offer.Id, newQuantity);
            }
        }
    }
}
