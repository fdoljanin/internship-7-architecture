using System;
using System.Collections.Generic;
using System.Text;
using PointOfSale.Data.Enums;

namespace PointOfSale.Data.Entities.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public OfferType Type { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public bool IsActive { get; set; }
        public ICollection<OfferCategory> OfferCategories { get; set; }
    }
}
