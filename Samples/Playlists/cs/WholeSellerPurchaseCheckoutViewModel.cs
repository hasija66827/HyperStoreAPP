using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
   public class WholeSellerPurchaseCheckoutViewModel : INotifyPropertyChanged
    {
        private float _amountToBePaid;
        public float AmountToBePaid { get { return this._amountToBePaid; } set { this._amountToBePaid = value; } }
        private float _paidAmount;
        public float PaidAmount
        {
            get { return this._paidAmount; }
            set
            {
                if (value < 0)
                {
                    MainPage.Current.NotifyUser("Paid Amount must be greater than zero", NotifyType.ErrorMessage);
                    value = 0;
                }
                else if (value > this._amountToBePaid)
                {
                    MainPage.Current.NotifyUser("Paid Amount must be lesser or equal to billing amount", NotifyType.ErrorMessage);
                    value = 0;
                }
                this._paidAmount = value;
                this.OnPropertyChanged(nameof(PaidAmount));
                this.OnPropertyChanged(nameof(RemainingAmount));
            }
        }
        public float RemainingAmount { get { return this._amountToBePaid - this._paidAmount; } }
        private float _intrestRate;
        public float IntrestRate
        {
            get { return this._intrestRate; }
            set
            {
                this._intrestRate = value;
                this.OnPropertyChanged(nameof(IntrestRate));
            }
        }
        private DateTime _dueDate;
        public DateTime DueDate { get { return this._dueDate; } set { this._dueDate = value; } }

        public WholeSellerPurchaseCheckoutViewModel(float amountToBePaid)
        {
            this._amountToBePaid = amountToBePaid;
            this._paidAmount = 0;
            this._intrestRate = 0;
            this._dueDate=DateTime.Now.AddDays(15);
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
