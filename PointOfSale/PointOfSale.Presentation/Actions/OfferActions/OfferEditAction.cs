using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.OfferActions
{
    public class OfferEditAction : IAction
    {
        private readonly OfferRepository _offerRepository;
        public string Label { get; set; } = "Add offer";

        public OfferEditAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            string message;
            var offerList = _offerRepository.GetAll();
            PrintHelpers.PrintOfferList(offerList);
            Console.WriteLine("");

            var offerEdited = new Offer();

            message = "Enter name of the offer:";
            var doesContinue =
                OfferReadHelpers.TryGetName(message, _offerRepository, false, out var name);
            if (!doesContinue) return;
            var offerToEdit = _offerRepository.FindByName(name);

            message = $"Enter new name of the offer, enter for default ({offerToEdit.Name}):";
            var notDefault =
                OfferReadHelpers.TryGetName(message, _offerRepository, true, out var newName);
            offerEdited.Name = notDefault ? newName : offerToEdit.Name;
            

            message = $"Enter new price which is positive, enter for default {offerToEdit.Price}:";
            notDefault = ReadHelpers.TryDecimalParse(message, out var newPrice, 0);
            offerEdited.Price = notDefault ? newPrice : offerToEdit.Price;

            _offerRepository.Edit(offerToEdit.Id, offerEdited);
            Console.WriteLine("Offer edited!");
        }
    }
}
