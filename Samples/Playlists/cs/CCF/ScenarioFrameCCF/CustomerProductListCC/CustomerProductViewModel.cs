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
    public delegate void QuantityChangedDelegate(object sender, decimal Quantity);
    public class CustomerProductViewModel : ProductViewModelBase, INotifyPropertyChanged
    {
        private decimal? _netValue;
        public decimal? NetValue { get { return this._netValue; } }

        private decimal? _quantityPurchased;
        public decimal? QuantityPurchased
        {
            get { return this._quantityPurchased; }
            set
            {
                this._quantityPurchased = (value >= 0) ? value : 0;
                this._netValue = this.SellingPrice * this.QuantityPurchased;
                this.OnPropertyChanged(nameof(QuantityPurchased));
                this.OnPropertyChanged(nameof(NetValue));
                CustomerProductListCC.Current.InvokeProductListChangedEvent();
            }
        }

        // Constructor to convert parent obect to child object.
        public CustomerProductViewModel(TProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
            {
                //If Property can be set then only we will set it.
                if (prop.CanWrite)
                    GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent));
            }
            this._quantityPurchased = 0;
            this._netValue = this.SellingPrice * this._quantityPurchased;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
