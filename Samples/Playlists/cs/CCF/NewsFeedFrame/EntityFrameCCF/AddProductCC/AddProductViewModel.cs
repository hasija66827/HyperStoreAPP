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
    public class AddProductViewModel : ProductViewModelBase, INotifyPropertyChanged
    {
        public AddProductViewModel() { }
        public AddProductViewModel(ProductViewModelBase product)
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
                this._discountPer = 0;
                this._discountAmount = 0;
                this._sellingPrice = this._displayPrice - this._discountAmount;
                this.OnPropertyChanged(nameof(DisplayPrice));
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
                this.OnPropertyChanged(nameof(DiscountPer));
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
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
                this.OnPropertyChanged(nameof(DiscountPer));
            }
        }

        public override float DiscountAmount
        {
            get { return this._discountAmount; }
            set
            {
                float f = (float)Convert.ToDouble(value);
                // Resetting discountAmt to zero if it is greater than displayPrice.
                if (f >= 0 && f <= this._displayPrice)
                    this._discountAmount = f;
                else
                {
                    this._discountAmount = 0;
                    MainPage.Current.NotifyUser("Discount Amount cannot be greater than the Display Price, resetting Discount Amount to zero", NotifyType.ErrorMessage);
                }
                this._discountPer = this._displayPrice != 0 ? (this._discountAmount / this._displayPrice) * 100 : 0;
                this._sellingPrice = this._displayPrice - this._discountAmount;
                this.OnPropertyChanged(nameof(DiscountAmount));
                this.OnPropertyChanged(nameof(SellingPrice));
                this.OnPropertyChanged(nameof(DiscountPer));
            }
        }

        public override int RefillTime
        {
            get => base.RefillTime;
            set
            {
                this._refillTime = value;
                this.OnPropertyChanged(nameof(RefillTime));
            }
        }

        public override int Threshold
        {
            get => base.Threshold;
            set
            {
                this._threshold = value;
                this.OnPropertyChanged(nameof(Threshold));
            }
        }

        //TODO: #feature: consider weight parameter for non inventory items
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
