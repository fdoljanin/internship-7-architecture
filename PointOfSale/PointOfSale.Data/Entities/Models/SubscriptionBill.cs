using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Data.Entities.Models
{
    public class SubscriptionBill
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime StartTime { get; set; }
        public int? BillId { get; set; }
        public Bill? Bill { get; set; }

    }
}
