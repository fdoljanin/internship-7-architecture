using System;
using System.Collections.Generic;
using PointOfSale.Data.Enums;

namespace PointOfSale.Data.Entities.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public BillType Type { get; set; }
        public decimal Cost { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool Cancelled { get; set; }
        public ICollection<ArticleBill> ArticleBills { get; set; }
        public ICollection<ServiceBill> ServiceBills { get; set; }
        public ICollection<SubscriptionBill> SubscriptionBills { get; set; }
    }
}
