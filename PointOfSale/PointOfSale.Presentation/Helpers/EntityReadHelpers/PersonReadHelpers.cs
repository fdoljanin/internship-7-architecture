using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using PointOfSale.Domain.Repositories;
using PointOfSale.Domain.Repositories.Abstractions;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class PersonReadHelpers
    {
        private readonly IPersonRepository _personRepository;

        public PersonReadHelpers(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        
        public string TryGetPin( bool needExisting,
            ref bool doesContinue)
        {
            while (true)
            {
                doesContinue = ReadHelpers.DoesContinue(out var pin);
                if (!ReadHelpers.IsPinValid(pin))
                {
                    Console.WriteLine("Pin not valid!");
                    continue;
                }

                if (_personRepository.DoesPinExist(pin) == needExisting) return pin;

                if (!needExisting)
                {
                    Console.WriteLine("Person with that PIN already exists!");
                    doesContinue = false;
                    return pin;
                }

                Console.WriteLine("Person with that PIN does not exist!");
            }
        }
    }
}
