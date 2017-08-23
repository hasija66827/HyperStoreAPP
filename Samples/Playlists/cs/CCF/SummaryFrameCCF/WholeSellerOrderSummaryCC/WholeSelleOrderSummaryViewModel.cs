using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSelleOrderSummaryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private decimal _totalBillAmount;
        public decimal TotalBillAmount
        {
            get { return this._totalBillAmount; }
            set
            {
                this._totalBillAmount = value;
                this.OnPropertyChanged(nameof(TotalBillAmount));
                this.OnPropertyChanged(nameof(TotalRemaiingAmount));
            }
        }

        private decimal _totalPaidAmount;
        public decimal TotalPaidAmount
        {
            get { return this._totalPaidAmount; }
            set
            {
                this._totalPaidAmount = value;
                this.OnPropertyChanged(nameof(TotalPaidAmount));
                this.OnPropertyChanged(nameof(TotalRemaiingAmount));
            }
        }
        public decimal TotalRemaiingAmount
        {
            get { return this._totalBillAmount - this._totalPaidAmount; }
        }
        public WholeSelleOrderSummaryViewModel()
        {
            this._totalBillAmount = 0;
            this._totalPaidAmount = 0;
        }


        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
