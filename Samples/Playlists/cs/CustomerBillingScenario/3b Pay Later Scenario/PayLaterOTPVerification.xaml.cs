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
using SDKTemp.Data;
using Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PayLaterOTPVerification : Page
    {
        private PayLaterModeViewModel _PayLaterModeViewModel { get; set; }
        private PageNavigationParameter _PageNavigationParameter { get; set; }
        public PayLaterOTPVerification()
        {
            this.InitializeComponent();
            SubmitBtn.Click += SubmitBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._PageNavigationParameter = (PageNavigationParameter)e.Parameter;
            var p = this._PageNavigationParameter;
            this._PayLaterModeViewModel = new PayLaterModeViewModel()
            {
                Customer = p.SelectedCustomer,
                ToBePaid = p.SelectPaymentModeViewModelBase.ToBePaid,
                PartiallyPayingAmount = 0
            };
        }

        private async void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: verify OTP
            if (OTPTB.Text == "123456")
            {
                var task = CustomerOrderDataSource.PlaceOrderAsync(this._PageNavigationParameter, this._PayLaterModeViewModel.PartiallyPayingAmount);
                var usingWalletAmount = await task;
                this.Frame.Navigate(typeof(CustomerProductListCC));
            }
            else
            {
                MainPage.Current.NotifyUser("Invalid OTP", NotifyType.ErrorMessage);
            }
        }
    }
}
