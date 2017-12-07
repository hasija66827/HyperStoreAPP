using HyperStoreServiceAPP.DTO;
using HyperStoreServiceAPP.DTO.InsightsDTO;
using Models;
using SDKTemplate;
using SDKTemplate.Data_Source;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : Page
    {
        public ObservableCollection<KeyValuePair<int, Product>> SusceptibleProducts { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal?>> MoneyInByExplicitTransaction { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal?>> MoneyOutByExplicitTransaction { get; set; }
        public ObservableCollection<KeyValuePair<string, SalesInsightViewModel>> SalesInsights { get; set; }
        public ObservableCollection<KeyValuePair<string, PurchaseInsightViewModel>> PurchaseInsights { get; set; }
        public ObservableCollection<Person> NewCustomers { get; set; }
        public ObservableCollection<Person> DetachedCustomers { get; set; }

        public Dashboard()
        {
            this.InitializeComponent();
            this.SusceptibleProducts = new ObservableCollection<KeyValuePair<int, Product>>();
            this.MoneyInByExplicitTransaction = new ObservableCollection<KeyValuePair<string, decimal?>>();
            this.MoneyOutByExplicitTransaction = new ObservableCollection<KeyValuePair<string, decimal?>>();
            this.SalesInsights = new ObservableCollection<KeyValuePair<string, SalesInsightViewModel>>();
            this.PurchaseInsights = new ObservableCollection<KeyValuePair<string, PurchaseInsightViewModel>>();
            this.NewCustomers = new ObservableCollection<Person>();
            this.DetachedCustomers = new ObservableCollection<Person>();
            LoadSusceptibleProductControl();
            LoadBusinessInsightControl();
            LoadNewCustomerChartControl();
            LoadDetachedCustomerChartControl();
            this.DataContext = this;
        }

        private async void LoadSusceptibleProductControl()
        {
            var susceptibleProductDTO = new SusceptibleProductsInsightDTO(new IRange<DateTime>(DateTime.Now.AddDays(-30), DateTime.Now), 25);
            var susceptibleProductsInsight = await InsightsDataSource.RetrieveSusceptibleProducts(susceptibleProductDTO);
            if (susceptibleProductsInsight != null)
            {
                foreach (var susceptibleProduct in susceptibleProductsInsight.SusceptibleProducts)
                    SusceptibleProducts.Add(susceptibleProduct);
            }
        }

        private async void LoadBusinessInsightControl()
        {
            var businessInsightDTO = new BusinessInsightDTO(new IRange<DateTime>(DateTime.Now.AddDays(-31), DateTime.Now));
            var businessInsight = await InsightsDataSource.RetrieveBusinessInsight(businessInsightDTO);
            if (businessInsight != null)
            {
                foreach (var orderInsight in businessInsight.OrderInsight)
                {
                    var salesInsight = new SalesInsightViewModel()
                    {
                        TotalSales = orderInsight.TotalSales,
                        MoneyInBySales = orderInsight.MoneyIn,
                    };
                    SalesInsights.Add(new KeyValuePair<string, SalesInsightViewModel>(orderInsight.FormattedOrderDate, salesInsight));

                    var purchaseInsight = new PurchaseInsightViewModel()
                    {
                        TotalPurchase = orderInsight.TotalPurchase,
                        MoneyOutByPurchase = orderInsight.MoneyOut,
                    };
                    PurchaseInsights.Add(new KeyValuePair<string, PurchaseInsightViewModel>(orderInsight.FormattedOrderDate, purchaseInsight));
                }

                foreach (var transactionInsight in businessInsight.TransactionInsight)
                {
                    this.MoneyInByExplicitTransaction.Add(new KeyValuePair<string, decimal?>(transactionInsight.FormattedTransactionDate, transactionInsight.MoneyIn));
                    this.MoneyOutByExplicitTransaction.Add(new KeyValuePair<string, decimal?>(transactionInsight.FormattedTransactionDate, transactionInsight.MoneyOut));                    
                }
            }
        }

        private async void LoadNewCustomerChartControl()
        {
            var customerInsightDTO = new CustomerInsightsDTO(new IRange<DateTime>(DateTime.Now.AddDays(-30), DateTime.Now), 25);
            var newCustomerInsights = await InsightsDataSource.RetreiveNewCustomers(customerInsightDTO);
            if (newCustomerInsights != null && newCustomerInsights.NewCustomerCount != 0)
            {
                foreach (var newCustomer in newCustomerInsights.Customer)
                    NewCustomers.Add(newCustomer);
            }
        }

        private async void LoadDetachedCustomerChartControl()
        {
            var customerInsightDTO = new CustomerInsightsDTO(new IRange<DateTime>(DateTime.Now.AddDays(-20), DateTime.Now), 25);
            var detachedCustomerInsights = await InsightsDataSource.RetreiveDetachedCustomer(customerInsightDTO);
            if (detachedCustomerInsights != null && detachedCustomerInsights.DetachedCustomerCount != 0)
            {
                foreach (var detachedCustomer in detachedCustomerInsights.Customer)
                    DetachedCustomers.Add(detachedCustomer);
            }
        }
    }
}
