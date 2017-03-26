using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string CostPrice { get { return Utility.RoundInt32(this._costPrice).ToString()+ "\u20B9"; } }
        private float _sellingPrice;
        public string SellingPrice { get { return Utility.RoundInt32(this._sellingPrice).ToString() + "\u20B9"; } }
        private Int32 _quantity;
        public string Quantity
        {
            get { return this._quantity.ToString(); }
            set
            {
                if (!Utility.CheckIfStringIsNumber(value))
                { this._quantity = 0; }
                else
                {
                    Int32 v = Convert.ToInt32(value);
                    this._quantity = (v >= 0) ? v : 0;
                }
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
        public string NetValue { get { return Utility.RoundInt32(this._netValue).ToString()+ "\u20B9"; } }
        //TODO: #feature: consider weight parameter for non inventory items
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Product(string id, string name, Int32 costprice, float discount, Int32 quantity)
        {
            this._id = id;
            this._name = name;
            this._costPrice = costprice != 0 ? costprice : 1;
            this._quantity = quantity;
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

    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products { get { return this._products; } }
        public ProductViewModel()
        {
            this._products.Add(new Product("1111", "Maggi", 12, 2, 1));
            this._products.Add(new Product("2222", "Ghee", 310, 0, 2));
            this._products.Add(new Product("4333", "shampoo", 200, 200, 50));
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
