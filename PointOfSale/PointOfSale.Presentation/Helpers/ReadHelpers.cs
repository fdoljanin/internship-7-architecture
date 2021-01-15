using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Presentation.Extensions;

namespace PointOfSale.Presentation.Helpers
{
    public static class ReadHelpers
    {
        public static bool DoesContinue(string message, out string input)
        {
            Console.WriteLine(message);
            input = Console.ReadLine().Trim();
            return input != "";
        }

        public static bool TryIntParse(string message, out int number, int lowerBound = int.MinValue)
        {
            while (true)
            {
                var doesContinue = DoesContinue(message, out var input);
                var doesParse = int.TryParse(input, out var intInput);
                number = intInput;
                if (doesContinue && (!doesParse || number < lowerBound))
                {
                    Console.WriteLine("Input not valid!");
                    continue;
                }
                return doesContinue;
            }
        }
        public static bool TryDecimalParse(string message, out decimal number, decimal lowerBound = decimal.MinValue)
        {
            while (true)
            {
                var doesContinue = DoesContinue(message, out var input);
                var doesParse = decimal.TryParse(input, out var decimalInput);
                number = decimalInput;
                if (doesContinue && (!doesParse || number < lowerBound))
                {
                    Console.WriteLine("Input not valid!");
                    continue;
                }
                return doesContinue;
            }
        }

        public static bool TryEnumParse<TEnum>(string message, out TEnum inputEnum) where TEnum:Enum
        {
            while (true)
            {
                var doesContinue = DoesContinue(message, out var input);
                var doesParse = Enum.TryParse(typeof(TEnum), input.Capitalize(), out var result);
                if (doesContinue && (!doesParse || int.TryParse(input, out _)))
                {
                    Console.WriteLine("Input not valid!");
                    continue;
                }
                inputEnum = doesContinue ? (TEnum) result : default;
                return doesContinue;
            }
        }

        public static bool Confirm(string message)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine().Trim();
            if (input == "yes") return true;
            if (input == "no") return false;
            Console.WriteLine("Input is not valid, choose yes/no!");
            return Confirm(message);
        }
    }
}
