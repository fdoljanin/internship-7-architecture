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

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class OfferCategoryDeleteAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly OfferCategoryHelpers _offerCategoryHelper;
        private readonly CategoryReadHelpers _categoryReadHelper;
        private readonly OfferRepository _offerRepository;

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete offer from category";

        public OfferCategoryDeleteAction(OfferCategoryRepository offerCategoryRepository, CategoryRepository categoryRepository,
            OfferRepository offerRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
            _categoryRepository = categoryRepository;
            _offerRepository = offerRepository;
            _offerCategoryHelper = new OfferCategoryHelpers(offerRepository);
            _categoryReadHelper = new CategoryReadHelpers(categoryRepository);
        }

        public void Call()
        {
            var doesContinue = true;
            var categoryList = _categoryRepository.GetAll();
            PrintHelpers.PrintCategories(categoryList);
            Console.WriteLine("Enter index of category to delete elements from:");
            var categoryIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, categoryList.Count) - 1;
            if (!doesContinue) return;
            var category = categoryList.ElementAt(categoryIndex);

            var offersInside = _offerCategoryRepository.GetOfferList(category.Id, true).ToList();

            PrintHelpers.PrintOfferList(offersInside);
            while (true)
            {
                Console.WriteLine($"Enter index of offer you want to delete from {category.Name}:");

                var offerIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, offersInside.Count) - 1;
                if (!doesContinue) return;

                var offer = offersInside.ElementAt(offerIndex);

                if (offer == null)
                {
                    Console.WriteLine("Offer is already there!");
                    continue;
                }

                _offerCategoryRepository.Delete(offer.Id, category.Id);

                offersInside[offerIndex] = null;
            }
        }

    }
}