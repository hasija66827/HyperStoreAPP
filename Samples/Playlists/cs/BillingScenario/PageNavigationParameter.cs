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
        private float _toBePaid;
        public float ToBePaid
        {
            get { return this._toBePaid; }
            set
            { this._toBePaid = value; }
        }

        #region Using Wallet
        // Step 2: Select Payement Mode
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
                this._overPaid = this._toBePaid;
                this._walletAmountToBeAddedNow = 0;//THINK ABOUT IT
                this.OnPropertyChanged(nameof(WalletBalanceToBeDeducted));
                this.OnPropertyChanged(nameof(ToBePaid));
                this.OnPropertyChanged(nameof(OverPaid));
            }
        }
        public float WalletBalanceToBeDeducted { get; set; }

        #region PayNow
        // Step 3: If Pay Now Option is selected in the Step 2:
        // Extra money to be added into the wallet now
        private float _walletAmountToBeAddedNow;
        public float WalletAmountToBeAddedNow
        {
            get { return this._walletAmountToBeAddedNow; }
            set { this._walletAmountToBeAddedNow = value; }
        }
        private float _overPaid;
        public float OverPaid
        {
            get { return this._overPaid; }
            set
            {
                if (value < this._toBePaid)
                {
                    this._overPaid = this._toBePaid;
                    MainPage.Current.NotifyUser("paying amount should be greater or equal to the amount to be paid, resetting it", NotifyType.ErrorMessage);
                }
                else
                    this._overPaid = value;
                this._walletAmountToBeAddedNow = this._overPaid - this._toBePaid;
                this.OnPropertyChanged(nameof(OverPaid));
                this.OnPropertyChanged(nameof(WalletAmountToBeAddedNow));
            }
        }
        #endregion
        #endregion

        #region PayLater
        // Step 3: If Pay later option is selected in step 2 
        // Partial billing payment to be paid later
        private float _walletAmountToBePaidLater;
        public float WalletAmountToBePaidLater
        {
            get { return this._walletAmountToBePaidLater; }
        }

        // Partial payment paid by the customer
        private float _partiallyPaid;
        public float PartiallyPaid
        {
            get { return this._partiallyPaid; }
            set
            {
                if (value >= this._toBePaid)
                {
                    this._partiallyPaid = 0;
                    MainPage.Current.NotifyUser("paying amount should be less than amount to be paid, resetting it to zero", NotifyType.ErrorMessage);
                }
                else
                {
                    this._partiallyPaid = value;
                }
                this._walletAmountToBePaidLater = this._toBePaid - this._partiallyPaid;
                this.OnPropertyChanged(nameof(PartiallyPaid));
                this.OnPropertyChanged(nameof(WalletAmountToBePaidLater));
            }
        }
        #endregion

        public PageNavigationParameter(BillingViewModel billingViewModel, CustomerViewModel customerViewModel)
        {
            this.BillingViewModel = billingViewModel;
            this.CustomerViewModel = customerViewModel;
            // uncheck the use wallet chkbox, if wallet balance is zero.
            if (this.CustomerViewModel.WalletBalance == 0)
                this.UseWallet = false;
            else
                this.UseWallet = true;
            this._walletAmountToBePaidLater = this.BillingViewModel.DiscountedBillAmount;
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
