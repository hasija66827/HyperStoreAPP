using SDKTemp.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using SDKTemp.ViewModel;
namespace SDKTemp
{
    public delegate void OrderListChangedDelegate(OrderListCC sender);
    public sealed partial class OrderListCC : Page
    {
        private OrderViewModel _lastSelectedItem;
        public List<OrderViewModel> orderList;
        public event OrderListChangedDelegate OrderListChangedEvent;
        private void UpdateMasterListViewItemSource(CustomerViewModel customer = null)
        {
            if (customer == null)
                Current.orderList = OrderDataSource.Orders;
            else
            {
                Current.orderList = OrderDataSource.RetrieveOrdersByMobileNumber(customer.MobileNo);
            }
            OrderListCC.Current.OrderListChangedEvent?.Invoke(OrderListCC.Current);
            MasterListView.ItemsSource = orderList;            
        }
        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (OrderViewModel)e.ClickedItem;
            _lastSelectedItem = clickedItem;
            // Play a refresh animation when the user switches detail items.
            EnableContentTransitions();
        }
        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            // just for adding the transition on the content selection.
            //DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }
    }
}
