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
    }
}