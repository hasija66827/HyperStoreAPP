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
    public sealed partial class WholeSalersCCF : Page
    {
        public static WholeSalersCCF Current;
        public WholeSalersCCF()
        {
            Current = this;
            this.InitializeComponent();
            UpdateMasterListViewItemSourceByFilterCriteria();
        }
        private void UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedWholesaler = WholeSellerASBCC.Current.SelectedWholeSellerInASB;
            var wholeSalerId = selectedWholesaler?.WholeSellerId;
            //var filterWholeSalerCriteria = FilterWhole.Current.FilterCustomerCriteria;
            //var items = CustomerDataSource.GetFilteredCustomer(wholeSalerId, filterWholeSalerCriteria);
            var items = WholeSellerDataSource.WholeSellers;
            MasterListView.ItemsSource = items;
            var totalResults = items.Count;
            WholeSallerCountTB.Text = "(" + totalResults.ToString() + "/" + WholeSellerDataSource.WholeSellers.Count.ToString() + ")";
        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (WholeSellerViewModel)e.ClickedItem;
            //this.CustomerPurchaseHistoryCollection.CustomerPurchaseHistories = AnalyticsDataSource.GetPurchasedProductForCustomer(clickedItem.CustomerId, 4);
            //DetailContentPresenter.Content = this.CustomerPurchaseHistoryCollection;
        }
    }
}
