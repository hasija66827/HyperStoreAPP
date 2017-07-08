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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WholeSalerOrderCC : Page
    {
        public static WholeSalerOrderCC Current;
        public WholeSalerOrderCC()
        {
            Current = this;
            this.InitializeComponent();
            WholeSellerASBCC.Current.SelectedWholeSellerChangedEvent += UpdateMasterListView;
            FilterWholeSalerOrderCC.Current.FilterWholeSalerOrderCriteriaChangedEvent += UpdateMasterListView;
            WholeSellerOrderDataSource.RetrieveOrdersAsync();
            UpdateMasterListView();
        }

        private void UpdateMasterListView()
        {
            var wholeSellerId = WholeSellerASBCC.Current.SelectedWholeSellerInASB?.WholeSellerId;
            var filterWholeSalerOrderCriteria = FilterWholeSalerOrderCC.Current.FilterWholeSalerOrderCriteria;
            var items = WholeSellerOrderDataSource.GetFilteredOrder(filterWholeSalerOrderCriteria, wholeSellerId);
            MasterListView.ItemsSource = items;
            var totalResults = items.Count;
            OrderCountTB.Text = "(" + totalResults.ToString() + "/" + WholeSellerOrderDataSource.Orders.Count.ToString() + ")";
        }
    }
}
