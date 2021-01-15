using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public static class CategoryReadHelpers
    {
        public static bool TryGetName(string message, CategoryRepository repository, bool needUnique, out string name)
        {
            while (true) //I will merge this with offerhelper
            {
                var doesContinue = ReadHelpers.DoesContinue(message, out var inputName);
                var unique = repository.CheckUnique(inputName);
                name = inputName;
                if (!doesContinue) return false;
                if (unique == needUnique) return true;
                Console.WriteLine(needUnique ? "Name needs to be unique!" : "Category with that name does not exist!");
            }
        }
    }
}
