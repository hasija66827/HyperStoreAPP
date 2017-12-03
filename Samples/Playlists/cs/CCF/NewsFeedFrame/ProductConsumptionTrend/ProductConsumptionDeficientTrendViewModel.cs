using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public sealed class ProductConsumptionViewModel
    {
        public DayOfWeek DayOfWeek { get; set; }
        public double EstConsumption { get; set; }
        public int RoundedEstConsumption { get { return (int)(this.EstConsumption); } }
        public string FormattedDay { get { return this.DayOfWeek.ToString().Substring(0, 3); } }
    }
}
