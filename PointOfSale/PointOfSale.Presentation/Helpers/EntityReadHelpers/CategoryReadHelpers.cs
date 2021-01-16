using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class CategoryReadHelpers
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryReadHelpers(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public string TryGetName(bool needUnique, ref bool doesContinue) //merge later
        {
            while (true)
            {
                doesContinue = ReadHelpers.DoesContinue(out var name);
                if (!doesContinue) return null;
                var unique = _categoryRepository.CheckUnique(name);
                if (unique == needUnique) return name;
                Console.WriteLine(needUnique ? "Name needs to be unique!" : "Category with that name does not exist!");
            }
        }
    }
}
