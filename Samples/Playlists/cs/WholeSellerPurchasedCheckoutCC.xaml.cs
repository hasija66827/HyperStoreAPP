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
    public sealed partial class WholeSellerPurchasedCheckoutCC : Page
    {
        public WholeSellerPurchaseCheckoutViewModel wholeSellerPurchaseCheckoutViewModel;
        public WholeSellerPurchaseNavigationParameter WholeSellerPurchaseNavigationParameter { get; set; }
        public WholeSellerPurchasedCheckoutCC()
        {
            this.InitializeComponent();
            PlaceOrderBtn.Click += PlaceOrderBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var w = (WholeSellerPurchaseNavigationParameter)e.Parameter;
            this.WholeSellerPurchaseNavigationParameter = w;
            this.wholeSellerPurchaseCheckoutViewModel = new WholeSellerPurchaseCheckoutViewModel(w.WholeSellerBillingViewModel.BillAmount);
        }

        private void PlaceOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WholeSellerPurchaseNavigationParameter.WholeSellerPurchaseCheckoutViewModel = this.wholeSellerPurchaseCheckoutViewModel;
            WholeSellerOrderDataSource.PlaceOrder(this.WholeSellerPurchaseNavigationParameter);
            //TODO: need to be removed when back button will be used.
            this.Frame.Navigate(typeof(WholeSellerPurchasedProductListCC));
        }
    }
}
