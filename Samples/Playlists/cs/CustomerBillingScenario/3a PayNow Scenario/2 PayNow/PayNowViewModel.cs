using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class PayNowViewModelBase
    {
        public decimal ToBePaid { get; set; }
        public virtual decimal ActuallyPaying { get; set; }
        public decimal WalletAmountToBeAdded { get { return this.ActuallyPaying - this.ToBePaid; } }
    }

    public class PayNowViewModel : PayNowViewModelBase, INotifyPropertyChanged
    {
        private decimal _actuallyPaying;
        public override decimal ActuallyPaying
        {
            get { return this._actuallyPaying; }
            set
            {
                if (value < this.ToBePaid)
                {
                    this._actuallyPaying = this.ToBePaid;
                    MainPage.Current.NotifyUser("paying amount should be greater or equal to the amount to be paid, resetting it", NotifyType.ErrorMessage);
                }
                else
                    this._actuallyPaying = value;

                this.OnPropertyChanged(nameof(ActuallyPaying));
                this.OnPropertyChanged(nameof(WalletAmountToBeAdded));
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
