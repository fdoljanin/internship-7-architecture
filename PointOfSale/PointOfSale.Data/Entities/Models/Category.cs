using System.Collections.Generic;

namespace PointOfSale.Data.Entities.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<OfferCategory> OfferCategories { get; set; }

    }
}
