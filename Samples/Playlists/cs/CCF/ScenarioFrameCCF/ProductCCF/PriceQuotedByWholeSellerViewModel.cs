using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        public List<TPriceQuotedBySupplier> PriceQuotedBySuppliers{get; set;
        }
    }
}
