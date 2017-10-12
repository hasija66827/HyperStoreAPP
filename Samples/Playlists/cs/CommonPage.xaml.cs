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

            if (s.ScenarioType == ScenarioType.CustomerOrder)
            {
                HeaderFrame.Navigate(typeof(CustomerASBCC));
                SearchBoxFrame.Navigate(typeof(FilterOrderCC));
                ScenarioFrame.Navigate(typeof(CustomerOrderListCCF));
                SummaryFrame.Navigate(typeof(OrderSummaryCC));
            }
            else if (s.ScenarioType == ScenarioType.SupplierOrder)
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC));
                SearchBoxFrame.Navigate(typeof(FilterSupplierOrderCC));
                ScenarioFrame.Navigate(typeof(SupplierOrderCCF));
                SummaryFrame.Navigate(typeof(SupplierOrderSummary));
            }
            else if (s.ScenarioType == ScenarioType.Customers)
            {
                HeaderFrame.Navigate(typeof(CustomerASBCC));
                SearchBoxFrame.Navigate(typeof(FilterPersonCC), Person.Customer);
                ScenarioFrame.Navigate(typeof(CustomersCCF));
                SummaryFrame.Navigate(typeof(CustomerSummaryCC));
            }
            else if (s.ScenarioType == ScenarioType.Suppliers)
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC));
                SearchBoxFrame.Navigate(typeof(FilterPersonCC), Person.Supplier);
                ScenarioFrame.Navigate(typeof(SupplierCCF));
                SummaryFrame.Navigate(typeof(SupplierSummaryCC));
            }
            else if (s.ScenarioType == ScenarioType.Products)
            {
                HeaderFrame.Navigate(typeof(ProductASBCC), ProductPage.SearchTheProduct);
                SearchBoxFrame.Navigate(typeof(FilterProductByTagCC));
                SummaryFrame.Navigate(typeof(FilterProductCC));
                ScenarioFrame.Navigate(typeof(ProductInStock));
            }
            else
                throw new NotImplementedException();
            base.OnNavigatedTo(e);
        }
    }
}
