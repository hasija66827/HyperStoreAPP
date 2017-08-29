using Models;
using SDKTemplate.DTO;
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
    public sealed partial class CustomerTrendCC : Page
    {
        private CustomerPurchaseTrendCollectionViewModel _CustomerPurchaseTrendCollection { get; set; }
        public CustomerTrendCC()
        {
            this.InitializeComponent();
            this._CustomerPurchaseTrendCollection = new CustomerPurchaseTrendCollectionViewModel();
            CustomersCCF.Current.CustomerSelectionChangeEvent += Current_CustomerSelectionChangeEvent;
        }

        private async void Current_CustomerSelectionChangeEvent(TCustomer selectedCustomer)
        {
            var customerPurchaseTrendDTO = new CustomerPurchaseTrendDTO()
            {
                CustomerId = selectedCustomer?.CustomerId,
                MonthsCount = 3,
            };
            this._CustomerPurchaseTrendCollection.CustomerPurchaseTrends = await AnalyticsDataSource.RetrieveCustomerPurchaseTrend(customerPurchaseTrendDTO);
            CustomerTrendContentPresenter.Content = this._CustomerPurchaseTrendCollection;
        }
    }
}
