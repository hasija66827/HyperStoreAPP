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
using SDKTemp.ViewModel;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomersCCF : Page
    {
        public static CustomersCCF Current;
        public CustomersCCF()
        {
            Current = this;
            this.InitializeComponent();
            CustomerASBCC.Current.SelectedCustomerChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            FilterCustomerCC.Current.FilterCustomerCChangedEvent += UpdateMasterListViewItemSourceByFilterCriteria;
            UpdateMasterListViewItemSourceByFilterCriteria();
        }

        private void UpdateMasterListViewItemSourceByFilterCriteria()
        {
            var selectedCustomer = CustomerASBCC.Current.SelectedCustomerInASB;
            var customerId = selectedCustomer?.CustomerId;
            var filterCustomerCriteria = FilterCustomerCC.Current.FilterCustomerCriteria;
            var items = CustomerDataSource.GetFilteredCustomer(customerId, filterCustomerCriteria);
            MasterListView.ItemsSource = items;
        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = (CustomerViewModel)e.ClickedItem;
        }
    }
}
