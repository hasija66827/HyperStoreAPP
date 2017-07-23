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
using SDKTemp.ViewModel;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// Master Detail View where 
    /// Master shows the list of the customer and
    /// Detail shows the all the products purchased by the customer in last three months.
    /// </summary>
    public sealed partial class CustomersCCF : Page
    {
        public CustomerPurchaseHistoryCollection CustomerPurchaseHistoryCollection { get; set; }
        public static CustomersCCF Current;
        public CustomerViewModel SelectedCustomer { get; set; }
        public CustomersCCF()
        {
            Current = this;
            this.InitializeComponent();
            this.CustomerPurchaseHistoryCollection = new CustomerPurchaseHistoryCollection();
            CustomerASBCC.Current.SelectedCustomerChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterPersonCC.Current.FilterPersonChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterPersonCC.Current.InitializeRangeSlider(CustomerDataSource.GetMinimumWalletBalance(), CustomerDataSource.GetMaximumWalletBalance());
            UpdateMasterListViewItemSourceByFilterCriteria();
        }

        /// <summary>
        /// Rerenders the Master View on
        /// a) Initialization of the class
        /// b) FilterCustomerCC or CustomerASBCC triggers the event.
        /// </summary>
        private void UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedCustomer = CustomerASBCC.Current.SelectedCustomerInASB;
            var customerId = selectedCustomer?.CustomerId;
            var filterCustomerCriteria = FilterPersonCC.Current.FilterPersonCriteria;
            var items = CustomerDataSource.GetFilteredCustomer(customerId, filterCustomerCriteria);
            MasterListView.ItemsSource = items;
            var totalResults = items.Count;
            CustomerCountTB.Text = "(" + totalResults.ToString() + "/" + CustomerDataSource.Customers.Count.ToString() + ")";
        }

        /// <summary>
        /// Rerenders the Detail view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.SelectedCustomer = (CustomerViewModel)e.ClickedItem;
            this.CustomerPurchaseHistoryCollection.CustomerPurchaseHistories = AnalyticsDataSource.GetPurchasedProductForCustomer(this.SelectedCustomer.CustomerId, 4);
            DetailContentPresenter.Content = this.CustomerPurchaseHistoryCollection;
        }
    }
}
