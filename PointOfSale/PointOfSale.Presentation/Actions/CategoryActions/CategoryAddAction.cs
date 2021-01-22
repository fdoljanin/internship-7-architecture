using System;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class CategoryAddAction : IAction
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly UniqueReadHelpers _uniqueReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add Category";

        public CategoryAddAction(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _uniqueReadHelper = new UniqueReadHelpers(categoryRepository);
        }

        public void Call()
        {
            bool doesContinue = true;
            var category = new Category();
            
            Console.WriteLine("Enter category name:");
            category.Name = _uniqueReadHelper.TryGetUniqueString(ref doesContinue);
            if (!doesContinue) return;

            _categoryRepository.Add(category);

            Console.WriteLine("Category added!");
            Console.ReadLine();
        }
    }
}