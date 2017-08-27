using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectPaymentMode : Page
    {
        private PageNavigationParameter _PageNavigationParameter { get; set; }
        private SelectPaymentModeViewModel _SelectPaymentModeViewModel { get; set; }
        public SelectPaymentMode()
        {
            this.InitializeComponent();
            ProceedToPayment.Click += ProceedToPayment_Click;
            PayLaterRadBtn.Click += PayLaterRadBtn_Click;
            WalletBalanceToBeDeductedTB.DataContextChanged += WalletBalanceToBeDeducted_DataContextChanged;
        }

        private void WalletBalanceToBeDeducted_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (this._SelectPaymentModeViewModel == null)
                throw new Exception("Page Navigation parameter should not be null");
            if (this._SelectPaymentModeViewModel.WalletAmountToBeDeducted >= 0)
                WalletBalanceToBeDeductedTB.Foreground = new SolidColorBrush(Windows.UI.Colors.LawnGreen);
            else
                WalletBalanceToBeDeductedTB.Foreground = new SolidColorBrush(Windows.UI.Colors.PaleVioletRed);
        }

        private void PayLaterRadBtn_Click(object sender, RoutedEventArgs e)
        {
            // Unchecking the use wallet check box
            _SelectPaymentModeViewModel.IsUsingWallet = false;
            UseWalletChkBox.IsChecked = false;
            // The usewallet chk box enable property is binded to the inverse of payLaterRadBtn checked property.
            // Hence useWalletChkBox will be disabled after this event execution.
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._PageNavigationParameter = (PageNavigationParameter)e.Parameter;
            var p = this._PageNavigationParameter;
            this._SelectPaymentModeViewModel = new SelectPaymentModeViewModel()
            {
                DiscountedBillAmount = p.BillingSummaryViewModel.DiscountedBillAmount,
                IsUsingWallet = p.SelectedCustomer?.WalletBalance == 0 ? false : true,
                CurrentWalletBalance = p.SelectedCustomer?.WalletBalance,
                IsPayingNow = true,
            };
        }

        private void ProceedToPayment_Click(object sender, RoutedEventArgs e)
        {
            if (PayNowRadBtn.IsChecked == true)
            {
                this._SelectPaymentModeViewModel.IsPayingNow = true;
                this._PageNavigationParameter.SelectPaymentModeViewModelBase = this._SelectPaymentModeViewModel;
                if (UseWalletChkBox.IsChecked == true)
                    this.Frame.Navigate(typeof(OTPVerificationForPayingNow), this._PageNavigationParameter);
                else
                    this.Frame.Navigate(typeof(PayNow), this._PageNavigationParameter);
            }
            else if (PayLaterRadBtn.IsChecked == true)
            {
                this._SelectPaymentModeViewModel.IsPayingNow = false;
                this._PageNavigationParameter.SelectPaymentModeViewModelBase = this._SelectPaymentModeViewModel;
                this.Frame.Navigate(typeof(PayLaterOTPVerification), this._PageNavigationParameter);
            }
        }
    }
}
