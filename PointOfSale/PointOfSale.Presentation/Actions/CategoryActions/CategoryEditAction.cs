using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class CategoryEditAction : IAction
    {
        private readonly CategoryRepository _categoryRepository;
        public string Label { get; set; } = "Edit Category";

        public CategoryEditAction(CategoryRepository CategoryRepository)
        {
            _categoryRepository = CategoryRepository;
        }

        public void Call()
        {
            PrintHelpers.PrintCategories(_categoryRepository.GetAll());
            var message = "Enter name of category to edit:";
            var doesContinue = CategoryReadHelpers.TryGetName(message, _categoryRepository, false, out var name);
            if (!doesContinue) return;
            var categoryToEdit = _categoryRepository.FindByName(name);
            
            message = $"Enter new category name, enter for default ({categoryToEdit.Name}):";
            var isNotDefault = CategoryReadHelpers.TryGetName(message, _categoryRepository, true, out var newName);
            if (!isNotDefault) return;

            _categoryRepository.Edit(categoryToEdit.Name,
                new Category()
                {
                    Name = newName
                }
                );
        }
    }
}