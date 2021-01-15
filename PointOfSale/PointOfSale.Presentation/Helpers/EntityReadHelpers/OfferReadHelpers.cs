using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Helpers;
using Console = System.Console;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public static class OfferReadHelpers
    {
        public static bool TryGetName(string message, OfferRepository repository, bool needUnique, out string name)
        {
            while (true)
            {
                var doesContinue = ReadHelpers.DoesContinue(message, out var inputName);
                var unique = repository.CheckUnique(inputName);
                name = inputName;
                if (!doesContinue) return false;
                if (unique == needUnique) return true;
                Console.WriteLine(needUnique ? "Name needs to be unique!" : "Product with that name does not exist!");
            }
        }
    }
}
