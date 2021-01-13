using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Data.Entities.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<OfferCategory> OfferCategories { get; set; }

    }
}
