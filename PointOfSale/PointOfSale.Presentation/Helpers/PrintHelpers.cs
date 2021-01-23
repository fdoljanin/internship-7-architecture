using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Data.Entities.Models;

namespace PointOfSale.Presentation.Helpers
{
    public static class PrintHelpers
    {
        public static void PrintOffer(Offer offer)
        {
            Console.Write($"{offer.Type}\t\t{offer.Name}\t\t{offer.Price}\t\t");
            if (offer.Quantity != null) Console.Write(offer.Quantity);
            Console.WriteLine();
        }

        public static void PrintOfferList(ICollection<Offer> offers)
        {
            if (offers.Count == 0)
            {
                MessageHelpers.NotAvailable("No offers found!");
                return;
            }

            Console.WriteLine("TYPE\t\tNAME\t\tPRICE\t\tQUANTITY");
            for (var i = 0; i < offers.Count; ++i)
            {
                Console.Write($"{i+1}. ");
                PrintOffer(offers.ElementAt(i));
            }
        }

        public static void PrintPerson(Person person)
        {
            Console.Write($"{person.Pin}\t\t{person.FirstName} {person.LastName}\t\t\t");
            if (person is Employee employee) Console.Write($"{employee.WorkStart}\t\t{employee.WorkEnd}");
            Console.WriteLine();
        }
        public static void PrintPersonList<TPerson>(ICollection<TPerson> persons) where TPerson:Person
        {
            if (persons.Count == 0)
            {
                MessageHelpers.NotAvailable($"Nobody to show!");
                return;
            }

            Console.Write("PIN\t\tNAME \t\t\t");
            if (persons.First() is Employee) Console.Write("WORK START\t\tWORK END");
            Console.WriteLine();
            for (var i = 0; i < persons.Count; ++i)
            {
                Console.Write($"{i+1}. ");
                PrintPerson(persons.ElementAt(i));
            }
        }

        public static void PrintCategories(ICollection<Category> categories)
        {
            if (categories.Count == 0)
            {
                MessageHelpers.NotAvailable("No categories found!");
                return;
            }

            Console.WriteLine("CATEGORY NAME");
            for (var i = 0; i < categories.Count; ++i)
            {
                Console.WriteLine($"{i+1}. {categories.ElementAt(i).Name}");
            }
        }

        public static void PrintBill(Bill bill)
        {
            Console.WriteLine($"{bill.Type}\t\t{bill.TransactionDate}\t\t{bill.Cost}");
        }

        public static void PrintBills(ICollection<Bill> bills)
        {
            if (bills.Count == 0)
            {
                MessageHelpers.NotAvailable("No bills found!");
                return;
            }

            Console.WriteLine("TYPE\t\t\tDATE\t\t\t\tCOST");
            for (var i = 1; i <= bills.Count; ++i)
            {
                Console.Write($"{i}. ");
                PrintBill(bills.ElementAt(i-1));
            }
        }

        public static void PrintSubscription(SubscriptionBill subscriptionBill)
        {
            Console.WriteLine($"{subscriptionBill.Offer.Name}\t\t\t{subscriptionBill.Customer.Pin}\t\t\t{subscriptionBill.StartTime}");
        }

        public static void PrintSubscriptions(ICollection<SubscriptionBill> subscriptionBills)
        {
            if (subscriptionBills.Count == 0)
            {
                MessageHelpers.NotAvailable("No subscriptions found!");
                return;
            }

            Console.WriteLine("OFFER NAME\t\t\tCUSTOMER PIN\t\t\tSTART DATE");
            for (var i = 1; i <= subscriptionBills.Count; ++i)
            {
                Console.Write($"{i}. ");
                PrintSubscription(subscriptionBills.ElementAt(i - 1));
            }
        }

    }
}
