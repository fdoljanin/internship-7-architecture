using System;

namespace PointOfSale.Data.Entities.Models
{
    public class ServiceBill
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int BillId { get; set; }
        public Bill Bill { get; set; }


    }
}
