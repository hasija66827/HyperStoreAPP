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
            }
        }
        private float _discountAmount;
        public string DiscountAmount
        {
            get { return Utility.RoundInt32(this._discountAmount).ToString(); }
            set
            {
                if (!Utility.CheckIfStringIsNumber(value))
                { this._discountAmount = 0; }
                else
                {
                    float f = (float)Convert.ToDouble(value);
                    // Resetting discount amount to zero, if it is greater than costprice.
                    this._discountAmount = (f > 0 && f <= this._costPrice) ? f : 0;
                }
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
        public string DiscountPer
        {
            get { return Utility.RoundInt32(this._discountPer).ToString(); }
            set
            {
                // Checking if the text is number
                if (!Utility.CheckIfStringIsNumber(value))
                { this._discountPer = 0; }
                else
                {
                    float f = (float)Convert.ToDouble(value);
                    // Resetting discountPer to zero if it is greater than 100.
                    this._discountPer = (f >= 0 && f <= 100) ? f : 0;
                }
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

        // Property is used by ASB(AutoSuggestBox) for display member path and text member path property
        public string Product_Id_Name { get { return string.Format("{0} ({1})", _id, _name); } }

        //TODO: #feature: consider weight parameter for non inventory items
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Product(string id, string name, Int32 costprice, float discount)
        {
            this._id = id;
            this._name = name;
            this._costPrice = costprice != 0 ? costprice : 1;
            this._quantity = 1;
            this._discountAmount = discount;
            this._discountPer = (discount / this._costPrice) * 100;
            this._sellingPrice = this._costPrice - discount;
            this._netValue = this._sellingPrice * this._quantity;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
