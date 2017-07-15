using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSellerProductListVieModel : INotifyPropertyChanged
    {
        private Guid _productId;
        public Guid ProductId { get { return this._productId; } }

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
            set
            {
                this._purchasePrice = value;
                this.OnPropertyChanged(nameof(NetValue));
                WholeSellerPurchasedProductListCC.InvokeProductListChangeEvent();
            }
        }
        private Int32 _quantityPurchased;
        public Int32 QuantityPurchased
        {
            get { return this._quantityPurchased; }
            set
            {
                this._quantityPurchased = value;
                this.OnPropertyChanged(nameof(QuantityPurchased));// This is done because quantity is updated by addItemtoBillingList method.
                this.OnPropertyChanged(nameof(NetValue));
                WholeSellerPurchasedProductListCC.InvokeProductListChangeEvent();
            }
        }
        public float NetValue
        {
            get { return this._quantityPurchased * this._purchasePrice; }
        }

        private float _sellingPrice;
        public float SellingPrice { get { return this._sellingPrice; } }
        public WholeSellerProductListVieModel(Guid productId, string barCode, string name, 
            float purchasePrice, Int32 quantityPurchased, float sellingPrice)
        {
            this._productId = productId;
            this._barCode = barCode;
            this._name = name;
            this._purchasePrice = purchasePrice;
            this._quantityPurchased = quantityPurchased;
            this._sellingPrice = sellingPrice;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
