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
        private readonly OfferRepository _offerRepository;
        public string Label { get; set; } = "Add offer in category";

        public OfferCategoryAddAction(OfferCategoryRepository offerCategoryRepository, CategoryRepository categoryRepository, 
            OfferRepository offerRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
            _categoryRepository = categoryRepository;
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            PrintHelpers.PrintCategories(_categoryRepository.GetAll());
            var message = "Enter name of category you want to add offer into:";
            var doesContinue = CategoryReadHelpers.TryGetName(message, _categoryRepository, false, out var categoryName);
            if (!doesContinue) return;

            while (true)
            {
                var category = _categoryRepository.FindFullByName(categoryName);
                message = $"Enter name of offer you want to add into {category.Name}:";
                doesContinue = OfferCategoryHelpers.TryGetOfferNameIfUnique
                    (message, _offerRepository, false, category.OfferCategories, out var offerId);
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
