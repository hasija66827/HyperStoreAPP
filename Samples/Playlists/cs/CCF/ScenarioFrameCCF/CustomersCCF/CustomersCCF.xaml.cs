using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static SDKTemplate.CustomerDataSource;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void CustomerSelectionChangeDelegate(TCustomer customer);
    public delegate void CustomerListUpdatedDelegate(List<TCustomer> customers);

    /// <summary>
    /// Master Detail View where 
    /// Master shows the list of the customer and
    /// Detail shows the all the products purchased by the customer in last three months.
    /// </summary>
    public sealed partial class CustomersCCF : Page
    {
        public static CustomersCCF Current;
        public event CustomerSelectionChangeDelegate CustomerSelectionChangeEvent;
        public event CustomerListUpdatedDelegate CustomerListUpdatedEvent;
        public CustomersCCF()
        {
            Current = this;
            this.InitializeComponent();
            CustomerASBCC.Current.SelectedCustomerChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterPersonCC.Current.FilterPersonChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await UpdateMasterListViewItemSourceByFilterCriteria();
        }

        /// <summary>
        /// Rerenders the Master View on
        /// a) Initialization of the class
        /// b) FilterCustomerCC or CustomerASBCC triggers the event.
        /// </summary>
        private async Task UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedCustomer = CustomerASBCC.Current.SelectedCustomerInASB;
            var customerId = selectedCustomer?.CustomerId;
            var filterCustomerCriteria = FilterPersonCC.Current.FilterPersonCriteria;
            CustomerFilterCriteriaDTO cfc = new CustomerFilterCriteriaDTO()
            {
                CustomerId = customerId,
                WalletAmount = filterCustomerCriteria.WalletBalance
            };
            var items = await CustomerDataSource.RetrieveCustomersAsync(cfc);
            if (items != null)
            {
                MasterListView.ItemsSource = items;
                var totalResults = items.Count;
                CustomerCountTB.Text = "(" + totalResults.ToString() + "/" + 12 + ")";
                CustomerListUpdatedEvent?.Invoke(items);
            }
        }

        /// <summary>
        /// Rerenders the Detail view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedCustomer = (TCustomer)e.ClickedItem;
            this.CustomerSelectionChangeEvent?.Invoke(selectedCustomer);
            var tfc = new CustomerTransactionFilterCriteriaDTO()
            {
                CustomerId = selectedCustomer?.CustomerId
            };
            DetailContentPresenter.Content = null;
            var transactions = await CustomerTransactionDataSource.RetrieveTransactionsAsync(tfc);
            if (transactions != null)
            {
                var customerTransactionCollection = new CustomerTransactionCollection();
                customerTransactionCollection.Transactions = transactions.Select(t => new CustomerTransactionViewModel(t)).ToList();
                customerTransactionCollection.CustomerName = selectedCustomer.Name;
                DetailContentPresenter.Content = customerTransactionCollection;
            }
        }

        private void ReceiveMoney_Click(object sender, RoutedEventArgs e)
        {
            var selecetedCustomer = (TCustomer)MasterListView.SelectedItem;
            if (selecetedCustomer != null)
                this.Frame.Navigate(typeof(CustomerNewTransactionCC), selecetedCustomer);
        }
    }
}
