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
        public virtual Guid ProductId
        {
            get { return this._productId; }
            set { this._productId = value; }
        }

        protected string _barCode;
        public virtual string BarCode
        {
            get { return this._barCode; }
            set { this._barCode = value; }
        }

        protected string _name;
        public virtual string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        protected float _displayPrice;
        public virtual float DisplayPrice
        {
            get { return Utility.RoundInt32(this._displayPrice); }
            set { this._displayPrice = value; }
        }

        protected float _sellingPrice;
        public virtual float SellingPrice
        {
            get { return Utility.RoundInt32(this._sellingPrice); }
            set { this._sellingPrice = value; }
        }

        protected float _discountAmount;
        public virtual float DiscountAmount
        {
            get { return this._discountAmount; }
            set
            {
                this._discountAmount = value;
                this._discountPer = (this._discountAmount * 100) / this._displayPrice;
            }
        }

        protected float _discountPer;
        public virtual float DiscountPer
        {
            get { return Utility.RoundInt32(this._discountPer); }
            set
            {
                this._discountPer = value;
                this._discountAmount = this._displayPrice * (100 - this._discountPer) * 100;
            }
        }

        protected Int32 _threshold;
        public virtual Int32 Threshold
        {
            get { return this._threshold; }
            set { this._threshold = value; }
        }
        protected Int32 _refillTime;
        public virtual Int32 RefillTime
        {
            get { return this._refillTime; }
            set { this._refillTime = value; }
        }
        private Int32 _totalQuantity;
        public virtual Int32 TotalQuantity
        {
            get { return this._totalQuantity; }
            set { this._totalQuantity = value; }
        }

        private Guid? _wholeSellerId;
        public virtual Guid? WholeSellerId
        {
            get { return this._wholeSellerId; }
            set { this._wholeSellerId = value; }
        }

        public ProductViewModelBase(DatabaseModel.Product item)
        {
            _productId = item.ProductId;
            _barCode = item.BarCode;
            _name = item.Name;
            _displayPrice = item.DisplayPrice;
            _discountPer = item.DiscountPer;
            _discountAmount = (_displayPrice * _discountPer) / 100;
            _sellingPrice = _displayPrice - _discountAmount;
            _threshold = item.Threshold;
            _totalQuantity = item.TotalQuantity;
            _wholeSellerId = item.WholeSellerId;
        }

        public ProductViewModelBase()
        {
            this._productId = Guid.NewGuid();
            this._barCode = "";
            this._name = "";
            this._displayPrice = 0;
            this._discountAmount = 0;
            this._discountPer = 0;
            this._sellingPrice = 0;
            this._threshold = 0;
            this._refillTime = 0;
            this._totalQuantity = 0;
            this._wholeSellerId = null;
        }

        public ProductViewModelBase(Guid productId, string barCode, string name,
            float displayPrice, float discountPer, Int32 threshold, Int32 totalQuantity, Guid? wholeSellerId)
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
            this._wholeSellerId = wholeSellerId;
        }
    }
}
