using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SelectPaymentModeViewModelBase
    {
        public decimal DiscountedBillAmount { get; set; }
        public virtual bool? IsUsingWallet { get; set; }
        public virtual bool? IsPayingNow { get; set; }
        public decimal? CurrentWalletBalance { get; set; }
        public decimal WalletAmountToBeDeducted
        {
            get
            {
                if (IsUsingWallet == true)
                    return Math.Min((decimal)this.CurrentWalletBalance, (decimal)this.DiscountedBillAmount);
                else
                    return 0;
            }
        }
        public decimal ToBePaid { get { return this.DiscountedBillAmount - this.WalletAmountToBeDeducted; } }

    }

    public sealed class SelectPaymentModeViewModel : SelectPaymentModeViewModelBase, INotifyPropertyChanged
    {
        private bool? _IsUisngWallet;
        public override bool? IsUsingWallet
        {
            get
            {
                var retValue = (this._IsUisngWallet != null) ? this._IsUisngWallet : false;
                return retValue;
            }
            set
            {
                this._IsUisngWallet = value;
                this.OnPropertyChanged(nameof(WalletAmountToBeDeducted));
                this.OnPropertyChanged(nameof(ToBePaid));

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
