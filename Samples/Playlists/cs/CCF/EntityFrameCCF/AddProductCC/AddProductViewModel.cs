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
    public sealed partial class ProductDetailsCC
    {
        private class ProductFormViewModel : ProductViewModelBase, INotifyPropertyChanged
        {
            public ProductFormViewModel() { }
            public ProductFormViewModel(ProductViewModelBase product)
            {
            }

            private decimal? _displayPrice;
            public override decimal? DisplayPrice
            {
                get { return this._displayPrice; }
                set
                {
                    this._displayPrice = value;
                    this.OnPropertyChanged(nameof(DiscountAmount));
                    this.OnPropertyChanged(nameof(SubTotal));
                    this.OnPropertyChanged(nameof(SellingPrice));
                }
            }

            private decimal? _discountPer;
            public override decimal? DiscountPer
            {
                get { return this._discountPer; }
                set
                {
                    this._discountPer = value;
                    decimal f = (decimal)Convert.ToDouble(value);
                    this.OnPropertyChanged(nameof(DiscountAmount));
                    this.OnPropertyChanged(nameof(SubTotal));
                    this.OnPropertyChanged(nameof(TotalGSTAmount));
                    this.OnPropertyChanged(nameof(SellingPrice));
                }
            }

            private decimal? _CGSTPer;
            public override decimal? CGSTPer
            {
                get { return this._CGSTPer; }
                set
                {
                    this._CGSTPer = value;
                    this.OnPropertyChanged(nameof(TotalGSTPer));
                    this.OnPropertyChanged(nameof(TotalGSTAmount));
                    this.OnPropertyChanged(nameof(SellingPrice));
                }
            }

            private decimal? _SGSTPer;
            public override decimal? SGSTPer
            {
                get { return this._SGSTPer; }
                set
                {
                    this._SGSTPer = value;
                    this.OnPropertyChanged(nameof(TotalGSTPer));
                    this.OnPropertyChanged(nameof(TotalGSTAmount));
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
}
