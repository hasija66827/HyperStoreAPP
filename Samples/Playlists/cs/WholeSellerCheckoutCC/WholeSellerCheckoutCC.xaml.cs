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
    public sealed partial class WholeSellerCheckoutCC : Page
    {
        public WholeSellerCheckoutViewModel wholeSellerPurchaseCheckoutViewModel;
        public WholeSellerCheckoutNavigationParameter WholeSellerCheckoutNavigationParameter { get; set; }
        public WholeSellerCheckoutCC()
        {
            this.InitializeComponent();
            PlaceOrderBtn.Click += PlaceOrderBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.WholeSellerCheckoutNavigationParameter = (WholeSellerCheckoutNavigationParameter)e.Parameter;
            var w = this.WholeSellerCheckoutNavigationParameter;
            this.wholeSellerPurchaseCheckoutViewModel = new WholeSellerCheckoutViewModel(w.WholeSellerBillingSummaryViewModel.BillAmount);
        }

        private void PlaceOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WholeSellerCheckoutNavigationParameter.WholeSellerCheckoutViewModel = this.wholeSellerPurchaseCheckoutViewModel;
            WholeSellerOrderDataSource.PlaceOrder(this.WholeSellerCheckoutNavigationParameter);
            this.Frame.Navigate(typeof(WholeSellerPurchasedProductListCC));
        }
    }
}
