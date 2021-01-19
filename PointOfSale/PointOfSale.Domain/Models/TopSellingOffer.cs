using System;
using System.Collections.Generic;
using System.Text;
using PointOfSale.Data.Entities.Models;

namespace PointOfSale.Domain.Models
{
    public class TopSellingOffer
    {
        public Offer Offer { get; set; }
        public int Quantity { get; set; }
    }
}
