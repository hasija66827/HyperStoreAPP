using SDKTemplate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SupplierCheckoutViewModelBase
    {
        public decimal AmountToBePaid { get; set; }
        public virtual decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get { return this.AmountToBePaid - this.PaidAmount; } }
        public decimal IntrestRate { get; set; }
        public DateTime DueDate { get; set; }
        public SupplierCheckoutViewModelBase() {
            this.AmountToBePaid = 0;
            this.PaidAmount = 0;
            this.IntrestRate = 0;
            this.DueDate = DateTime.Now;
        }
    }

    public class SupplierCheckoutViewModel : SupplierCheckoutViewModelBase, INotifyPropertyChanged
    {
        private decimal _paidAmount;
        public override decimal PaidAmount
        {
            get { return this._paidAmount; }
            set
            {
                if (value < 0)
                {
                    MainPage.Current.NotifyUser(string.Format("Paid Amount {0} must be greater than zero", this._paidAmount), NotifyType.ErrorMessage);
                    value = 0;
                }
                else if (value > this.AmountToBePaid)
                {
                    MainPage.Current.NotifyUser(string.Format("Paid Amount {0} must be lesser or equal to billing amount {1}", this._paidAmount, this.AmountToBePaid), NotifyType.ErrorMessage);
                    value = 0;
                }
                this._paidAmount = value;
                this.OnPropertyChanged(nameof(PaidAmount));
                this.OnPropertyChanged(nameof(RemainingAmount));
            }
        }
        public SupplierCheckoutViewModel():base() { }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
