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
    public sealed partial class CommonPage : Page
    {
        public CommonPage()
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
                ScenarioFrame.Navigate(s.ClassType);
                //RightBottomFrame.Navigate(typeof(BillingSummaryCC));
                SummaryFrame.Navigate(typeof(RecommendedProductCC));
            }
            else if (s.ClassType == typeof(CustomerOrderListCCF))
            {
                HeaderFrame.Navigate(typeof(CustomerASBCC));
                SearchBoxFrame.Navigate(typeof(FilterOrderCC));
                ScenarioFrame.Navigate(s.ClassType);
                SummaryFrame.Navigate(typeof(OrderSummaryCC));
            }
            else if (s.ClassType == typeof(CustomersCCF))
            {
                HeaderFrame.Navigate(typeof(CustomerASBCC));
                SearchBoxFrame.Navigate(typeof(FilterPersonCC), Person.Customer);
                ScenarioFrame.Navigate(typeof(CustomersCCF));
                //TODO: Move To Dashboard
                //RightBottomFrame.Navigate(typeof(CustomerTrendCC));
                SummaryFrame.Navigate(typeof(CustomerSummaryCC));


            }
            else if (s.ClassType == typeof(ProductInStock))
            {
                HeaderFrame.Navigate(typeof(ProductASBCC), ProductPage.SearchTheProduct);
                SearchBoxFrame.Navigate(typeof(FilterProductByTagCC));
                SummaryFrame.Navigate(typeof(FilterProductCC));
                ScenarioFrame.Navigate(s.ClassType);
                //TODO: Move to Dashboard
                //RightBottomFrame.Navigate(typeof(ProductConsumptionPer));
            }
            else if (s.ClassType == typeof(SupplierCCF))
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC));
                SearchBoxFrame.Navigate(typeof(FilterPersonCC), Person.Supplier);
                ScenarioFrame.Navigate(typeof(SupplierCCF));
                SummaryFrame.Navigate(typeof(SupplierSummaryCC));
            }
            else if (s.ClassType == typeof(SupplierPurchasedProductListCC))
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC));
                SearchBoxFrame.Navigate(typeof(ProductASBCC));
                ScenarioFrame.Navigate(typeof(SupplierPurchasedProductListCC));
                SummaryFrame.Navigate(typeof(SupplierBillingSummaryCC));
            }

            else if (s.ClassType == typeof(SupplierOrderCCF))
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC));
                SearchBoxFrame.Navigate(typeof(FilterSupplierOrderCC));
                ScenarioFrame.Navigate(typeof(SupplierOrderCCF));
                SummaryFrame.Navigate(typeof(SupplierOrderSummary));
            }
            base.OnNavigatedTo(e);
        }
    }
}
