using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Data.Entities.Models
{
    public class OfferCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }
}
