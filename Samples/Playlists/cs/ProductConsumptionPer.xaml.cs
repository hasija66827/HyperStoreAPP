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
    public class Record
    {
        public string Day
        {
            get;
            set;
        }
        public int Quantity
        {
            get;
            set;
        }
        public Record(string day, int quantity)
        {
            this.Day = day;
            this.Quantity = quantity;
        }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductConsumptionPer : Page
    {
        public List<Record> Records { get; set; }
        public ProductConsumptionPer()
        {
            this.InitializeComponent();
            this.InitiailizeRecord();
            ProductInStock.Current.ProductStockSelectionChangedEvent += Current_ProductStockSelectionChangedEvent;
            
        }

        private void Current_ProductStockSelectionChangedEvent(ProductViewModelBase productViewModelBase)
        {
            //TODO: make an ODATA CAll, currently chart is not rerendering
            Random rand = new Random();
            this.Records.ForEach(record => record.Quantity = rand.Next(0, 200));
            (ColumnChart.Series[0] as ColumnSeries).ItemsSource = Records;
        }

        private void InitiailizeRecord()
        {
            this.Records = new List<Record>(7);
            string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
            foreach(var day in days)
                this.Records.Add(new Record(day, 0));   
        }
    }
}
