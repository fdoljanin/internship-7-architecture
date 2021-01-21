using System.Collections.Generic;

namespace PointOfSale.Data.Entities.Models
{
    public class Employee:Person
    {
        public int WorkStart { get; set; }
        public int WorkEnd { get; set; }
        public ICollection<ServiceBill> ServiceBills { get; set; }
    }
}
