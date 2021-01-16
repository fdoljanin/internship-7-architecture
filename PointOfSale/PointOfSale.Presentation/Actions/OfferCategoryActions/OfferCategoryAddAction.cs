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

namespace PointOfSale.Presentation.Actions.OfferCategoryActions
{
    public class OfferCategoryAddAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly OfferCategoryHelpers _offerCategoryHelper;
        private readonly CategoryReadHelpers _categoryReadHelper;
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
            var doesContinue = true;
            PrintHelpers.PrintCategories(_categoryRepository.GetAll());
            Console.WriteLine("Enter name of category you want to add offer into:");
            var categoryName = _categoryReadHelper.TryGetName(false, ref doesContinue);
            if (!doesContinue) return;

            //maybe show list of offers that aren't in?

            while (true)
            {
                var category = _categoryRepository.FindFullByName(categoryName);
                Console.WriteLine($"Enter name of offer you want to add into {category.Name}:");
                var offerId = _offerCategoryHelper.TryGetOfferId
                    (false, category.OfferCategories, ref doesContinue);
                if (!doesContinue) return;

                _offerCategoryRepository.Add(
                    new OfferCategory()
                    {
                        CategoryId = category.Id,
                        OfferId = offerId
                    }
                    );
            }
        }
    }
}
