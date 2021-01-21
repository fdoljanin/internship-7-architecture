using System.Collections.Generic;

namespace PointOfSale.Data.Entities.Models
{
    public class Customer:Person
    {
        public ICollection<SubscriptionBill> SubscriptionBills { get; set; }
    }
}
