using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SupplierBillingProductViewModelBase : ProductViewModelBase
    {
        public decimal? NetValue { get { return this.QuantityPurchased * this.PurchasePrice; } }
        public virtual decimal? QuantityPurchased { get; set; }
        public virtual decimal? PurchasePrice { get; set; }
        public decimal? ProfitAmount { get { return this.ValueIncTax - this.PurchasePrice; } }
        public decimal? ProfitPer
        {
            get
            {
                if (PurchasePrice != 0) { return this.ProfitAmount * 100 / this.PurchasePrice; }
                else return 0;
            }
        }
        public SupplierBillingProductViewModelBase(TProduct parent) : base(parent)
        {
            this.QuantityPurchased = 1;
            this.PurchasePrice = this.ValueIncTax;
        }
    }

    public sealed class SupplierBillingProductViewModel : SupplierBillingProductViewModelBase, INotifyPropertyChanged
    {

        private decimal? _purchasePrice;
        public override decimal? PurchasePrice
        {
            get { return this._purchasePrice; }
            set
            {
                this._purchasePrice = value;
                this.OnPropertyChanged(nameof(NetValue));
                this.OnPropertyChanged(nameof(ProfitAmount));
                this.OnPropertyChanged(nameof(ProfitPer));
                SupplierPurchasedProductListCC.Current.InvokeProductListChangeEvent();
            }
        }

        private decimal? _quantityPurchased;
        public override decimal? QuantityPurchased
        {
            get { return this._quantityPurchased; }
            set
            {
                this._quantityPurchased = value;
                this.OnPropertyChanged(nameof(QuantityPurchased));// This is done because quantity is updated by addItemtoBillingList method.
                this.OnPropertyChanged(nameof(NetValue));
                SupplierPurchasedProductListCC.Current.InvokeProductListChangeEvent();
            }
        }

        public SupplierBillingProductViewModel(TProduct parent) : base(parent)
        {
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
