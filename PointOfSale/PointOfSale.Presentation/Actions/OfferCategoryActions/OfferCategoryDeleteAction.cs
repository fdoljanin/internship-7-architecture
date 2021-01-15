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
    public class OfferCategoryDeleteAction : IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly OfferRepository _offerRepository;
        public string Label { get; set; } = "Delete offer from category";

        public OfferCategoryDeleteAction(OfferCategoryRepository offerCategoryRepository, CategoryRepository categoryRepository, 
            OfferRepository offerRepository)
        {
            _offerCategoryRepository = offerCategoryRepository;
            _categoryRepository = categoryRepository;
            _offerRepository = offerRepository;
        }

        public void Call()
        {
            PrintHelpers.PrintCategories(_categoryRepository.GetAll());
            var message = "Enter name of category you want to delete from:";
            var doesContinue = CategoryReadHelpers.TryGetName(message, _categoryRepository, false, out var categoryName);
            if (!doesContinue) return;

            var categoryPrint = _categoryRepository.FindFullByName(categoryName);
            foreach (var offerId in categoryPrint.OfferCategories.Select(oc => oc.OfferId))
            {
                PrintHelpers.PrintOffer(_offerRepository.Find(offerId));
            }
            message = $"Enter name of offer you want to delete from {categoryPrint.Name}:";

            while (true)
            {
                var category = _categoryRepository.FindFullByName(categoryName);
                Console.WriteLine(category.OfferCategories.Count);
                doesContinue = OfferCategoryHelpers.TryGetOfferNameIfUnique
                    (message, _offerRepository, true, category.OfferCategories, out var offerId);
                if (!doesContinue) return;

                _offerCategoryRepository.Delete(offerId, category.Id);
            }
        }
    }
}