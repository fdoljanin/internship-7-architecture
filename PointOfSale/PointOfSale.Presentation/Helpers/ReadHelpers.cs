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
        public static bool DoesContinue(out string input)
        {
            input = Console.ReadLine().Trim();
            return input != "";
        }

        public static int TryIntParse(ref bool doesContinue, int lowerBound = int.MinValue, int upperBound = int.MaxValue)
        {
            while (true)
            {
                doesContinue = DoesContinue(out var input);
                var doesParse = int.TryParse(input, out var number);
                if (doesContinue && (!doesParse || number < lowerBound || number > upperBound))
                {
                    Console.WriteLine("Input not valid!");
                    continue;
                }
                return number;
            }
        }
        public static decimal TryDecimalParse(ref bool doesContinue, decimal lowerBound = decimal.MinValue)
        {
            while (true)
            {
                doesContinue = DoesContinue(out var input);
                var doesParse = decimal.TryParse(input, out var number);
                if (doesContinue && (!doesParse || number < lowerBound))
                {
                    Console.WriteLine("Input not valid!");
                    continue;
                }
                return number;
            }
        }


        public static TEnum TryEnumParse<TEnum>(ref bool doesContinue)
        {
            while (true)
            {
                doesContinue = DoesContinue(out var input);
                var doesParse = Enum.TryParse(typeof(TEnum), input.Capitalize(), out var result);
                if (doesContinue && (!doesParse || int.TryParse(input, out _)))
                {
                    Console.WriteLine("Input not valid!");
                    continue;
                }
                return (TEnum) result;
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

        public static bool IsPinValid(string pin)
        {
            foreach (var digit in pin)
            {
                if (!"0123456789".Contains(digit))
                    return false;
            }
            return true;
        }

        public static (int start, int end) TryGetWorkingHours(int min, int max, ref bool continues)
        {
            while (true)
            {
                continues = DoesContinue(out var input);
                if (!continues) return (-1, -1);
                var hours = input.Split();
                if (hours.Length != 2)
                {
                    Console.WriteLine("Please enter in right format!");
                    continue;
                }

                var doesParse = int.TryParse(hours[0], out var workStart);
                doesParse = int.TryParse(hours[1], out var workEnd);
                if (!doesParse)
                {
                    Console.WriteLine("Please enter a number!");
                    continue;
                }

                if (workStart < min || workEnd > max)
                {
                    Console.WriteLine("Hours not valid!");
                    continue;
                }

                if (workEnd < workStart)
                {
                    Console.WriteLine("End should be after start!");
                    continue;
                }

                return (workStart, workEnd);

            }
        }
    }
}
