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
    public class OfferCategoryAddAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly OfferCategoryHelpers _offerCategoryHelper;
        private readonly CategoryReadHelpers _categoryReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add offer in category";

        public OfferCategoryAddAction(OfferCategoryRepository offerCategoryRepository, CategoryRepository categoryRepository, 
            OfferRepository offerRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
            _categoryRepository = categoryRepository;
            _offerCategoryHelper = new OfferCategoryHelpers(offerRepository);
            _categoryReadHelper = new CategoryReadHelpers(categoryRepository);
        }

        public void Call()
        {
            var doesContinue= true;
            var categoryList = _categoryRepository.GetAll();
            PrintHelpers.PrintCategories(categoryList);
            Console.WriteLine("Enter index of category to insert elements into:");
            var categoryIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, categoryList.Count) - 1;
            if (!doesContinue) return;
            var category = categoryList.ElementAt(categoryIndex);

            var offersOutside = _offerCategoryRepository.GetOfferList(category.Id, false).ToList();
            
            PrintHelpers.PrintOfferList(offersOutside);
            while (true)
            {
                Console.WriteLine($"Enter index of offer you want to add into {category.Name}:");

                var offerIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, offersOutside.Count) - 1;
                if (!doesContinue) return;

                var offer = offersOutside.ElementAt(offerIndex);

                if (offer == null)
                {
                    Console.WriteLine("Offer is already there!");
                    continue;
                }

                _offerCategoryRepository.Add(
                    new OfferCategory()
                    {
                        CategoryId = category.Id,
                        OfferId = offer.Id
                    }
                    );

                offersOutside[offerIndex] = null;
            }
        }
    }
}
