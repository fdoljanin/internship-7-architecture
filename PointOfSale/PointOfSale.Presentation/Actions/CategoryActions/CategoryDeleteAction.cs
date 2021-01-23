using System;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

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
            if (categoryList.Count == 0) return;

            Console.WriteLine("Enter index of category to delete:");
            var categoryToDelete = ReadHelpers.TryGetListMember(categoryList, ref doesContinue);
            if (!doesContinue) return;

            if (!ReadHelpers.Confirm($"Are you sure you want to delete category {categoryToDelete.Name}?"))
            {
                MessageHelpers.Success("Action stopped.");
                Console.ReadLine();
                return;
            }

            _categoryRepository.Delete(categoryToDelete.Id);
            MessageHelpers.Success("Category deleted!");
            Console.ReadLine();
        }
    }
}