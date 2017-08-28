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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void WholeSellerOrderListUpdatedDelegate();
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WholeSellerOrderCC : Page
    {
        public static WholeSellerOrderCC Current;
        public WholeSellerOrderListUpdatedDelegate SupplierOrderListUpdatedEvent;
        public WholeSellerOrderCC()
        {
            Current = this;
            this.InitializeComponent();
            SupplierASBCC.Current.SelectedSupplierChangedEvent += UpdateMasterListViewByFilterCriteriaAsync;
            FilterSupplierOrderCC.Current.FilterSupplierOrderCriteriaChangedEvent += UpdateMasterListViewByFilterCriteriaAsync;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await UpdateMasterListViewByFilterCriteriaAsync();
        }

        private async Task UpdateMasterListViewByFilterCriteriaAsync()
        {
            var selectedSupplierId = SupplierASBCC.Current.SelectedSupplierInASB?.SupplierId;
            var filterSupplierOrderCriteria = FilterSupplierOrderCC.Current.FilterSupplierOrderCriteria;
            var sofc = new SupplierOrderFilterCriteriaDTO()
            {
                SupplierId = selectedSupplierId,
                DueDateRange = filterSupplierOrderCriteria?.DueDateRange,
                OrderDateRange = filterSupplierOrderCriteria?.OrderDateRange,
                PartiallyPaidOrderOnly = filterSupplierOrderCriteria?.IncludePartiallyPaidOrdersOnly,
            };
            var supplierOrderList = await SupplierOrderDataSource.RetrieveSupplierOrdersAsync(sofc);
            var items = supplierOrderList.Select(so => new SupplierOrderViewModel(so));
            MasterListView.ItemsSource = items;
            var totalResults = items.Count();
            //TODO: Remove xxxxx
            OrderCountTB.Text = "(" + totalResults.ToString() + "/" + "xxxxx" + ")";
            this.SupplierOrderListUpdatedEvent?.Invoke();
        }

        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedOrder = (SupplierOrderViewModel)e.ClickedItem;
            if (selectedOrder.OrderDetails.Count == 0)
            {
                var supplierOrderDetails = await SupplierOrderDataSource.RetrieveOrderDetailsAsync(selectedOrder.SupplierOrderId);
                selectedOrder.OrderDetails = supplierOrderDetails.Select(sod => new SupplierOrderProductViewModel(sod)).ToList();
                //Bug: what if he clicks first item which requires database call then second, then you are setting the response to the first order
                DetailContentPresenter.Content = MasterListView.SelectedItem;
            }
        }

    }
}
