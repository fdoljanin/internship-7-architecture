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
        private readonly OfferReadHelpers _offerReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add offer";

        public OfferAddAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
            _offerReadHelper = new OfferReadHelpers(offerRepository);
        }
        public void Call()
        {
            var doesContinue = true;

            Console.WriteLine("Enter type (Item, Service, Rent):");
            var type = ReadHelpers.TryEnumParse<OfferType>(ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter name of product:");
            var name = _offerReadHelper.TryGetName(true, ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter price, which is not negative:");
            var price = ReadHelpers.TryDecimalParse(ref doesContinue, 0);
            if (!doesContinue) return;

            var quantity = -1;
            if (type != OfferType.Service)
            {
                Console.WriteLine("Enter quantity, which is not negative:");
                quantity = ReadHelpers.TryIntParse(ref doesContinue, 0);
                if (!doesContinue) return;
            }
            _offerRepository.Add(
                new Offer()
                {
                    Type = type,
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
