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
    public class ProductViewModelBase:TProduct
    {
        public  decimal? DiscountAmount
        {
            get { return this.DisplayPrice * (this.DiscountPer) / 100; }

        }
        public  decimal? SubTotal
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
        public string FormattedNameQuantity
        {
            get { return this.Name + " (" + this.TotalQuantity + ")"; }
        }
        public ProductViewModelBase(TProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }

        public ProductViewModelBase()
        {
            /*
            this._productId = Guid.NewGuid();
            this._barCode = "";
            this._CGSTPer = 0;
            this._displayPrice = 0;
            this._discountPer = 0;
            this._name = "";
            this._refillTime = 0;
            this._SGSTPer = 0;
            this._threshold = 0;
            this._totalQuantity = 0;
            this._wholeSellerId = null;*/
        }

        public ProductViewModelBase(Guid productId, string barCode, decimal cgstPer,
            decimal displayPrice, decimal discountPer, string name, decimal sgstPer, decimal threshold, decimal totalQuantity, Guid? wholeSellerId)
        {
            /*
            this._productId = productId;
            this._barCode = barCode;
            this._CGSTPer = cgstPer;
            this._name = name;
            this._displayPrice = displayPrice;
            this._discountPer = discountPer;
            this._SGSTPer = sgstPer;
            this._threshold = threshold;
            this._totalQuantity = totalQuantity;
            this._wholeSellerId = wholeSellerId;*/
        }
    }
}
