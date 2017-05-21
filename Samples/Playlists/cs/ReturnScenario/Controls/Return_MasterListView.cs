using MasterDetailApp.Data;
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
using MasterDetailApp.ViewModel;
namespace MasterDetailApp
{
    public sealed partial class MasterDetailPage : Page
    {
        private OrderViewModel _lastSelectedItem;

        //Will Update the MasterListView by filtering out Customers of specific mobile number.
        private void UpdateMasterListViewItemSource(CustomerViewModel customer=null)
        {
            List<OrderViewModel> items;
            //If mobile number is null then return empty list.
            if (customer == null)
                items =OrderDataSource.Orders;
            else
            {
                 items = OrderDataSource.RetrieveOrdersByMobileNumber(customer.MobileNo);
            }
            MasterListView.ItemsSource = items;
            string sum = "";
            foreach (var item in items)
            {
                sum += item.PaidAmount;
            }
            
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
