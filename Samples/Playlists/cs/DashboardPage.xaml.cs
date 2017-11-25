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
    public class FinancialStuff
    {
        public string Name { get; set; }
        public int Amount { get; set; }
    }
    public class GoldDemand
    {
        public string Demand { get; set; }
        public double Year2010 { get; set; }
        public double Year2011 { get; set; }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Dashboard : Page
    {
        public Dashboard()
        {
            this.InitializeComponent();
            this.Demands = new ObservableCollection<GoldDemand>
            {
                new GoldDemand()
                {
                    Demand = "Jewelry", Year2010 = 1998.0, Year2011 = 2361.2
                },
                new GoldDemand()
                {
                    Demand = "Electronics", Year2010 = 1284.0, Year2011 = 1328.0
                },
                new GoldDemand()
                {
                    Demand = "Research", Year2010 = 1090.5, Year2011 = 1032.0
                },
                new GoldDemand()
                {
                    Demand = "Investment", Year2010 = 1643.0, Year2011 = 1898.0
                },
                new GoldDemand()
                {
                    Demand = "Bank Purchases", Year2010 = 987.0, Year2011 = 887.0
                }
            };

            this.DataContext = this;
        }

        public ObservableCollection<GoldDemand> Demands { get; set; } 
        

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadChartContents();
        }

        private void LoadChartContents()
        {
            Random rand = new Random();
           
        }
    }
}
