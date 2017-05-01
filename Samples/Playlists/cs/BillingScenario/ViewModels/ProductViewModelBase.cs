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
        public virtual Guid ProductId {
            get { return this._productId; }
            set { this._productId = value; }
        }

        protected string _barCode;
        public virtual string BarCode {
            get { return this._barCode; }
            set { this._barCode = value; }
        }

        protected string _name;
        public virtual string Name {
            get { return this._name; }
            set { this._name = value; }
        }

        protected float _displayPrice;
        public virtual float DisplayPrice {
            get { return Utility.RoundInt32(this._displayPrice); }
            set { this._displayPrice = value; }
        }

        protected float _sellingPrice;
        public virtual float SellingPrice {
            get { return Utility.RoundInt32(this._sellingPrice); }
            set { this._sellingPrice = value; }
        }

        protected float _discountAmount;
        public virtual float DiscountAmount {
            get { return this._discountAmount; }
            set { this._discountAmount = value; } }

        protected float _discountPer;
        public virtual float DiscountPer {
            get { return Utility.RoundInt32(this._discountPer); }
            set { this._discountPer = value; } }

        protected Int32 _threshold;
        public virtual Int32 Threshold
        {
            get { return this._threshold; }
            set { this._threshold = value; }
        }

        private Int32 _totalQuantity;
        public virtual Int32 TotalQuantity {
            get { return this._totalQuantity; }
            set { this._totalQuantity = value; }
        }

        public ProductViewModelBase()
        {
        }
        public ProductViewModelBase(Guid productId, string barCode, string name, 
            float displayPrice, float discountPer, Int32 threshold, Int32 totalQuantity)
        {
            this._productId = productId;
            this._barCode = barCode;
            this._name = name;
            this._displayPrice = displayPrice;
            this._discountPer = discountPer;
            this._discountAmount = (this._displayPrice * this._discountPer) / 100;
            this._sellingPrice = this._displayPrice - this._discountAmount;
            this._threshold = threshold;
            this._totalQuantity = totalQuantity;
        }
    }
}
