using HyperStoreService.CustomModels;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;

namespace SDKTemplate
{
    /// <summary>
    /// This class is used by detail page of Product in stock page.
    /// </summary>
    public class PriceQuotedBySupplierCollection
    {
        public List<PriceQuotedBySupplierViewModel> PriceQuotedBySuppliers
        {
            get; set;
        }
    }

    public sealed class PriceQuotedBySupplierViewModel : PriceQuotedBySupplier
    {
        public string FormattedOrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this.OrderDate);
            }
        }
     
        public PriceQuotedBySupplierViewModel(PriceQuotedBySupplier parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
