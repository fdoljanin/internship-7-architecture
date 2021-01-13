using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSale.Data.Entities.Models
{
    public class Employee:Person
    {
        public int WorkStart { get; set; }
        public int WorkEnd { get; set; }
        public ICollection<ServiceBill> ServiceBills { get; set; }
    }
}
