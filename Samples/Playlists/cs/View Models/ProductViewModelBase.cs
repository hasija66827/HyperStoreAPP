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
    public class ProductViewModelBase : Product
    {
        public decimal? DiscountAmount
        {
            get { return this.MRP * (this.DiscountPer) / 100; }
        }
        public decimal? ValueIncTax
        {
            get { return this.MRP - this.DiscountAmount; }
        }

        public virtual decimal? TotalGSTPer
        {
            get { return this.CGSTPer + this.SGSTPer; }
        }

        public decimal? ValueExcTax { get { return ValueIncTax * 100 / (100 + CGSTPer + SGSTPer); } }

        public virtual decimal? TotalGSTAmount
        {
            get { return this.ValueIncTax - ValueExcTax; }
        }

        //TODO: place it in another viewmodel
        public string FormattedNameQuantity
        {
            get { return this.Name + " (" + this.TotalQuantity + ")"; }
        }

        public ProductViewModelBase() { }

        public ProductViewModelBase(Product parent)
        {
            foreach (PropertyInfo prop in typeof(Product).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
