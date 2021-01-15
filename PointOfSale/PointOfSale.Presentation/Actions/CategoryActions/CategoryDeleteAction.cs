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
        public string Label { get; set; } = "Delete Category";

        public CategoryDeleteAction(CategoryRepository CategoryRepository)
        {
            _categoryRepository = CategoryRepository;
        }

        public void Call()
        {
            PrintHelpers.PrintCategories(_categoryRepository.GetAll());
            var message = "Enter name of category you want to delete:";
            var doesContinue = CategoryReadHelpers.TryGetName(message, _categoryRepository, false, out var name);
            if (!doesContinue) return;
            if (!ReadHelpers.Confirm($"Are you sure you want to delete category {name}?")) return;
            _categoryRepository.Delete(name);
        }
    }
}