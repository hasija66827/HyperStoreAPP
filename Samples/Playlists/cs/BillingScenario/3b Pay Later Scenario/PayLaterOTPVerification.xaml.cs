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
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PayLaterOTPVerification : Page
    {
        private PageNavigationParameter PageNavigationParameter { get; set; }
        public PayLaterOTPVerification()
        {
            this.InitializeComponent();
            SubmitBtn.Click += SubmitBtn_Click;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.PageNavigationParameter = (PageNavigationParameter)e.Parameter;
        }
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: verify OTP
            if (OTPTB.Text == "123456")
            {
                var updatedCustomerWalletBalance = OrderDataSource.PlaceOrder(PageNavigationParameter,PaymentMode.payLater);
                MainPage.Current.NotifyUser("OTP Verified and The updated wallet balance of the customer is \u20b9" + updatedCustomerWalletBalance, NotifyType.StatusMessage);
                this.Frame.Navigate(typeof(ProductListCC));
            }
            else
            {
                MainPage.Current.NotifyUser("Invalid OTP", NotifyType.ErrorMessage);
            }
        }
    }
}
