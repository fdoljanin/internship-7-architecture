using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class CategoryDeleteAction : IAction
    {
        private readonly CategoryRepository _categoryRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete Category";

        public CategoryDeleteAction(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Call()
        {
            var doesContinue = true;
            var categoryList = _categoryRepository.GetAll();
            PrintHelpers.PrintCategories(categoryList);

            Console.WriteLine("Enter index of category to delete:");
            var categoryIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, categoryList.Count) - 1;
            if (!doesContinue) return;

            var categoryToDelete = categoryList.ElementAt(categoryIndex);
            if (!ReadHelpers.Confirm($"Are you sure you want to delete category {categoryToDelete.Name}?")) return;
            _categoryRepository.Delete(categoryToDelete.Id);
        }
    }
}