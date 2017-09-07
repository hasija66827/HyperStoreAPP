using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Models
{
    public class TProductConsumptionDeficientTrend
    {
        public DayOfWeek Day { get; set; }
        public decimal AvgConsumption { get; set; }
        [Range(0, 1)]
        public float AvgHitRate { get; set; }
        public string FormattedDay { get { return this.Day.ToString().Substring(0,3); } }
        public TProductConsumptionDeficientTrend() { }
    }
}