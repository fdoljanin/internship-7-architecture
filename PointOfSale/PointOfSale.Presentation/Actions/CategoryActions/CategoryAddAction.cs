using System;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class CategoryAddAction : IAction
    {
        private readonly CategoryRepository _categoryRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add Category";

        public CategoryAddAction(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Call()
        {
            var doesContinue = true;
            var category = new Category();
            
            Console.WriteLine("Enter category name:");
            category.Name = UniqueReadHelpers.TryGetUniqueString(_categoryRepository, ref doesContinue);
            if (!doesContinue) return;

            _categoryRepository.Add(category);

            MessageHelpers.Success("Category added!");
            Console.ReadLine();
        }
    }
}