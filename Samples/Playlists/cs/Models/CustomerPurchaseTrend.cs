using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class TCustomerPurchaseTrend
    {
        public int TotalQuantityPurchased { get; set; }
        public TProduct Product { get; set; }
        public decimal NetValue { get; set; }
        public TCustomerPurchaseTrend() { }
    }
}