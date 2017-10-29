using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Models
{
    public class ProductConsumptionDeficientTrend
    {
        public DayOfWeek Day { get; set; }
        public float AvgConsumption { get; set; }
        [Range(0, 1)]
        public float AvgHitRate { get; set; }
        public ProductConsumptionDeficientTrend() { }
    }
}