﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public delegate void QuantityChangedDelegate(object sender, Int32 Quantity);
    public class CustomerProductViewModel : ProductViewModelBase, INotifyPropertyChanged
    {
        public override float DiscountAmount
        {
            get { return this._discountAmount; }
            set
            {
                float f = (float)Convert.ToDouble(value);
                // Resetting discount amount to zero, if it is greater than costprice.
                this._discountAmount = (f > 0 && f <= this._displayPrice) ? f : 0;
                this._discountPer = (this._discountAmount / this._displayPrice) * 100;
                this._sellingPrice = this._displayPrice - this._discountAmount;
                this._netValue = this._sellingPrice * this._quantityPurchased;
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
                this.OnPropertyChanged(nameof(DiscountPer));
                this.OnPropertyChanged(nameof(NetValue));
                CustomerProductListCC.Current.InvokeProductListChangedEvent();
                //TODO: ProfileSettings: Allow People to change the discount  on run time.
            }
        }

        private float _netValue;
        public float NetValue { get { return Utility.RoundInt32(this._netValue); } }

        private Int32 _quantityPurchased;
        public Int32 QuantityPurchased
        {
            get { return this._quantityPurchased; }
            set
            {
                this._quantityPurchased = (value >= 0) ? value : 0;
                this._netValue = this._sellingPrice * this._quantityPurchased;
                this.OnPropertyChanged(nameof(QuantityPurchased));
                this.OnPropertyChanged(nameof(NetValue));
                CustomerProductListCC.Current.InvokeProductListChangedEvent();      
            }
        }

        public override float DiscountPer
        {
            get { return Utility.RoundInt32(this._discountPer); }
            set
            {
                float f = (float)Convert.ToDouble(value);
                // Resetting discountPer to zero if it is greater than 100.
                this._discountPer = (f >= 0 && f <= 100) ? f : 0;
                this._discountAmount = (this._displayPrice * this._discountPer) / 100;
                this._sellingPrice = this._displayPrice - this._discountAmount;
                this._netValue = this._sellingPrice * this._quantityPurchased;
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
                this.OnPropertyChanged(nameof(DiscountPer));
                this.OnPropertyChanged(nameof(NetValue));
            }
        }

        // Constructor to convert parent obect to child object.
        public CustomerProductViewModel(ProductViewModelBase parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
            {
                //If Property can be set then only we will set it.
                if (prop.CanWrite)
                    GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent));
            }
            this._quantityPurchased = 0;
            this._netValue = this._sellingPrice * this._quantityPurchased;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
