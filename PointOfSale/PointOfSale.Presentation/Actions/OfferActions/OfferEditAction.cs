using System;
using System.Linq;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.OfferActions
{
    public class OfferEditAction : IAction
    {
        private readonly OfferRepository _offerRepository;
        private readonly UniqueReadHelpers _uniqueReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit offer";

        public OfferEditAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
            _uniqueReadHelper = new UniqueReadHelpers(offerRepository);
        }

        public void Call()
        {
            var isNotBlank = true;
            var offerList = _offerRepository.GetAll();
            PrintHelpers.PrintOfferList(offerList);
            Console.WriteLine();

            Console.WriteLine("Enter offer index:");
            var offerIndex = ReadHelpers.TryIntParse(ref isNotBlank, 1, offerList.Count) - 1;
            if (!isNotBlank) return;
            var offerToEdit = offerList.ElementAt(offerIndex);

            Console.WriteLine($"Enter new name of the offer, enter for default ({offerToEdit.Name}):");
            var newName = _uniqueReadHelper.TryGetUniqueString(ref isNotBlank);
            offerToEdit.Name = isNotBlank ? newName : offerToEdit.Name;


            Console.WriteLine($"Enter new price which is positive, enter for default ({offerToEdit.Price}):");
            var newPrice = ReadHelpers.TryDecimalParse(ref isNotBlank, 0);
            offerToEdit.Price = isNotBlank ? newPrice : offerToEdit.Price;

            _offerRepository.Edit(offerToEdit.Id, offerToEdit);
            Console.WriteLine("Offer edited!");
        }
    }
}
