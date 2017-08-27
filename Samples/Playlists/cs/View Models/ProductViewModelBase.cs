using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class ProductViewModelBase : TProduct
    {
        public decimal? DiscountAmount
        {
            get { return this.DisplayPrice * (this.DiscountPer) / 100; }

        }
        public decimal? SubTotal
        {
            get { return this.DisplayPrice - this.DiscountAmount; }
        }

        public virtual decimal? SellingPrice
        {
            get { return this.SubTotal + this.TotalGSTAmount; }
        }

        public virtual decimal? TotalGSTPer
        {
            get { return this.CGSTPer + this.SGSTPer; }
        }

        public virtual decimal? TotalGSTAmount
        {
            get { return this.SubTotal * (this.TotalGSTPer) / 100; }
        }
        //TODO: place it in another viewmodel
        public string FormattedNameQuantity
        {
            get { return this.Name + " (" + this.TotalQuantity + ")"; }
        }

        public ProductViewModelBase() { }

        public ProductViewModelBase(TProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
