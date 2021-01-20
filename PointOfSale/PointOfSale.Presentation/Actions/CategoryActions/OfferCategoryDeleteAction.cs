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
            PrintHelpers.PrintCategories(_categoryRepository.GetAll());
            Console.WriteLine("Enter name of category you want to delete from:");
            var categoryName = _categoryReadHelper.TryGetName(false, ref doesContinue);
            if (!doesContinue) return;

            var categoryToPrint = _categoryRepository.FindFullByName(categoryName);
            foreach (var offerId in 
                categoryToPrint.OfferCategories.Select(oc => oc.OfferId))
            {
                PrintHelpers.PrintOffer(_offerRepository.Find(offerId));
            }

            Console.WriteLine($"Enter name of offer you want to delete from {categoryToPrint.Name}:");

            var category = _categoryRepository.FindFullByName(categoryName);
            while (true)
            {
                var offerId = _offerCategoryHelper.TryGetOfferId
                    (true, category.OfferCategories, ref doesContinue);
                if (!doesContinue) return;

                _offerCategoryRepository.Delete(offerId, category.Id);

                var del = category.OfferCategories.First(oc => oc.OfferId == offerId && oc.CategoryId == category.Id); //weird but works, fix later
                category.OfferCategories.Remove(del);
            }
        }
    }
}