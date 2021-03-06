﻿using System.Collections.Generic;
using PointOfSale.Data.Enums;

namespace PointOfSale.Data.Entities.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public OfferType Type { get; set; } 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public bool IsActive { get; set; }
        public ICollection<OfferCategory> OfferCategories { get; set; }
        public ICollection<ArticleBill> ArticleBills { get; set; }
        public ICollection<ServiceBill> ServiceBills { get; set; }
        public ICollection<SubscriptionBill> SubscriptionBills { get; set; }
    }
}
