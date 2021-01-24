using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Presentation.Extensions;

namespace PointOfSale.Presentation.Helpers
{
    public static class ReadHelpers
    {
        public static string TryGetInput(ref bool doesContinue)
        {
            var input = Console.ReadLine()?.Trim();
            doesContinue = input != "";
            return input;
        }

        public static int TryIntParse(ref bool doesContinue, int lowerBound = int.MinValue, int upperBound = int.MaxValue)
        {
            while (true)
            {
                var input = TryGetInput(ref doesContinue);
                if (!doesContinue) return default;

                if (input.Last() == '.') input = input.Remove(input.Length - 1); 
                var doesParse = int.TryParse(input, out var number);

                if (doesContinue && (!doesParse || number < lowerBound || number > upperBound))
                {
                    MessageHelpers.Error("Input not valid!");
                    continue;
                }
                return number;
            }
        }
        public static decimal TryDecimalParse(ref bool doesContinue, decimal lowerBound = decimal.MinValue)
        {
            while (true)
            {
                var input = TryGetInput(ref doesContinue);
                var doesParse = decimal.TryParse(input, out var number);
                if (doesContinue && (!doesParse || number < lowerBound))
                {
                    MessageHelpers.Error("Input not valid!");
                    continue;
                }
                return number;
            }
        }


        public static TEnum TryEnumParse<TEnum>(ref bool doesContinue)
        {
            while (true)
            {
                var input = TryGetInput(ref doesContinue);
                if (!doesContinue) return default;
                var doesParse = Enum.TryParse(typeof(TEnum), input.Capitalize(), out var result);
                if (doesContinue && (!doesParse || int.TryParse(input, out _)))
                {
                    MessageHelpers.Error("Input not valid!");
                    continue;
                }
                return (TEnum) result;
            }
        }

        public static bool Confirm(string message)
        {
            MessageHelpers.Confirm(message);
            var input = Console.ReadLine()?.Trim().ToLower();
            if (input == "yes") return true;
            if (input == "no") return false;
            MessageHelpers.Error("Input is not valid, choose yes/no!");
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

        public static (int start, int end) TryGetWorkingHours(int min, int max, ref bool doesContinue)
        {
            while (true)
            {
                var input = TryGetInput(ref doesContinue);
                if (!doesContinue) return (-1, -1);
                var hours = input.Split();
                if (hours.Length != 2)
                {
                    MessageHelpers.Error("Please enter in right format!");
                    continue;
                }

                var doesParse = int.TryParse(hours[0], out var workStart);
                doesParse &= int.TryParse(hours[1], out var workEnd);
                if (!doesParse)
                {
                    MessageHelpers.Error("Please enter a number!");
                    continue;
                }

                if (workStart < min || workEnd > max)
                {
                    MessageHelpers.Error("Hours not valid!");
                    continue;
                }

                if (workEnd < workStart)
                {
                    MessageHelpers.Error("End should be after start!");
                    continue;
                }

                return (workStart, workEnd);
            }
        }

        public static (DateTime start, DateTime end) GetDateRange(ref bool doesContinue)
        {
            while (true)
            {
                var input = TryGetInput(ref doesContinue);
                if (!doesContinue) return (default, default);
                var dates = input.Split();

                if (dates.Length == 2)
                {
                    var doesParse = DateTime.TryParse(dates[0], out var start);
                    doesParse &= DateTime.TryParse(dates[1], out var end);

                    if (doesParse && start < end) return (start, end);
                    MessageHelpers.Error("Enter valid dates!");
                    continue;
                }

                if (dates.Length == 1)
                {
                    var doesParse = DateTime.TryParse(dates[0], out var start);

                    if (doesParse && start < DateTime.Now) return (start, DateTime.Now);
                    MessageHelpers.Error("Enter valid date!");
                }

                Console.WriteLine("Input not valid!");
            }
        }

        public static (int lowerBound, int upperBound) TryGetLessOrMore(ref bool doesContinue)
        {
            while (true)
            {
                var input = TryGetInput(ref doesContinue);
                if (!doesContinue) return (default, default);

                var doesParse = int.TryParse(input.Substring(1), out var number);

                if (!doesParse)
                {
                    MessageHelpers.Error("Enter a number!");
                    continue;
                }
                if (input[0] == '>') return (number, int.MaxValue);
                if (input[0] == '<') return (int.MinValue, number);

                MessageHelpers.Error("Input not valid!");
            }
        }

        public static TEntity TryGetListMember<TEntity>(ICollection<TEntity> entityList, ref bool doesContinue)
        {
            var entityIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, entityList.Count) - 1;
            return doesContinue ? entityList.ElementAt(entityIndex) : default;
        }
    }
}
