﻿using System;
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
using HyperStoreServiceAPP.DTO;
using SDKTemplate.CustomeModel;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    public delegate void PersonListUpdatedDelegate(List<Person> suppliers);
    /// <summary>
    /// Master Detail View where 
    /// Master shows the list of wholeseller and 
    /// Detail shows list of transaction done with the wholeseller.
    /// </summary>
    public sealed partial class PersonCCF : Page
    {
        public static PersonCCF Current;
        private EntityType _EntityType { get; set; }
        public event PersonListUpdatedDelegate PersonListUpdatedEvent;
        private Person _RightTappedSupplier { get; set; }
        private Int32 _totalPerson;
        public PersonCCF()
        {
            Current = this;
            this.InitializeComponent();
            PersonASBCC.Current.SelectedPersonChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterPersonCC.Current.FilterPersonChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            this._EntityType = (EntityType)e.Parameter;
            MasterColumnTitleTB.Text = this._EntityType.ToString();
            var personMetadata = await PersonDataSource.RetrievePersonMetadata(new PersonMetadataDTO() { EntityType = this._EntityType });
            _totalPerson = personMetadata.TotalRecords;
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
                SupplierId = selectedSupplier?.PersonId,
                WalletAmount = filterSupplierCriteria?.WalletBalance
            };
            var items = await PersonDataSource.RetrievePersonsAsync(sfc);
            if (items != null)
            {
                MasterListView.ItemsSource = items.Select(s => new PersonViewModel(s)).ToList();
                var totalResults = items.Count;
                PersonCountTB.Text = "( " + totalResults + " / " + _totalPerson + " )";
                PersonListUpdatedEvent?.Invoke(items);
            }
        }

        /// <summary>
        /// Rerenders the Detail View.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedPerson = (Person)e.ClickedItem;
            var tfc = new SupplierTransactionFilterCriteriaDTO()
            {
                SupplierId = selectedPerson?.PersonId
            };
            DetailContentPresenter.Content = null;
            var transactions = await TransactionDataSource.RetrieveTransactionsAsync(tfc);
            if (transactions != null)
            {
                var transactionCollection = new TransactionCollection();
                transactionCollection.Transactions = transactions.Select(t => new TransactionViewModel(t)).ToList();
                transactionCollection.PersonName = selectedPerson.Name;
                DetailContentPresenter.Content = transactionCollection;
            }
        }

        private void PayMoney_Click(object sender, RoutedEventArgs e)
        {
            var selectedSupplier = (Person)MasterListView.SelectedItem;
            if (selectedSupplier != null)
                this.Frame.Navigate(typeof(NewTransactionCC), selectedSupplier);
        }

        private void MasterListView_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            ListView listView = (ListView)sender;
            SupplierMenuFlyout.ShowAt(listView, e.GetPosition(listView));
            _RightTappedSupplier = ((FrameworkElement)e.OriginalSource).DataContext as Person;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MainPage.Current.UpdateSupplier(_RightTappedSupplier);
        }
    }
}
