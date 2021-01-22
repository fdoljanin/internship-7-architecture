using System;
using System.Linq;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

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
            var offerList = _offerRepository.GetAll().Where(o => o.Type != OfferType.Service).ToList();

            PrintHelpers.PrintOfferList(offerList);

            while (true)
            {
                Console.WriteLine("Enter offer index");
                var offer = ReadHelpers.TryGetListMember(offerList, ref doesContinue);
                if (!doesContinue) return;

                Console.WriteLine($"Enter new quantity for {offer.Name} (old: {offer.Quantity}):");
                var newQuantity = ReadHelpers.TryIntParse(ref doesContinue, 0);
                if (!doesContinue) break;

                offer.Quantity = newQuantity;
                _offerRepository.Edit(offer.Id, offer);

                Console.WriteLine("Quantity modified!");
            }
        }
    }
}
