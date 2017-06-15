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
        private float _totalBillAmount;
        public float TotalBillAmount
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
        private Int32 _totalProducts;
        public Int32 TotalProducts
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
        private float _additionalDiscountPer;
        public float AdditionalDiscountPer
        {
            get { return this._additionalDiscountPer; }
            set
            {
                this._additionalDiscountPer = value;
                this.OnPropertyChanged(nameof(AdditionalDiscountPer));
                this.OnPropertyChanged(nameof(DiscountedBillAmount));
            }
        }

        public float DiscountedBillAmount { get { return ((100 - this._additionalDiscountPer) * this._totalBillAmount) / 100; } }

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
