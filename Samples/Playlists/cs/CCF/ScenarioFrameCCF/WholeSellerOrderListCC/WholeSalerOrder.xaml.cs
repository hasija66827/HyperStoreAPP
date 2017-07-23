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
        public List<WholeSellerOrderViewModel> WholeSellerOrdersViewModel;
        public WholeSellerOrderListUpdatedDelegate WholeSellerProductListUpdatedEvent;
        public WholeSellerOrderCC()
        {
            Current = this;
            this.InitializeComponent();
            this.WholeSellerOrdersViewModel = new List<WholeSellerOrderViewModel>();
            WholeSellerASBCC.Current.SelectedWholeSellerChangedEvent += UpdateMasterListView;
            FilterWholeSalerOrderCC.Current.FilterWholeSalerOrderCriteriaChangedEvent += UpdateMasterListView;
            WholeSellerOrderDataSource.RetrieveOrdersAsync();
            UpdateMasterListView();
        }

        private void UpdateMasterListView()
        {
            var wholeSellerId = WholeSellerASBCC.Current.SelectedWholeSellerInASB?.WholeSellerId;
            var filterWholeSalerOrderCriteria = FilterWholeSalerOrderCC.Current.FilterWholeSalerOrderCriteria;
            this.WholeSellerOrdersViewModel = WholeSellerOrderDataSource.GetFilteredOrder(filterWholeSalerOrderCriteria, wholeSellerId);
            MasterListView.ItemsSource = this.WholeSellerOrdersViewModel;
            var totalResults = this.WholeSellerOrdersViewModel.Count;
            OrderCountTB.Text = "(" + totalResults.ToString() + "/" + WholeSellerOrderDataSource.Orders.Count.ToString() + ")";
            this.WholeSellerProductListUpdatedEvent?.Invoke();
        }
    }
}
