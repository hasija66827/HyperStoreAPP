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
    public class BillingSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private decimal _totalBillAmount;
        public decimal TotalBillAmount
        {
            get
            {
                return this._totalBillAmount;
            }
            set
            {
                this._totalBillAmount = value;
                this.OnPropertyChanged(nameof(TotalBillAmount));
                this.OnPropertyChanged(nameof(DiscountedBillAmount));
            }
        }
        private decimal _totalProducts;
        public decimal TotalProducts
        {
            get
            {
                return this._totalProducts;
            }
            set
            {
                this._totalProducts = value;
                this.OnPropertyChanged(nameof(TotalProducts));
            }
        }
        private decimal _additionalDiscountPer;
        public decimal AdditionalDiscountPer
        {
            get { return this._additionalDiscountPer; }
            set
            {
                this._additionalDiscountPer = value;
                this.OnPropertyChanged(nameof(AdditionalDiscountPer));
                this.OnPropertyChanged(nameof(DiscountedBillAmount));
            }
        }

        public decimal DiscountedBillAmount { get { return ((100 - this._additionalDiscountPer) * this._totalBillAmount) / 100; } }

        public BillingSummaryViewModel()
        {
            _totalProducts = 0;
            _totalBillAmount = 0;
            _additionalDiscountPer = 0;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
