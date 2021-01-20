using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.CategoryActions
{
    public class CategoryAddAction : IAction
    {
        private readonly CategoryRepository _categoryRepository;
        private readonly CategoryReadHelpers _categoryReadHelper;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add Category";

        public CategoryAddAction(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _categoryReadHelper = new CategoryReadHelpers(categoryRepository);
        }

        public void Call()
        {
            bool doesContinue = true;

            Console.WriteLine("Enter category name:");
            var name = _categoryReadHelper.TryGetName( true, ref doesContinue);
            if (!doesContinue) return;
            _categoryRepository.Add(
                new Category()
                {
                    Name = name
                }
                );
        }
    }
}