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
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void CustomerOrderListChangedDelegate(CustomerOrderListCCF sender);
    public sealed partial class CustomerOrderListCCF : Page
    {
        public static CustomerOrderListCCF Current;
        public event CustomerOrderListChangedDelegate CustomerOrderListUpdatedEvent;
        public CustomerOrderListCCF()
        {
            Current = this;
            this.InitializeComponent();

            if (CustomerASBCC.Current == null)
                throw new Exception("CustomerASBCC should be loaded before OrderListCCF");
            CustomerASBCC.Current.SelectedCustomerChangedEvent +=
                new SelectedCustomerChangedDelegate(UpdateMasterListViewItemSourceByFilterCriteria);

            if (FilterOrderCC.Current == null)
                throw new Exception("FilterOrderCC should be loaded before OrderListCCF");
            FilterOrderCC.Current.DateChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            UpdateForVisualState(AdaptiveStates.CurrentState);
            // Don't play a content transition for first item load.
            // Sometimes, this content will be animated as part of the page transition.
            await UpdateMasterListViewItemSourceByFilterCriteria();
            DisableContentTransitions();
        }

        private async Task UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedDateRange = FilterOrderCC.Current.FilterCustomerOrderViewModel?.OrderDateRange;
            var selectedCustomerId = CustomerASBCC.Current.SelectedCustomerInASB?.CustomerId;
            var cofc = new CustomerOrderFilterCriteriaDTO()
            {
                CustomerId = selectedCustomerId,
                CustomerOrderNo = null,
                OrderDateRange = selectedDateRange
            };
            var customerOrderList = await CustomerOrderDataSource.RetrieveCustomerOrdersAsync(cofc);
            var items = customerOrderList.Select(co => new CustomerOrderViewModel(co));
            MasterListView.ItemsSource = items;
            OrderCountTB.Text = "(" + items.Count().ToString() + "/" + "xxxx" + ")";
            CustomerOrderListUpdatedEvent?.Invoke(CustomerOrderListCCF.Current);
        }

        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedOrder = (CustomerOrderViewModel)e.ClickedItem;
            if (selectedOrder.OrderDetails.Count == 0)
            {
                var customerOrderDetails = await CustomerOrderDataSource.RetrieveOrderDetailsAsync(selectedOrder.CustomerOrderId);
                selectedOrder.OrderDetails = customerOrderDetails.Select(cod => new CustomerOrderProductViewModel(cod)).ToList();
                DetailContentPresenter.Content = MasterListView.SelectedItem;
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
