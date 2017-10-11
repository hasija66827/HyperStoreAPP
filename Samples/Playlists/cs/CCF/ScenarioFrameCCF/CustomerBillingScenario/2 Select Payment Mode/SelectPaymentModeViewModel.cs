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
        public decimal? PayAmount { get; set; }
        public virtual bool? IsUsingWallet { get; set; }
        public virtual bool? IsPayingNow { get; set; }
        public decimal? CurrentWalletBalance { get; set; }
        public decimal WalletAmountToBeDeducted
        {
            get
            {
                try
                {
                    if (IsUsingWallet == true)
                        return Math.Min((decimal)this.CurrentWalletBalance, (decimal)this.PayAmount);
                    else
                        return 0;
                }
                catch(Exception e)
                {
                    //conversion to decimal might fail.
                }
                return 0;
            }
        }
        public decimal? ToBePaid { get { return this.PayAmount - this.WalletAmountToBeDeducted; } }
    }

    public sealed class SelectPaymentModeViewModel : SelectPaymentModeViewModelBase, INotifyPropertyChanged
    {
        private bool? _IsUsingWallet;
        public override bool? IsUsingWallet
        {
            get
            {
                var retValue = (this._IsUsingWallet != null) ? this._IsUsingWallet : false;
                return retValue;
            }
            set
            {
                this._IsUsingWallet = value;
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
