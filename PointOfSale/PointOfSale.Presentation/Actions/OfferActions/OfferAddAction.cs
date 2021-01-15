using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
        public string Label { get; set; } = "Add offer";

        public OfferAddAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public void Call()
        {
            var doesContinue = ReadHelpers.TryEnumParse<OfferType>("Enter type (Item, Service, Rent):", out var offerType);
            if (!doesContinue) return;
            doesContinue = OfferReadHelpers.TryGetName("Enter name of product:", _offerRepository, true, out var name);
            if (!doesContinue) return;
            doesContinue = ReadHelpers.TryDecimalParse("Enter price, which is not negative:", out var price, 0);
            if (!doesContinue) return;
            var quantity = -1;
            if (offerType != OfferType.Service)
            {
                doesContinue = ReadHelpers.TryIntParse("Enter quantity, which is not negative:", out var inQuantity, 0);
                if (!doesContinue) return;
                quantity = inQuantity;
            }
            _offerRepository.Add(
                new Offer()
                {
                    Type = offerType,
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    IsActive = true
                }
                );
            Console.WriteLine("Success!");
            
        }
    }
}
