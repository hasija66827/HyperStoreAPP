using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Models;

namespace SDKTemplate
{
    public class PageNavigationParameter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public List<CustomerOrderProductViewModelBase> ProductsConsumed { get; set; }
        public TCustomer SelectedCustomer { get; set; }
        public BillingSummaryViewModel BillingSummaryViewModel { get; set; }
        private decimal _toBePaid;
        public decimal ToBePaid
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
                    var walletBalance = (decimal)this.SelectedCustomer.WalletBalance;
                    var discountedBillAmount = this.BillingSummaryViewModel.DiscountedBillAmount;
                    this.WalletBalanceToBeDeducted = (walletBalance <= discountedBillAmount) ? walletBalance : discountedBillAmount;
                }
                else
                    this.WalletBalanceToBeDeducted = 0;
                this._toBePaid = this.BillingSummaryViewModel.DiscountedBillAmount - this.WalletBalanceToBeDeducted;
                this._overPaid = this._toBePaid;
                this._walletAmountToBeAddedNow = 0;//THINK ABOUT IT
                this.OnPropertyChanged(nameof(WalletBalanceToBeDeducted));
                this.OnPropertyChanged(nameof(ToBePaid));
                this.OnPropertyChanged(nameof(OverPaid));
            }
        }
        public decimal WalletBalanceToBeDeducted { get; set; }

        #region PayNow
        // Step 3: If Pay Now Option is selected in the Step 2:
        private bool _isPaidNow;
        public bool IsPaidNow
        {
            get { return this._isPaidNow; }
            set { this._isPaidNow = value; }
        }
        // Extra money to be added into the wallet now
        private decimal _walletAmountToBeAddedNow;
        public decimal WalletAmountToBeAddedNow
        {
            get { return this._walletAmountToBeAddedNow; }
            set { this._walletAmountToBeAddedNow = value; }
        }
        private decimal _overPaid;
        public decimal OverPaid
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
        private decimal _walletAmountToBePaidLater;
        public decimal WalletAmountToBePaidLater
        {
            get { return this._walletAmountToBePaidLater; }
            set { this._walletAmountToBePaidLater = value; }
        }

        // Partial payment paid by the customer
        private decimal _partiallyPaid;
        public decimal PartiallyPaid
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

        public PageNavigationParameter() { }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
