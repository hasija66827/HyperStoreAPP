using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class CustomerPurchaseTrend
    {
        public int TotalQuantityPurchased { get; set; }
        public Product Product { get; set; }
        public decimal NetValue { get; set; }
        public CustomerPurchaseTrend() { }
    }
}