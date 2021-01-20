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
        private readonly CategoryReadHelpers _categoryReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit Category";

        public CategoryEditAction(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _categoryReadHelper = new CategoryReadHelpers(categoryRepository);
        }

        public void Call()
        {
            var isNotBlank = true;
            PrintHelpers.PrintCategories(_categoryRepository.GetAll());
            Console.WriteLine("Enter name of category to edit:");
            var name = _categoryReadHelper.TryGetName(false, ref isNotBlank);
            if (!isNotBlank) return;
            var categoryToEdit = _categoryRepository.FindByName(name);
            
            Console.WriteLine($"Enter new category name, enter for default ({categoryToEdit.Name}):");
            var newName = _categoryReadHelper.TryGetName(true, ref isNotBlank);
            if (!isNotBlank) return;

            _categoryRepository.Edit(categoryToEdit.Name,
                new Category()
                {
                    Name = newName
                }
                );
        }
    }
}