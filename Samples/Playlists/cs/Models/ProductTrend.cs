using System;
using System.Collections.Generic;
using System.Linq;
namespace Models
{
    public class TProductConsumptionTrend
    {
        public DayOfWeek Day { get; set; }
        public float Quantity { get; set; }
        public TProductConsumptionTrend(DayOfWeek day, float quantity)
        {
            this.Day = day;
            this.Quantity = quantity;
        }
    }
}