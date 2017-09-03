using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Windows.Globalization.DateTimeFormatting;
using System.Reflection;

namespace SDKTemplate
{
    public class RecommendedProductViewModel : TRecommendedProduct
    {
        public string FormattedLastOrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this.LastOrderDate);
            }
        }
        public RecommendedProductViewModel(TRecommendedProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
