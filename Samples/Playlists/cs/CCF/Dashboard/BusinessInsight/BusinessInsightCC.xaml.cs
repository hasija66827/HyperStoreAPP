using HyperStoreServiceAPP.DTO.InsightsDTO;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.CCF.Dashboard.SalesPurchaseInsight
{
   
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BusinessInsightCC : Page
    {
        public ObservableCollection<KeyValuePair<string, SalesInsightViewModel>> SalesInsights { get; set; }
        public ObservableCollection<KeyValuePair<string, PurchaseInsightViewModel>> PurchaseInsights { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal?>> MoneyInByExplicitTransaction { get; set; }
        public ObservableCollection<KeyValuePair<string, decimal?>> MoneyOutByExplicitTransaction { get; set; }

        public BusinessInsightCC()
        {
            this.InitializeComponent();
            this.SalesInsights = new ObservableCollection<KeyValuePair<string, SalesInsightViewModel>>();
            this.PurchaseInsights = new ObservableCollection<KeyValuePair<string, PurchaseInsightViewModel>>();
            this.MoneyInByExplicitTransaction = new ObservableCollection<KeyValuePair<string, decimal?>>();
            this.MoneyOutByExplicitTransaction = new ObservableCollection<KeyValuePair<string, decimal?>>();
            this.DataContext = this;
            LoadBusinessInsightControl();
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
    }
}
