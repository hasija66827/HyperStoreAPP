using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class PayLaterModeViewModelBase
    {
        public decimal ToBePaid { get; set; }
        public virtual decimal PartiallyPayingAmount { get; set; }
        public decimal AmountToBePaidLater { get { return this.ToBePaid - this.PartiallyPayingAmount; } }
        public TCustomer Customer { get; set; }
    }

    public class PayLaterModeViewModel : PayLaterModeViewModelBase, INotifyPropertyChanged
    {
        private decimal _partiallyPaid;
        public override decimal PartiallyPayingAmount
        {
            get { return this._partiallyPaid; }
            set
            {
                if (value >= this.ToBePaid)
                {
                    this._partiallyPaid = 0;
                    MainPage.Current.NotifyUser("paying amount should be less than amount to be paid, resetting it to zero", NotifyType.ErrorMessage);
                }
                else
                {
                    this._partiallyPaid = value;
                }
                this.OnPropertyChanged(nameof(PartiallyPayingAmount));
                this.OnPropertyChanged(nameof(AmountToBePaidLater));
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
