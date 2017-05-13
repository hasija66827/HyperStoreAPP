using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using MasterDetailApp.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SDKTemplate
{
    public class PageNavigationParameter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public BillingViewModel BillingViewModel { get; set; }
        public CustomerViewModel CustomerViewModel { get; set; }
        public float WalletBalanceToBeDeducted { get; set; }

        private float _toBePaid;
        public float ToBePaid
        {
            get { return this._toBePaid; }
            set
            { this._toBePaid = value; }
        }
        // Extra money to be added in the wallet
        private float _change;
        public float Change
        {
            get { return this._change; }
            set { this._change = value; }
        }

        private float _paid;
        public float Paid
        {
            get { return this._paid; }
            set
            {
                if (value < this._toBePaid)
                    this._paid = this._toBePaid;
                else
                    this._paid = value;
                this._change = this._paid - this._toBePaid;
                this.OnPropertyChanged(nameof(Paid));
                this.OnPropertyChanged(nameof(Change));
            }
        }
        //Above three properties changes
        private bool? _useWallet;
        public bool? UseWallet
        {
            get
            {
                var retValue = (this._useWallet != null) ? this._useWallet : false;
                return retValue;
            }
            set
            {
                this._useWallet = value;
                if (value == true)
                {
                    var walletBalance = this.CustomerViewModel.WalletBalance;
                    var discountedBillAmount = this.BillingViewModel.DiscountedBillAmount;
                    this.WalletBalanceToBeDeducted = (walletBalance <= discountedBillAmount) ? walletBalance : discountedBillAmount;
                }
                else
                    this.WalletBalanceToBeDeducted = 0;
                this._toBePaid = this.BillingViewModel.DiscountedBillAmount - this.WalletBalanceToBeDeducted;
                this._paid = this._toBePaid;
                this._change = 0;//THINK ABOUT IT
                this.OnPropertyChanged(nameof(WalletBalanceToBeDeducted));
                this.OnPropertyChanged(nameof(ToBePaid));
                this.OnPropertyChanged(nameof(Paid));
            }
        }

        public PageNavigationParameter(BillingViewModel billingViewModel, CustomerViewModel customerViewModel)
        {
            this.BillingViewModel = billingViewModel;
            this.CustomerViewModel = customerViewModel;
            // uncheck the use wallet chkbox, if wallet balance is zero.
            if (this.CustomerViewModel.WalletBalance == 0)
                this.UseWallet = false;
            else
                this.UseWallet = true;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
