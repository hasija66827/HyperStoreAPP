using HyperStoreServiceAPP.DTO;
using HyperStoreServiceAPP.DTO.InsightsDTO;
using Models;
using SDKTemplate;
using SDKTemplate.CCF.Dashboard.CustomerInsight;
using SDKTemplate.CCF.Dashboard.ProductInsight;
using SDKTemplate.CCF.Dashboard.SalesPurchaseInsight;
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
    public class InsightComboboxItem
    {
        public string Text { get; set; }
        public Type PageType { get; set; }
        public override string ToString() { return Text; }
        public InsightComboboxItem(string text, Type pageType)
        {
            Text = text;
            PageType = pageType;
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : Page
    {
        private List<InsightComboboxItem> insights;
        private List<InsightComboboxItem> Insights { get { return this.insights; } }

        public Dashboard()
        {
            this.InitializeComponent();
            InsightCombobox.SelectionChanged += InsightSelectorCmb_SelectionChanged;
           InitializeInsightSelectorCmb();
        }

        private void InsightSelectorCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (InsightComboboxItem)InsightCombobox.SelectedItem;
            this.DashboardFrame.Navigate(selectedItem.PageType);
        }

        private void InitializeInsightSelectorCmb()
        {
            insights = new List<InsightComboboxItem>();
            insights.Add(new InsightComboboxItem("Business Insight Dashboard", typeof(BusinessInsightCC)));
            insights.Add(new InsightComboboxItem("Product Insight Dashboard", typeof(ProductInsightCC)));
            insights.Add(new InsightComboboxItem("Customer Insight Dashboard", typeof(CustomerInsightCC)));
        }
    }
}
