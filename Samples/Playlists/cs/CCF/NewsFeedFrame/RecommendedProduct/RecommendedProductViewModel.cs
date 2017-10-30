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
    public class RecommendedProductViewModel : RecommendedProduct
    {
        public string DaysSincePurchased
        {
            get
            {
                var daySincePurchased = DateTime.Now.DayOfYear - this.LastOrderDate.DayOfYear;
                if (daySincePurchased == 0)
                    return "Today";
                else if (daySincePurchased == 1)
                    return "1 day ago";
                else
                    return daySincePurchased + " days ago";
            }
        }
        public string FormattedLastOrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this.LastOrderDate);
            }
        }
        public RecommendedProductViewModel(RecommendedProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
