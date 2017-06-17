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
using SDKTemp.ViewModel;
using SDKTemp.Data;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void OrderListChangedDelegate(OrderListCCF sender);
    public sealed partial class OrderListCCF : Page
    {
        public static OrderListCCF Current;
        public List<OrderViewModel> orderList;
        public event OrderListChangedDelegate OrderListChangedEvent;
        public OrderListCCF()
        {
            Current = this;
            this.InitializeComponent();

            if (CustomerASBCC.Current == null)
                throw new Exception("CustomerASBCC should be loaded before OrderListCCF");
            CustomerASBCC.Current.SelectedCustomerChangedEvent +=
                new SelectedCustomerChangedDelegate(UpdateMasterListViewItemSourceByCustomer);

            if (FilterOrderCC.Current == null)
                throw new Exception("FilterOrderCC should be loaded before OrderListCCF");
            FilterOrderCC.Current.DateChangedEvent += UpdateMasterListViewItemSourceByDate;
        }

        private void UpdateMasterListViewItemSourceByDate(object sender)
        {
            FilterOrderCC a = (FilterOrderCC)sender;
            FilterOrderViewModel selectedDateRange = a.SelectedDateRange;
            if (selectedDateRange == null)
                Current.orderList = CustomerOrderDataSource.Orders;
            else
                Current.orderList = CustomerOrderDataSource.RetrieveOrdersByDate(selectedDateRange);
            OrderListCCF.Current.OrderListChangedEvent?.Invoke(OrderListCCF.Current);
            MasterListView.ItemsSource = Current.orderList;
        }

        private void UpdateMasterListViewItemSourceByCustomer(CustomerViewModel customer = null)
        {
            if (customer == null)
                Current.orderList = CustomerOrderDataSource.Orders;
            else
                Current.orderList = CustomerOrderDataSource.RetrieveOrdersByMobileNumber(customer.MobileNo);
            OrderListCCF.Current.OrderListChangedEvent?.Invoke(OrderListCCF.Current);
            MasterListView.ItemsSource = orderList;
        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (OrderViewModel)e.ClickedItem;
            // Play a refresh animation when the user switches detail items.
            EnableContentTransitions();
        }

        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            // just for adding the transition on the content selection.
            //DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            MasterListView.ItemsSource = CustomerOrderDataSource.Orders;
            UpdateForVisualState(AdaptiveStates.CurrentState);
            // Don't play a content transition for first item load.
            // Sometimes, this content will be animated as part of the page transition.
            DisableContentTransitions();
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(e.NewState, e.OldState);
        }

        private void UpdateForVisualState(VisualState newState, VisualState oldState = null)
        {
            var isNarrow = newState == NarrowState;
            EntranceNavigationTransitionInfo.SetIsTargetElement(MasterListView, isNarrow);
            if (DetailContentPresenter != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
            }
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
