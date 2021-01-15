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
        public string Label { get; set; } = "Add Category";

        public CategoryAddAction(CategoryRepository CategoryRepository)
        {
            _categoryRepository = CategoryRepository;
        }

        public void Call()
        {
            var message = "Enter category name:";
            var doesContinue = CategoryReadHelpers.TryGetName(message, _categoryRepository, true, out var name);
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