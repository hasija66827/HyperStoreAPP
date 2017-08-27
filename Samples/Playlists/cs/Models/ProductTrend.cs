using System;
using System.Collections.Generic;
using System.Linq;
namespace HyperStoreService.Models
{
    public class ProductTrend
    {
        public DayOfWeek Day { get; set; }
        public float Quantity { get; set; }
        public ProductTrend(DayOfWeek day, float quantity)
        {
            this.Day = day;
            this.Quantity = quantity;
        }
    }
}