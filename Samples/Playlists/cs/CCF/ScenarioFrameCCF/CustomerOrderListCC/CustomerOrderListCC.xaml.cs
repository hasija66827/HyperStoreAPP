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
using Windows.UI.Xaml.Media.Animation;
using SDKTemp.Data;
using System.Threading.Tasks;
using SDKTemplate.DTO;
using Models;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void CustomerOrderListChangedDelegate(IEnumerable<TCustomerOrder> customerOrders);
    public sealed partial class CustomerOrderListCCF : Page
    {
        public static CustomerOrderListCCF Current;
        public event CustomerOrderListChangedDelegate CustomerOrderListUpdatedEvent;
        private Int32 _totalOrders;
        public CustomerOrderListCCF()
        {
            Current = this;
            this.InitializeComponent();

            SupplierASBCC.Current.SelectedSupplierChangedEvent +=
                new SelectedSupplierChangedDelegate(UpdateMasterListViewItemSourceByFilterCriteria);

            FilterOrderCC.Current.DateChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            // Don't play a content transition for first item load.
            // Sometimes, this content will be animated as part of the page transition.
            _totalOrders = await CustomerOrderDataSource.RetrieveTotalCustomerOrder();
            await UpdateMasterListViewItemSourceByFilterCriteria();           
        }

        private async Task UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedDateRange = FilterOrderCC.Current.FilterCustomerOrderViewModel?.OrderDateRange;
            var selectedCustomerId = SupplierASBCC.Current.SelectedSupplierInASB?.SupplierId;
            var cofc = new CustomerOrderFilterCriteriaDTO()
            {
                CustomerId = selectedCustomerId,
                CustomerOrderNo = null,
                OrderDateRange = selectedDateRange
            };
            var customerOrderList = await CustomerOrderDataSource.RetrieveCustomerOrdersAsync(cofc);
            if (customerOrderList != null)
            {
                var items = customerOrderList.Select(co => new CustomerOrderViewModel(co));
                MasterListView.ItemsSource = items;
                OrderCountTB.Text = "( " + items.Count() + " / " + _totalOrders + " )";
                CustomerOrderListUpdatedEvent?.Invoke(items);
            }
        }

        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedOrder = (CustomerOrderViewModel)e.ClickedItem;
            if (selectedOrder.OrderDetails.Count == 0)
            {
                DetailContentPresenter.Content = null;
                var customerOrderDetails = await CustomerOrderDataSource.RetrieveOrderDetailsAsync(selectedOrder.CustomerOrderId);
                if (customerOrderDetails != null)
                {
                    selectedOrder.OrderDetails = customerOrderDetails.Select(cod => new CustomerOrderProductViewModel(cod)).ToList();
                    DetailContentPresenter.Content = MasterListView.SelectedItem;
                }
            }
            // Play a refresh animation when the user switches detail items.
            EnableContentTransitions();
        }

        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            // just for adding the transition on the content selection.
            //DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }

    
      

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            // Assure we are displaying the correct item. This is necessary in certain adaptive cases.
            //MasterListView.SelectedItem = _lastSelectedItem;
        }

        private void DisableContentTransitions()
        {
            if (DetailContentPresenter != null)
            {
                DetailContentPresenter.ContentTransitions.Clear();
            }
        }
    }
}
