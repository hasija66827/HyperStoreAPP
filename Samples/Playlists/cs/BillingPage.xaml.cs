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
    public sealed partial class BillingPage : Page
    {
        public BillingPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Scenario s = e.Parameter as Scenario;

            if (s.ClassType == typeof(CustomerProductListCC))
            {
                HeaderFrame.Navigate(typeof(CustomerASBCC));
                SearchBoxFrame.Navigate(typeof(ProductASBCC));
                ScenarioFrame.Navigate(typeof(CustomerProductListCC));
                RecommendedProductFrame.Navigate(typeof(RecommendedProductCC));
                SummaryFrame.Navigate(typeof(BillingSummaryCC));

            }
            else if (s.ClassType == typeof(SupplierPurchasedProductListCC))
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC));
                SearchBoxFrame.Navigate(typeof(ProductASBCC));
                ScenarioFrame.Navigate(typeof(SupplierPurchasedProductListCC));
                SummaryFrame.Navigate(typeof(SupplierBillingSummaryCC));
                RecommendedProductFrame.Navigate(typeof(BlankPage));
            }
            else
                throw new NotImplementedException();
            base.OnNavigatedTo(e);
        }
    }
}
