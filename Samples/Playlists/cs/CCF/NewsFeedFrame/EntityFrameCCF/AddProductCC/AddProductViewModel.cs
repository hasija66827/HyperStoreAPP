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
    /*InotifyProperty changed ensures that whenever a property of the object changes 
    we can notify that other dependent propoerty of object had been changed.*/
    public class ProductDetailViewModel : ProductViewModelBase, INotifyPropertyChanged
    {
        public ProductDetailViewModel() { }
        public ProductDetailViewModel(ProductViewModelBase product)
        {
            this.ProductId = product.ProductId;
            this._barCode = product.BarCode;
            this._CGSTPer = product.CGSTPer;
            this.DisplayPrice = product.DisplayPrice;
            this.DiscountAmount = product.DiscountAmount;
            this.Name = product.Name;
            this.RefillTime = product.RefillTime;      
            this._SGSTPer = product.SGSTPer;   
            this.Threshold = product.Threshold;
        }

        public override float DisplayPrice
        {
            get => base.DisplayPrice;
            set
            {
                this._displayPrice = value;
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SubTotal));
                this.OnPropertyChanged(nameof(SellingPrice));
            }
        }

        public override float DiscountPer
        {
            get { return Utility.RoundInt32(this._discountPer); }
            set
            {
                this._discountPer = value;
                float f = (float)Convert.ToDouble(value);
                this.DiscountAmount = (this._displayPrice * this._discountPer) / 100;
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SubTotal));
                this.OnPropertyChanged(nameof(SellingPrice));
            }
        }

        public override float DiscountAmount
        {
            get => base.DiscountAmount;
            set
            {
                float f = (float)Convert.ToDouble(value);
                // Resetting discountAmt to zero if it is greater than displayPrice.
                if (f >= 0 && f <= this._displayPrice)
                    this.DiscountAmount = f;
                else
                {
                    this.DiscountAmount = 0;
                    MainPage.Current.NotifyUser("Discount Amount cannot be greater than the Display Price, resetting Discount Amount to zero", NotifyType.ErrorMessage);
                }
                this._discountPer = this._displayPrice != 0 ? (this.DiscountAmount / this._displayPrice) * 100 : 0;
                this.OnPropertyChanged(nameof(DiscountPer));
                this.OnPropertyChanged(nameof(SubTotal));
                this.OnPropertyChanged(nameof(SellingPrice));
                this.OnPropertyChanged(nameof(DiscountAmount));
            }
        }

        public override float CGSTPer
        {
            get => base.CGSTPer;
            set
            {
                this._CGSTPer = value;
                this.OnPropertyChanged(nameof(SellingPrice));
            }
        }

        public override float SGSTPer
        {
            get => base.SGSTPer;
            set
            {
                this._SGSTPer = value;
                this.OnPropertyChanged(nameof(SellingPrice));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
