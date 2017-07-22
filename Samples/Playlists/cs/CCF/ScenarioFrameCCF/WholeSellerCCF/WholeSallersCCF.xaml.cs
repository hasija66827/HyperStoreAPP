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
using SDKTemplate.View_Models;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void SelectedTransactionChangedDelegate();
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WholeSalersCCF : Page
    {
        public static WholeSalersCCF Current;
        public TransactionHistoryOfWholeSellerCollection TransactionHistoryOfWholeSellerCollection { get; set; }
        public event SelectedTransactionChangedDelegate SelectedTransactionChangedEvent;
        public WholeSellerViewModel SelectedWholeSeller { get; set; }
        public TransactionViewModel SelectedTransaction { get; set; }
        public WholeSalersCCF()
        {
            Current = this;
            this.InitializeComponent();
            this.TransactionHistoryOfWholeSellerCollection = new TransactionHistoryOfWholeSellerCollection();
            this.SelectedWholeSeller = null;
            WholeSellerASBCC.Current.SelectedWholeSellerChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterPersonCC.Current.FilterPersonChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterPersonCC.Current.InitializeRangeSlider(WholeSellerDataSource.GetMinimumWalletBalance(), WholeSellerDataSource.GetMaximumWalletBalance());
            UpdateMasterListViewItemSourceByFilterCriteria();
        }

        private void UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedWholesaler = WholeSellerASBCC.Current.SelectedWholeSellerInASB;
            var wholeSalerId = selectedWholesaler?.WholeSellerId;
            var filterWholeSalerCriteria = FilterPersonCC.Current.FilterPersonCriteria;
            var items = WholeSellerDataSource.GetFilteredWholeSeller(wholeSalerId, filterWholeSalerCriteria);
            MasterListView.ItemsSource = items;
            var totalResults = items.Count;
            WholeSallerCountTB.Text = "(" + totalResults.ToString() + "/" + WholeSellerDataSource.WholeSellers.Count.ToString() + ")";
        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.SelectedWholeSeller = (WholeSellerViewModel)e.ClickedItem;
            this.TransactionHistoryOfWholeSellerCollection.Transactions = TransactionDataSource.RetreiveTransactionWholeSellerId(SelectedWholeSeller.WholeSellerId);
            DetailContentPresenter.Content = this.TransactionHistoryOfWholeSellerCollection;
        }

        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(WholeSellerTransactionCC), SelectedWholeSeller);
        }

        private void TransactionHistoriesOfWholeSellers_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.SelectedTransaction = (TransactionViewModel)e.ClickedItem;
            this.SelectedTransactionChangedEvent?.Invoke();
        }
    }
}
