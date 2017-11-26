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
        public ObservableCollection<KeyValuePair<DateTime?, SalesInsightViewModel>> SalesInsights { get; set; }
        public ObservableCollection<KeyValuePair<DateTime?, PurchaseInsightViewModel>> PurchaseInsights { get; set; }
        public ObservableCollection<Person> NewCustomers { get; set; }
        public ObservableCollection<Person> DetachedCustomers { get; set; }

        public Dashboard()
        {
            this.InitializeComponent();
            this.SusceptibleProducts = new ObservableCollection<KeyValuePair<int, Product>>();
            this.SalesInsights = new ObservableCollection<KeyValuePair<DateTime?, SalesInsightViewModel>>();
            this.PurchaseInsights = new ObservableCollection<KeyValuePair<DateTime?, PurchaseInsightViewModel>>();
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
            var susceptibleProductDTO = new SusceptibleProductsInsightDTO(new IRange<DateTime>(DateTime.Now.AddDays(-31), DateTime.Now), 25);
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
                for (int i = 0; i < businessInsight.OrderInsight.Count(); i++)
                {
                    var salesInsight = new SalesInsightViewModel()
                    {
                        TotalSales = businessInsight.OrderInsight[i].TotalSales,
                        MoneyInBySales = businessInsight.OrderInsight[i].MoneyIn,
                        MoneyInByExplicitTransaction = businessInsight.TransactionInsight[i].MoneyIn,
                    };
                    SalesInsights.Add(new KeyValuePair<DateTime?, SalesInsightViewModel>(businessInsight.OrderInsight[i].Date, salesInsight));

                    var purchaseInsight = new PurchaseInsightViewModel()
                    {
                        TotalPurchase = businessInsight.OrderInsight[i].TotalPurchase,
                        MoneyOutByPurchase = businessInsight.OrderInsight[i].MoneyOut,
                        MoneyOutByExplicitTransactions = businessInsight.TransactionInsight[i].MoneyOut,
                    };
                    PurchaseInsights.Add(new KeyValuePair<DateTime?, PurchaseInsightViewModel>(businessInsight.OrderInsight[i].Date, purchaseInsight));
                }
            }
        }

        private async void LoadNewCustomerChartControl()
        {
            var customerInsightDTO = new CustomerInsightsDTO(new IRange<DateTime>(DateTime.Now.AddDays(-31), DateTime.Now), 25);
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
