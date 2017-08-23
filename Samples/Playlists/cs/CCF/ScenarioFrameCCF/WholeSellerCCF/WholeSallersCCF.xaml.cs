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
using System.Threading.Tasks;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void SelectedTransactionChangedDelegate();
    /// <summary>
    /// Master Detail View where 
    /// Master shows the list of wholeseller and 
    /// Detail shows list of transaction done with the wholeseller.
    /// </summary>
    public sealed partial class WholeSellersCCF : Page
    {
        public static WholeSellersCCF Current;
        public TransactionHistoryOfWholeSellerCollection TransactionHistoryOfWholeSellerCollection { get; set; }
        public event SelectedTransactionChangedDelegate SelectedTransactionChangedEvent;
        public WholeSellerViewModel SelectedWholeSeller { get; set; }
        public TransactionViewModel SelectedTransaction { get; set; }
        public WholeSellersCCF()
        {
            Current = this;
            this.InitializeComponent();
            this.TransactionHistoryOfWholeSellerCollection = new TransactionHistoryOfWholeSellerCollection();
            this.SelectedWholeSeller = null;
            WholeSellerASBCC.Current.SelectedWholeSellerChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterPersonCC.Current.FilterPersonChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await UpdateMasterListViewItemSourceByFilterCriteria();
        }

        /// <summary>
        /// Rerenders the Master View on
        /// a) Initialization of the class
        /// b) FilterWholesellerCC or WholesellerASBCC triggers the event.
        /// </summary>
        private async Task UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedWholesaler = WholeSellerASBCC.Current.SelectedWholeSellerInASB;
            var wholeSalerId = selectedWholesaler?.SupplierId;
            var filterWholeSalerCriteria = FilterPersonCC.Current.FilterPersonCriteria;
            var items = await SupplierDataSource.RetrieveSuppliersAsync(new SupplierFilterCriteria() {
                SupplierId = wholeSalerId,
                WalletAmount = filterWholeSalerCriteria.WalletBalance
            });
            MasterListView.ItemsSource = items;
            var totalResults = items.Count;
            WholeSallerCountTB.Text = "(" + totalResults.ToString() + "/" + 12 + ")";

        }

        /// <summary>
        /// Rerenders the Detail View.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.SelectedWholeSeller = (WholeSellerViewModel)e.ClickedItem;
            this.TransactionHistoryOfWholeSellerCollection.Transactions = TransactionDataSource.RetreiveTransactionWholeSellerId((Guid)SelectedWholeSeller.SupplierId);
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
