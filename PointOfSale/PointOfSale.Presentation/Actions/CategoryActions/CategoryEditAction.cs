using System;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class CategoryEditAction : IAction
    {
        private readonly CategoryRepository _categoryRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit Category";

        public CategoryEditAction(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Call()
        {
            var isNotBlank = true;
            var categoryList = _categoryRepository.GetAll();
            PrintHelpers.PrintCategories(categoryList);
            if (categoryList.Count == 0) return;

            Console.WriteLine("Enter index of category to edit:");
            var categoryToEdit = ReadHelpers.TryGetListMember(categoryList, ref isNotBlank);
            if (!isNotBlank) return;

            Console.WriteLine($"Enter new category name, enter for default ({categoryToEdit.Name}):");
            var newName = UniqueReadHelpers.TryGetUniqueString(_categoryRepository, ref isNotBlank);
            categoryToEdit.Name = isNotBlank ? newName : categoryToEdit.Name;

            _categoryRepository.Edit(categoryToEdit.Id, categoryToEdit);

            MessageHelpers.Success("Category edited!");
            Console.ReadLine();
        }
    }
}