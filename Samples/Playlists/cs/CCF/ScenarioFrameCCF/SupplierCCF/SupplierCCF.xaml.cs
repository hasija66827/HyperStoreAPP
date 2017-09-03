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
using System.Threading.Tasks;
using Models;
using SDKTemplate.DTO;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void SelectedTransactionChangedDelegate(TSupplierTransaction transaction);
    /// <summary>
    /// Master Detail View where 
    /// Master shows the list of wholeseller and 
    /// Detail shows list of transaction done with the wholeseller.
    /// </summary>
    public sealed partial class SupplierCCF : Page
    {
        public static SupplierCCF Current;
        public event SelectedTransactionChangedDelegate SelectedTransactionChangedEvent;
        public SupplierCCF()
        {
            Current = this;
            this.InitializeComponent();
            SupplierASBCC.Current.SelectedSupplierChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
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
            var selectedSupplier = SupplierASBCC.Current.SelectedSupplierInASB;
            var filterSupplierCriteria = FilterPersonCC.Current.FilterPersonCriteria;
            var sfc = new SupplierFilterCriteriaDTO()
            {
                SupplierId = selectedSupplier?.SupplierId,
                WalletAmount = filterSupplierCriteria?.WalletBalance
            };
            var items = await SupplierDataSource.RetrieveSuppliersAsync(sfc);
            if (items != null)
            {
                MasterListView.ItemsSource = items;
                var totalResults = items.Count;
                ///TODO:remove xxxxx
                WholeSallerCountTB.Text = "(" + totalResults.ToString() + "/" + "xxxx" + ")";
            }
        }

        /// <summary>
        /// Rerenders the Detail View.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedSupplier = (TSupplier)e.ClickedItem;
            var tfc = new SupplierTransactionFilterCriteriaDTO()
            {
                SupplierId = selectedSupplier?.SupplierId
            };
            DetailContentPresenter.Content = null;
            var transactions = await SupplierTransactionDataSource.RetrieveTransactionsAsync(tfc);
            if (transactions != null)
            {
                var supplierTransactionCollection = new SupplierTransactionCollection();
                supplierTransactionCollection.Transactions = transactions.Select(t => new SupplierTransactionViewModel(t)).ToList();
                supplierTransactionCollection.SupplierName = selectedSupplier.Name;
                DetailContentPresenter.Content = supplierTransactionCollection;
            }
        }

        private void PayMoney_Click(object sender, RoutedEventArgs e)
        {
            var selectedSupplier = (TSupplier)MasterListView.SelectedItem;
            if (selectedSupplier != null)
                this.Frame.Navigate(typeof(SupplierNewTransactionCC), selectedSupplier);
        }

        private void TransactionHistoriesOfWholeSellers_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedTransaction = (SupplierTransactionViewModel)e.ClickedItem;
            this.SelectedTransactionChangedEvent?.Invoke(selectedTransaction);
        }
    }
}
