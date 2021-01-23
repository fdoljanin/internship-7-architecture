using System;
using System.Linq;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class OfferCategoryDeleteAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly CategoryRepository _categoryRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete offer from category";

        public OfferCategoryDeleteAction(OfferCategoryRepository offerCategoryRepository, CategoryRepository categoryRepository,
            OfferRepository offerRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
            _categoryRepository = categoryRepository;
        }

        public void Call()
        {
            var doesContinue = true;
            var categoryList = _categoryRepository.GetAll();
            PrintHelpers.PrintCategories(categoryList);
            if (categoryList.Count == 0) return;

            Console.WriteLine("Enter index of category to delete offers from:");
            var category = ReadHelpers.TryGetListMember(categoryList, ref doesContinue);
            if (!doesContinue) return;

            var offersInside = _offerCategoryRepository.GetOfferList(category.Id, true).ToList();
            PrintHelpers.PrintOfferList(offersInside);
            if (offersInside.Count == 0) return;

            while (true)
            {
                Console.WriteLine($"Enter index of offer you want to delete from {category.Name}:");

                var offerIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, offersInside.Count) - 1;
                if (!doesContinue) return;

                var offer = offersInside.ElementAt(offerIndex);

                if (offer == null)
                {
                    MessageHelpers.Error("Offer is already deleted!");
                    continue;
                }

                _offerCategoryRepository.Delete(offer.Id, category.Id);

                MessageHelpers.Success("Deleted!");
                offersInside[offerIndex] = null;
            }
        }

    }
}