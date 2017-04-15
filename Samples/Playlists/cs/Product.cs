using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class Product : INotifyPropertyChanged
    {
        private string _id;
        public string Id { get { return this._id; } }
        private string _name;
        public string Name { get { return this._name; } }
        private float _costPrice;
        public float CostPrice { get { return Utility.RoundInt32(this._costPrice); } }
        private float _sellingPrice;
        public float SellingPrice { get { return Utility.RoundInt32(this._sellingPrice); } }
        private Int32 _quantity;
        public Int32 Quantity
        {
            get { return this._quantity; }
            set
            {
                this._quantity = (value >= 0) ? value : 0;
                this._netValue = this._sellingPrice * this._quantity;
                this.OnPropertyChanged(nameof(Quantity));
                this.OnPropertyChanged(nameof(NetValue));
                // Invoking event to notify change in quantity of the product.
                QuantityPropertyChangedEvenHandler();
            }
        }
        // This event will notify the subscriber of Product class that the Quantity Property has been changed.
        public event QuantityPropertyChangedDelegate QuantityPropertyChangedEvenHandler;

        private float _discountAmount;
        public float DiscountAmount
        {
            get { return this._discountAmount; }
            set
            {
                float f = (float)Convert.ToDouble(value);
                // Resetting discount amount to zero, if it is greater than costprice.
                this._discountAmount = (f > 0 && f <= this._costPrice) ? f : 0;

                this._discountPer = (this._discountAmount / this._costPrice) * 100;
                this._sellingPrice = this._costPrice - this._discountAmount;
                this._netValue = this._sellingPrice * this._quantity;
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
                this.OnPropertyChanged(nameof(DiscountPer));
                this.OnPropertyChanged(nameof(NetValue));
            }
        }

        private float _discountPer;
        public float DiscountPer
        {
            get { return Utility.RoundInt32(this._discountPer); }
            set
            {

                float f = (float)Convert.ToDouble(value);
                // Resetting discountPer to zero if it is greater than 100.
                this._discountPer = (f >= 0 && f <= 100) ? f : 0;

                this._discountAmount = (this._costPrice * this._discountPer) / 100;
                this._sellingPrice = this._costPrice - this._discountAmount;
                this._netValue = this._sellingPrice * this._quantity;
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
                this.OnPropertyChanged(nameof(DiscountPer));
                this.OnPropertyChanged(nameof(NetValue));
            }
        }

        private float _netValue;
        public float NetValue { get { return Utility.RoundInt32(this._netValue); } }

        
        //TODO: #feature: consider weight parameter for non inventory items
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Product(string id, string name, float costprice, float discountPer)
        {
            this._id = id;
            this._name = name;
            this._costPrice = costprice;
            this._quantity = 0;
            this._discountPer = discountPer;
            this._discountAmount = (this._costPrice*this._discountPer)/100;
            this._sellingPrice = this._costPrice - this._discountAmount;
            this._netValue = this._sellingPrice * this._quantity;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
