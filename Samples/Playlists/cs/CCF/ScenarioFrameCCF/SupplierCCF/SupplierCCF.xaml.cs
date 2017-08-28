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
    public delegate void SelectedTransactionChangedDelegate();
    /// <summary>
    /// Master Detail View where 
    /// Master shows the list of wholeseller and 
    /// Detail shows list of transaction done with the wholeseller.
    /// </summary>
    public sealed partial class SupplierCCF : Page
    {
        public static SupplierCCF Current;
        //TODO: make it private o.w public getter
        public SupplierTransactionCollection TransactionHistoryOfWholeSellerCollection { get; set; }
        public event SelectedTransactionChangedDelegate SelectedTransactionChangedEvent;
        private TSupplier _SelectedSupplier { get; set; }
        private SupplierTransactionViewModel _SelectedTransaction { get; set; }
        public SupplierCCF()
        {
            Current = this;
            this.InitializeComponent();
            this.TransactionHistoryOfWholeSellerCollection = new SupplierTransactionCollection();
            this._SelectedSupplier = null;
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
            MasterListView.ItemsSource = items;
            var totalResults = items.Count;
            ///TODO:remove xxxxx
            WholeSallerCountTB.Text = "(" + totalResults.ToString() + "/" + "xxxx" + ")";
        }

        /// <summary>
        /// Rerenders the Detail View.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this._SelectedSupplier = (TSupplier)e.ClickedItem;
            var tfc = new TransactionFilterCriteriaDTO()
            {
                SupplierId = _SelectedSupplier?.SupplierId
            };
            var transactions = await SupplierTransactionDataSource.RetrieveTransactionsAsync(tfc);
            this.TransactionHistoryOfWholeSellerCollection.Transactions = transactions.Select(t => new SupplierTransactionViewModel(t)).ToList();
            DetailContentPresenter.Content = this.TransactionHistoryOfWholeSellerCollection;
        }

        private void AddMoney_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SupplierNewTransactionCC), _SelectedSupplier);
        }

        private void TransactionHistoriesOfWholeSellers_ItemClick(object sender, ItemClickEventArgs e)
        {
            this._SelectedTransaction = (SupplierTransactionViewModel)e.ClickedItem;
            this.SelectedTransactionChangedEvent?.Invoke();
        }
    }
}
