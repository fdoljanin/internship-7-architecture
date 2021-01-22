using System;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.OfferActions
{
    public class OfferDeleteAction:IAction
    {
        private readonly OfferRepository _offerRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete offer";

        public OfferDeleteAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            var doesContinue = true;
            var offerList = _offerRepository.GetAll();
            PrintHelpers.PrintOfferList(offerList);

            Console.WriteLine("Enter offer index to delete:");
            var offerToDelete = ReadHelpers.TryGetListMember(offerList, ref doesContinue);
            if (!doesContinue) return;

            if (ReadHelpers.Confirm($"Are you sure you want to delete {offerToDelete.Name}? (yes/no)"))
            {
                _offerRepository.Delete(offerToDelete.Id);
                Console.WriteLine("Offer deleted!");
            }
            else 
                Console.WriteLine("Action cancelled!");

            Console.ReadLine();
        }
    }
}
