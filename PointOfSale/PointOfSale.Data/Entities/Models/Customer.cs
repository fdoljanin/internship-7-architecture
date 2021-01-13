using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Data.Entities.Models
{
    public class Customer:Person
    {
        public ICollection<SubscriptionBill> SubscriptionBills { get; set; }
    }
}
