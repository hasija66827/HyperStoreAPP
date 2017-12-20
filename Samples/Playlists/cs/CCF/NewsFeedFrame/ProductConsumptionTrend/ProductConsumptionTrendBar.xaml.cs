using Models;
using SDKTemplate.Data_Source;
using SDKTemplate.DTO;
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
    public sealed partial class ProductConsumptionTrendCC : Page
    {
        public ObservableCollection<Person> NewCustomers { get; set; }
        public ObservableCollection<ProductConsumptionViewModel> ProductEstimatedConsumption { get; set; }
        public ProductConsumptionTrendCC()
        {
            this.InitializeComponent();
            this.NewCustomers = new ObservableCollection<Person>();
            this.ProductEstimatedConsumption = new ObservableCollection<ProductConsumptionViewModel>();
            ProductInStock.Current.ProductStockSelectionChangedEvent += Current_ProductStockSelectionChangedEvent;
            this.DataContext = this;
        }

        private void Current_ProductStockSelectionChangedEvent(ProductViewModelBase productViewModelBase)
        {
            this.ProductEstimatedConsumption.Clear();
            var mapDay_ProductEstConsumption = productViewModelBase.MapDay_ProductEstConsumption;
           
            if (mapDay_ProductEstConsumption != null)
            {
                var productsEstConsumption = mapDay_ProductEstConsumption.ProductEstConsumption.OrderBy(x => x.Key);
                foreach (var productEstConsumption in productsEstConsumption)
                {
                    this.ProductEstimatedConsumption.Add(new ProductConsumptionViewModel()
                    {
                        DayOfWeek = productEstConsumption.Key,
                        EstConsumption = productEstConsumption.Value
                    });
                }
            }
        }
    }
}
