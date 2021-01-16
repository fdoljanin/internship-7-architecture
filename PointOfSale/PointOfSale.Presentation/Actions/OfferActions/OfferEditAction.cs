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
        private readonly OfferReadHelpers _offerReadHelper;
        public string Label { get; set; } = "Edit offer";

        public OfferEditAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
            _offerReadHelper = new OfferReadHelpers(offerRepository);
        }

        public void Call()
        {
            var isNotBlank = true;
            var offerList = _offerRepository.GetAll();
            PrintHelpers.PrintOfferList(offerList);
            Console.WriteLine("");

            var offerEdited = new Offer();

            Console.WriteLine("Enter name of the offer:");
            var name = _offerReadHelper.TryGetName(false, ref isNotBlank);
            if (!isNotBlank) return;
            var offerToEdit = _offerRepository.FindByName(name);

            Console.WriteLine($"Enter new name of the offer, enter for default ({offerToEdit.Name}):");
            var newName = _offerReadHelper.TryGetName(true, ref isNotBlank);
            offerEdited.Name = isNotBlank ? newName : offerToEdit.Name;


            Console.WriteLine($"Enter new price which is positive, enter for default ({offerToEdit.Price}):");
            var newPrice = ReadHelpers.TryDecimalParse(ref isNotBlank, 0);
            offerEdited.Price = isNotBlank ? newPrice : offerToEdit.Price;

            _offerRepository.Edit(offerToEdit.Id, offerEdited);
            Console.WriteLine("Offer edited!");
        }
    }
}
