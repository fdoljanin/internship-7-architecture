using System;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.OfferActions
{
    public class OfferAddAction : IAction
    {
        private readonly OfferRepository _offerRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add offer";

        public OfferAddAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public void Call()
        {
            var doesContinue = true;
            var offer = new Offer()
            {
                IsActive = true
            };

            Console.WriteLine("Enter type (Item, Service, Rent):");
            offer.Type = ReadHelpers.TryEnumParse<OfferType>(ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter name of product:");
            offer.Name = UniqueReadHelpers.TryGetUniqueString(_offerRepository, ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter price, which is not negative:");
            offer.Price = ReadHelpers.TryDecimalParse(ref doesContinue, 0);
            if (!doesContinue) return;

            if (offer.Type != OfferType.Service)
            {
                Console.WriteLine("Enter quantity, which is not negative:");
                offer.Quantity = ReadHelpers.TryIntParse(ref doesContinue, 0);
                if (!doesContinue) return;
            }

            _offerRepository.Add(offer);

            Console.WriteLine("Success!");
            Console.ReadLine();
        }
    }
}
