using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public sealed class ProductConsumptionDeficientTrendViewModel : ProductConsumptionDeficientTrend
    {
        public double RoundedAvgConsumption { get { return Math.Round(this.AvgConsumption, 2); } }
        public string FormattedDay { get { return this.Day.ToString().Substring(0, 3); } }
        public double SecondSeriesHits { get { return Math.Round(this.AvgConsumption * this.AvgHitRate, 2); } }

        public ProductConsumptionDeficientTrendViewModel(ProductConsumptionDeficientTrend parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
