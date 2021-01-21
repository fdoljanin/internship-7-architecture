﻿using PointOfSale.Data.Entities.Models;

namespace PointOfSale.Domain.Models
{
    public class TopSellingOffer
    {
        public Offer Offer { get; set; }
        public int Quantity { get; set; }
    }
}
