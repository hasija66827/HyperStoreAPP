using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class ProductViewModelBase
    {
        protected Guid _productId;
        public virtual Guid ProductId { get { return this._productId; } }
        protected string _barCode;
        public virtual string BarCode { get { return this._barCode; } }
        protected string _name;
        public virtual string Name { get { return this._name; } }
        protected float _displayPrice;
        public virtual float DisplayPrice { get { return Utility.RoundInt32(this._displayPrice); } }
        protected float _sellingPrice;
        public virtual float SellingPrice { get { return Utility.RoundInt32(this._sellingPrice); } }
        
        protected float _discountAmount;
        public virtual float DiscountAmount
        {
            get { return this._discountAmount; }
            set
            {
                float f = (float)Convert.ToDouble(value);
                // Resetting discount amount to zero, if it is greater than costprice.
                this._discountAmount = (f > 0 && f <= this._displayPrice) ? f : 0;
            }
        }

        protected float _discountPer;
        public virtual float DiscountPer
        {
            get { return Utility.RoundInt32(this._discountPer); }
            set
            {

                float f = (float)Convert.ToDouble(value);
                // Resetting discountPer to zero if it is greater than 100.
                this._discountPer = (f >= 0 && f <= 100) ? f : 0;
            }
        }
        protected Int32 _threshold;
        public virtual Int32 Threshold { get { return this._threshold; } }

        public ProductViewModelBase()
        {
            _displayPrice = 0;
            _discountPer = 0;
            _name = "xxxxx";
        }
        public ProductViewModelBase(Guid productId, string barCode, string name, float displayPrice, float discountPer, Int32 threshold=0)
        {
            this._productId = productId;
            this._barCode = barCode;
            this._name = name;
            this._displayPrice = displayPrice;
            this._discountPer = discountPer;
            this._discountAmount = (this._displayPrice * this._discountPer) / 100;
            this._sellingPrice = this._displayPrice - this._discountAmount;
            this._threshold = threshold;
        }
    }
}
