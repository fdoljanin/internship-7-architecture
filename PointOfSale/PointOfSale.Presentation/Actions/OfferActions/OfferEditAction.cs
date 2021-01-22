using System;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.OfferActions
{
    public class OfferEditAction : IAction
    {
        private readonly OfferRepository _offerRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit offer";

        public OfferEditAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            var isNotBlank = true;
            var offerList = _offerRepository.GetAll();
            PrintHelpers.PrintOfferList(offerList);

            Console.WriteLine("Enter offer index:");
            var offerToEdit = ReadHelpers.TryGetListMember(offerList, ref isNotBlank);
            if (!isNotBlank) return;

            Console.WriteLine($"Enter new name of the offer, enter for default ({offerToEdit.Name}):");
            var newName = UniqueReadHelpers.TryGetUniqueString(_offerRepository, ref isNotBlank);
            offerToEdit.Name = isNotBlank ? newName : offerToEdit.Name;


            Console.WriteLine($"Enter new price which is positive, enter for default ({offerToEdit.Price}):");
            var newPrice = ReadHelpers.TryDecimalParse(ref isNotBlank, 0);
            offerToEdit.Price = isNotBlank ? newPrice : offerToEdit.Price;

            _offerRepository.Edit(offerToEdit.Id, offerToEdit);

            Console.WriteLine("Offer edited!");
            Console.ReadLine();
        }
    }
}
