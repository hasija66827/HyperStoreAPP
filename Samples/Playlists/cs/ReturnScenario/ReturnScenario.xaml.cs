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
using SDKTemplate;
namespace SDKTemp
{
    public sealed partial class OrderListCC : Page
    {
        public static OrderListCC Current;
        public OrderListCC()
        {
            this.InitializeComponent();
            Current = this;
            if (CustomerASBCC.Current == null)
                throw new Exception("CustomerASBCC should be loaded before orderListCC");
            CustomerASBCC.Current.SelectedCustomerChangedEvent += new SelectedCustomerChangedDelegate(UpdateMasterListViewItemSource);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            /*#perf: Highly Database intensive operation and is called eevery time when we navigate to this page*/
            //#Optimization can be, if no customer has been recently added, then don't refetch again. Or with database updation we can update our all the data source as well, i.e our cache.
            // Hence we can call this function one time only in the constructor, instead of calling it everytime on page navigation. 
            //Called every time on navigation
            OrderDataSource.RetrieveOrdersAsync();
            MasterListView.ItemsSource = OrderDataSource.Orders;
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
            MasterListView.SelectedItem = _lastSelectedItem;
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
