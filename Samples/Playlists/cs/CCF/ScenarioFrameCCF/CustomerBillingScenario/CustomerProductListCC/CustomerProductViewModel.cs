﻿using Models;
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
    public class CustomerBillingProductViewModelBase : ProductViewModelBase
    {
        public decimal? NetValue { get { return this.QuantityPurchased * this.ValueIncTax; } }
        public virtual decimal? QuantityPurchased { get; set; }

        public CustomerBillingProductViewModelBase(Product parent) : base(parent)
        {
            this.QuantityPurchased = 0;
        }
    }

    public delegate void QuantityChangedDelegate(object sender, decimal Quantity);
    public sealed class CustomerBillingProductViewModel : CustomerBillingProductViewModelBase, INotifyPropertyChanged
    {
        private decimal? _quantityConsumed;
        public override decimal? QuantityPurchased
        {
            get { return this._quantityConsumed; }
            set
            {
                this._quantityConsumed = (value >= 0) ? value : 0;
                this.OnPropertyChanged(nameof(QuantityPurchased));
                this.OnPropertyChanged(nameof(NetValue));
                CustomerProductListCC.Current.InvokeProductListChangedEvent();
            }
        }

        public CustomerBillingProductViewModel(Product parent) : base(parent) { }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
