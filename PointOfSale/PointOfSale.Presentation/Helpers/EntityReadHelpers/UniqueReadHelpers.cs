using System;
using PointOfSale.Domain.Repositories.Abstractions;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class UniqueReadHelpers
    {
        private readonly IUniqueString _repositoryToCheck;

        public UniqueReadHelpers(IUniqueString repositoryToCheck)
        {
            _repositoryToCheck = repositoryToCheck;
        }
        public string TryGetUniqueString(ref bool doesContinue)
        {
            while (true)
            {
                var inputString = ReadHelpers.TryGetInput(ref doesContinue);
                var unique = _repositoryToCheck.IsStringUnique(inputString);
                if (unique || !doesContinue) return inputString;
                Console.WriteLine("It should be unique!");
            }
        }

        public string TryGetUniquePin(ref bool doesContinue)
        {
            while (true)
            {
                var pin = TryGetUniqueString(ref doesContinue);
                if (ReadHelpers.IsPinValid(pin) || !doesContinue) return pin;
                Console.WriteLine("Pin should consist of digits!");
            }
        }
    }
}
