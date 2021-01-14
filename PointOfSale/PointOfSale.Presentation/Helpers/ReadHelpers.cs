using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale.Presentation.Helpers
{
    public static class ReadHelpers
    {
        public static bool IsInputBlank(string message, out string input)
        {
            Console.WriteLine(message);
            input = Console.ReadLine().Trim();
            return input != "";
        }
    }
}
