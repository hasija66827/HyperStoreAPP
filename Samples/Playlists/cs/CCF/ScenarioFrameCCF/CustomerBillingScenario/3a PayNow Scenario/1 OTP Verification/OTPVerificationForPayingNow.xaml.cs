using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class OTPVerificationForPayingNow : Page
    {
        private CustomerPageNavigationParameter _PageNavigationParameter { get; set; }
        private OTPVerificationForPayingNowViewModel _OTPVerificationForPayingNow { get; set; }
        public OTPVerificationForPayingNow()
        {
            this.InitializeComponent();
            SubmitBtn.Click += SubmitBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._PageNavigationParameter = (CustomerPageNavigationParameter)e.Parameter;
            var p = this._PageNavigationParameter;
            this._OTPVerificationForPayingNow = new OTPVerificationForPayingNowViewModel()
            {
                Customer = p.SelectedCustomer,
                WalletAmountToBeDeducted = p.SelectPaymentModeViewModelBase.WalletAmountToBeDeducted
            };
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: verify OTP
            if (OTPTB.Text == "123456")
            {
                MainPage.Current.NotifyUser("OTP Verified", NotifyType.StatusMessage);
                this.Frame.Navigate(typeof(PayNow), this._PageNavigationParameter);
            }
            else
            {
                MainPage.Current.NotifyUser("Invalid OTP", NotifyType.ErrorMessage);
            }

        }
    }
}
