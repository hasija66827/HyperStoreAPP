using SDKTemplate.DTO;
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
            var scenarioType = e.Parameter as ScenarioType?;

            if (scenarioType == ScenarioType.CustomerOrder)
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC), EntityType.Customer);
                SearchBoxFrame.Navigate(typeof(FilterSupplierOrderCC));
                ScenarioFrame.Navigate(typeof(SupplierOrderCCF), EntityType.Customer);
                SummaryFrame.Navigate(typeof(SupplierOrderSummary));
            }
            else if (scenarioType == ScenarioType.SupplierOrder)
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC), EntityType.Supplier);
                SearchBoxFrame.Navigate(typeof(FilterSupplierOrderCC));
                ScenarioFrame.Navigate(typeof(SupplierOrderCCF), EntityType.Supplier);
                SummaryFrame.Navigate(typeof(SupplierOrderSummary));
            }
            else if (scenarioType == ScenarioType.Customers)
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC), EntityType.Customer);
                SearchBoxFrame.Navigate(typeof(FilterPersonCC), EntityType.Customer);
                ScenarioFrame.Navigate(typeof(SupplierCCF), EntityType.Customer);
                SummaryFrame.Navigate(typeof(SupplierSummaryCC));
            }
            else if (scenarioType == ScenarioType.Suppliers)
            {
                HeaderFrame.Navigate(typeof(SupplierASBCC), EntityType.Supplier);
                SearchBoxFrame.Navigate(typeof(FilterPersonCC), EntityType.Supplier);
                ScenarioFrame.Navigate(typeof(SupplierCCF), EntityType.Supplier);
                SummaryFrame.Navigate(typeof(SupplierSummaryCC));
            }
            else if (scenarioType == ScenarioType.Products)
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
