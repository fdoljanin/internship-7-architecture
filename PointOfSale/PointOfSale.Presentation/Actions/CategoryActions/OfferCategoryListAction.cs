using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class OfferCategoryListAction:IAction
    {
        private readonly OfferCategoryRepository _offerCategoryRepository;
        private readonly CategoryRepository _categoryRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "List offers per category";
        public OfferCategoryListAction(OfferCategoryRepository offerCategoryRepository, CategoryRepository categoryRepository)
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

            while (true)
            {
                Console.WriteLine("Enter index of category to show offers from:");
                var category = ReadHelpers.TryGetListMember(categoryList, ref doesContinue);
                if (!doesContinue) return;

                var offerList = _offerCategoryRepository.GetOfferList(category.Id, true).ToList();
                PrintHelpers.PrintOfferList(offerList);
            }
        }
    }
}
