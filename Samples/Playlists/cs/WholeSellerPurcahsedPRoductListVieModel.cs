using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSellerProductListVieModel
    {
        private string _barCode;
        public string BarCode
        {
            get { return this._barCode; }
            set { this._barCode = value; }
        }
        private string _name;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        private float _purchasePrice;
        public float PurchasePrice
        {
            get { return this._purchasePrice; }
            set { this._purchasePrice = value; }
        }
        private Int32 _quantityPurchased;
        public Int32 QuantityPurchased
        {
            get { return this._quantityPurchased; }
            set { this._quantityPurchased = value; }
        }
        private float _netValue;
        public float NetValue
        {
            get { return this._netValue; }
            set { this._netValue = value; }
        }
    }
}
