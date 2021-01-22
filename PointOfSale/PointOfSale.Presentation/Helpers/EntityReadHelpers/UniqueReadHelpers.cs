using System;
using PointOfSale.Domain.Repositories.Abstractions;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public static class UniqueReadHelpers
    {
        public static string TryGetUniqueString(IUniqueString repositoryToCheck, ref bool doesContinue)
        {
            while (true)
            {
                var inputString = ReadHelpers.TryGetInput(ref doesContinue);
                var unique = repositoryToCheck.IsStringUnique(inputString);
                if (unique || !doesContinue) return inputString;
                Console.WriteLine("It should be unique!");
            }
        }

        public static string TryGetUniquePin(IUniqueString repositoryToCheck, ref bool doesContinue)
        {
            while (true)
            {
                var pin = TryGetUniqueString(repositoryToCheck, ref doesContinue);
                if (ReadHelpers.IsPinValid(pin) || !doesContinue) return pin;
                Console.WriteLine("Pin should consist of digits!");
            }
        }
    }
}
