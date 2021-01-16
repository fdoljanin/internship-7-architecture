﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;

namespace PointOfSale.Presentation.Helpers
{
    public static class PrintHelpers
    {
        public static void PrintOffer(Offer offer)
        {
            Console.Write($"{offer.Type}\t\t{offer.Name}\t\t{offer.Price}\t\t");
            if (offer.Type != OfferType.Service) Console.Write(offer.Quantity);
            Console.WriteLine();
        }

        public static void PrintOfferList(ICollection<Offer> offers)
        {
            Console.WriteLine("TYPE\t\tNAME\t\tPRICE\t\tQUANTITY");
            foreach (var offer in offers)
                PrintOffer(offer);
        }

        public static void PrintPerson(Person person)
        {
            Console.Write($"{person.Pin}\t\t{person.FirstName} {person.LastName}\t\t\t");
            if (person is Employee employee) Console.Write($"{employee.WorkStart}\t\t{employee.WorkEnd}");
            Console.WriteLine();
        }
        public static void PrintPersonList<TPerson>(ICollection<TPerson> persons) where TPerson:Person
        {
            Console.Write("PIN\t\tNAME \t\t\t");
            if (persons.First() is Employee) Console.Write("WORK START\t\tWORK END");
            Console.WriteLine();
            foreach (var person in persons)
            {
                PrintPerson(person);
            }
        }

        public static void PrintCategories(ICollection<Category> categories)
        {
            foreach (var category in categories)
            {
                Console.WriteLine(category.Name);
            }
        }
    }
}
