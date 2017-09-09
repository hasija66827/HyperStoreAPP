using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Models
{
    public class TProductConsumptionDeficientTrend
    {
        public DayOfWeek Day { get; set; }
        public float AvgConsumption { get; set; }
        [Range(0, 1)]
        public float AvgHitRate { get; set; }
        public TProductConsumptionDeficientTrend() { }
    }

    public sealed class ProductConsumptionDeficientTrendViewModel : TProductConsumptionDeficientTrend
    {
        public double RoundedAvgConsumption { get { return Math.Round(this.AvgConsumption, 2); } }
        public string FormattedDay { get { return this.Day.ToString().Substring(0, 3); } }
        public double SecondSeriesHits { get { return Math.Round(this.AvgConsumption * this.AvgHitRate, 2); } }

        public ProductConsumptionDeficientTrendViewModel(TProductConsumptionDeficientTrend parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}