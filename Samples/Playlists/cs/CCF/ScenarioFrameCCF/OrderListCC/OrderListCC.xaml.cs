using HyperStoreServiceAPP.DTO;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void OrderListUpdatedDelegate(IEnumerable<Order> supplierOrders);
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderCCF : Page
    {
        public static OrderCCF Current;
        private EntityType EntityType { get; set; }
        public OrderListUpdatedDelegate OrderListUpdatedEvent;
        private Int32 _totalOrders;
        public OrderCCF()
        {
            Current = this;
            this.InitializeComponent();
            PersonASBCC.Current.SelectedPersonChangedEvent += UpdateMasterListViewByFilterCriteriaAsync;
            FilterOrderCC.Current.FilterOrderCriteriaChangedEvent += UpdateMasterListViewByFilterCriteriaAsync;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this.EntityType = (EntityType)e.Parameter;
            MasteColumnTitleTB.Text = this.EntityType + " Orders";
            _totalOrders = await OrderDataSource.RetrieveTotalOrder();
            await UpdateMasterListViewByFilterCriteriaAsync();
        }

        private async Task UpdateMasterListViewByFilterCriteriaAsync()
        {
            var selectedPersonId = PersonASBCC.Current.SelectedPersonInASB?.PersonId;
            var filterSupplierOrderCriteria = FilterOrderCC.Current.FilterSupplierOrderCriteria;
            var sofc = new SupplierOrderFilterCriteriaDTO()
            {
                SupplierId = selectedPersonId,
                DueDateRange = filterSupplierOrderCriteria?.DueDateRange,
                OrderDateRange = filterSupplierOrderCriteria?.OrderDateRange,
                PartiallyPaidOrderOnly = filterSupplierOrderCriteria?.IncludePartiallyPaidOrdersOnly,
                EntityType = this.EntityType,
            };
            var orders = await OrderDataSource.RetrieveOrdersAsync(sofc);
            if (orders != null)
            {
                var items = orders.Select(so => new OrderViewModel(so));
                MasterListView.ItemsSource = items;
                var totalResults = items.Count();
                OrderCountTB.Text = "( " + totalResults + " / " + _totalOrders + " )";
                this.OrderListUpdatedEvent?.Invoke(items);
            }
        }

        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedOrder = (OrderViewModel)e.ClickedItem;
            if (selectedOrder.OrderDetails.Count == 0)
            {
                DetailContentPresenter.Content = null;
                var orderDetails = await OrderDataSource.RetrieveOrderDetailsAsync(selectedOrder.OrderId);
                if (orderDetails != null)
                {
                    selectedOrder.OrderDetails = orderDetails.Select(sod => new OrderProductViewModel(sod)).ToList();
                    //Bug: what if he clicks first item which requires database call then second, then you are setting the response to the first order
                    DetailContentPresenter.Content = MasterListView.SelectedItem;
                }
            }
        }
    }
}
