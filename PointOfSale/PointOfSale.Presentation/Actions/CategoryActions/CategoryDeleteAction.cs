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
        private readonly CategoryReadHelpers _categoryReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Delete Category";

        public CategoryDeleteAction(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _categoryReadHelper = new CategoryReadHelpers(categoryRepository);
        }

        public void Call()
        {
            var doesContinue = true;
            PrintHelpers.PrintCategories(_categoryRepository.GetAll());
            Console.WriteLine("Enter name of category you want to delete:");
            var name = _categoryReadHelper.TryGetName(false, ref doesContinue);
            if (!doesContinue) return;
            if (!ReadHelpers.Confirm($"Are you sure you want to delete category {name}?")) return;
            _categoryRepository.Delete(name);
        }
    }
}