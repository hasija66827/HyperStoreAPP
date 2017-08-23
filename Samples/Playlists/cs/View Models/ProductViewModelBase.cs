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

        protected decimal _CGSTPer;
        public virtual decimal CGSTPer
        {
            get { return this._CGSTPer; }
            set { this._CGSTPer = value; }
        }


        protected decimal _discountPer;
        public virtual decimal DiscountPer
        {
            get { return Utility.RoundInt32(this._discountPer); }
            set { this._discountPer = value; }
        }

        protected decimal _displayPrice;
        public virtual decimal DisplayPrice
        {
            get { return Utility.RoundInt32(this._displayPrice); }
            set { this._displayPrice = value; }
        }

        protected string _name;
        public virtual string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public virtual string FormattedNameQuantity
        {
            get { return this._name + " ("+this._totalQuantity+")"; }
        }

        protected Int32 _refillTime;
        public virtual Int32 RefillTime
        {
            get { return this._refillTime; }
            set { this._refillTime = value; }
        }

        protected decimal _SGSTPer;
        public virtual decimal SGSTPer
        {
            get { return this._SGSTPer; }
            set { this._SGSTPer = value; }
        }

        protected Int32 _threshold;
        public virtual Int32 Threshold
        {
            get { return this._threshold; }
            set { this._threshold = value; }
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

        public virtual decimal DiscountAmount
        {
            get { return this._displayPrice * (this._discountPer) / 100; }

        }

        public virtual decimal SubTotal
        {
            get { return this._displayPrice - this.DiscountAmount; }
        }

        public virtual decimal SellingPrice
        {
            get { return this.SubTotal + this.TotalGSTAmount; }
        }

        public virtual decimal TotalGSTPer
        {
            get { return this._CGSTPer + this._SGSTPer; }
        }

        public virtual decimal TotalGSTAmount
        {
            get { return this.SubTotal * (this.TotalGSTPer) / 100; }
        }

        public ProductViewModelBase(DatabaseModel.Product product)
        {
            _productId = product.ProductId;
            _barCode = product.BarCode;
            _CGSTPer = product.CGSTPer;
            _name = product.Name;
            _displayPrice = product.DisplayPrice;
            _discountPer = product.DiscountPer;
            _SGSTPer = product.SGSTPer;
            _threshold = product.Threshold;
            _totalQuantity = product.TotalQuantity;
            _wholeSellerId = product.WholeSellerId;
        }

        public ProductViewModelBase()
        {
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
            this._wholeSellerId = null;
        }

        public ProductViewModelBase(Guid productId, string barCode, decimal cgstPer,
            decimal displayPrice, decimal discountPer, string name, decimal sgstPer, Int32 threshold, Int32 totalQuantity, Guid? wholeSellerId)
        {
            this._productId = productId;
            this._barCode = barCode;
            this._CGSTPer = cgstPer;
            this._name = name;
            this._displayPrice = displayPrice;
            this._discountPer = discountPer;
            this._SGSTPer = sgstPer;
            this._threshold = threshold;
            this._totalQuantity = totalQuantity;
            this._wholeSellerId = wholeSellerId;
        }
    }
}
