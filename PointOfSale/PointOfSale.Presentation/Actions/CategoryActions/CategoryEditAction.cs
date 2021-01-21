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
        private readonly UniqueReadHelpers _uniqueReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Edit Category";

        public CategoryEditAction(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _uniqueReadHelper = new UniqueReadHelpers(categoryRepository);
        }

        public void Call()
        {
            var isNotBlank = true;
            var categoryList = _categoryRepository.GetAll();
            PrintHelpers.PrintCategories(categoryList);
            Console.WriteLine("Enter index of category to edit:");
            var categoryIndex = ReadHelpers.TryIntParse(ref isNotBlank, 1, categoryList.Count) -1 ;
            if (!isNotBlank) return;
            var categoryToEdit = categoryList.ElementAt(categoryIndex);
            
            Console.WriteLine($"Enter new category name, enter for default ({categoryToEdit.Name}):");
            var newName = _uniqueReadHelper.TryGetUniqueString(ref isNotBlank);
            if (!isNotBlank) return;

            _categoryRepository.Edit(categoryToEdit.Id,
                new Category()
                {
                    Name = newName
                }
                );
        }
    }
}