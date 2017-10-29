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
    public delegate void SupplierListUpdatedDelegate(List<TSupplier> suppliers);
    /// <summary>
    /// Master Detail View where 
    /// Master shows the list of wholeseller and 
    /// Detail shows list of transaction done with the wholeseller.
    /// </summary>
    public sealed partial class SupplierCCF : Page
    {
        public static SupplierCCF Current;
        private EntityType _EntityType { get; set; }
        public event SupplierListUpdatedDelegate SupplierListUpdatedEvent;
        private TSupplier _RightTappedSupplier { get; set; }
        private Int32 _totalSuppliers;
        public SupplierCCF()
        {
            Current = this;
            this.InitializeComponent();
            PersonASBCC.Current.SelectedPersonChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterPersonCC.Current.FilterPersonChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this._EntityType = (EntityType)e.Parameter;
            _totalSuppliers = await PersonDataSource.RetrieveTotalPersons();
            await UpdateMasterListViewItemSourceByFilterCriteria();
        }

        /// <summary>
        /// Rerenders the Master View on
        /// a) Initialization of the class
        /// b) FilterWholesellerCC or WholesellerASBCC triggers the event.
        /// </summary>
        private async Task UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedSupplier = PersonASBCC.Current.SelectedPersonInASB;
            var filterSupplierCriteria = FilterPersonCC.Current.FilterPersonCriteria;
            var sfc = new SupplierFilterCriteriaDTO()
            {
                EntityType = this._EntityType,
                SupplierId = selectedSupplier?.SupplierId,
                WalletAmount = filterSupplierCriteria?.WalletBalance
            };
            var items = await PersonDataSource.RetrievePersonsAsync(sfc);
            if (items != null)
            {
                MasterListView.ItemsSource = items;
                var totalResults = items.Count;
                SupplierCountTB.Text = "( " + totalResults + " / " + _totalSuppliers + " )";
                SupplierListUpdatedEvent?.Invoke(items);
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
                var supplierTransactionCollection = new TransactionCollection();
                supplierTransactionCollection.Transactions = transactions.Select(t => new TransactionViewModel(t)).ToList();
                supplierTransactionCollection.PersonName = selectedSupplier.Name;
                DetailContentPresenter.Content = supplierTransactionCollection;
            }
        }

        private void PayMoney_Click(object sender, RoutedEventArgs e)
        {
            var selectedSupplier = (TSupplier)MasterListView.SelectedItem;
            if (selectedSupplier != null)
                this.Frame.Navigate(typeof(SupplierNewTransactionCC), selectedSupplier);
        }

        private void MasterListView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView listView = (ListView)sender;
            SupplierMenuFlyout.ShowAt(listView, e.GetPosition(listView));
            _RightTappedSupplier = ((FrameworkElement)e.OriginalSource).DataContext as TSupplier;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Current.UpdateSupplier(_RightTappedSupplier);
        }
    }
}
