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
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductConsumptionPer : Page
    {
        public List<ProductConsumptionDeficientTrendViewModel> ProductConsumptionDeficientTrend { get; set; }
        public ProductConsumptionPer()
        {
            this.InitializeComponent();
            ProductInStock.Current.ProductStockSelectionChangedEvent += Current_ProductStockSelectionChangedEvent;
        }

        private async void Current_ProductStockSelectionChangedEvent(ProductViewModelBase productViewModelBase)
        {
            var productConsumptionTrendDTO = new ProductConsumptionTrendDTO()
            {
                ProductId = productViewModelBase.ProductId,
                MonthsCount = 1,
            };
            (ColumnChart.Series[0] as ColumnSeries).ItemsSource = null;
             var x= await AnalyticsDataSource.RetrieveProductConsumptionTrend(productConsumptionTrendDTO);
            this.ProductConsumptionDeficientTrend = x.Select(pct => new ProductConsumptionDeficientTrendViewModel(pct)).ToList();
            (ColumnChart.Series[0] as ColumnSeries).ItemsSource = ProductConsumptionDeficientTrend;
            (ColumnChart.Series[1] as ColumnSeries).ItemsSource = ProductConsumptionDeficientTrend;

        }
    }
}
