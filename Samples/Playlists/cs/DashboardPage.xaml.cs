﻿using HyperStoreServiceAPP.DTO;
using HyperStoreServiceAPP.DTO.InsightsDTO;
using Models;
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
            this.Demands = new ObservableCollection<KeyValuePair<int, Product>>();
            LoadSusceptibleProductControl();
            this.DataContext = this;
        }

        public ObservableCollection<KeyValuePair<int, Product>> Demands { get; set; }


        private async void LoadSusceptibleProductControl()
        {
            var susceptibleProductDTO = new SusceptibleProductsInsightDTO(new IRange<DateTime>(DateTime.Now.AddDays(-30), DateTime.Now), 25);
            var susceptibleProductsInsight = await InsightsDataSource.RetrieveSusceptibleProducts(susceptibleProductDTO);
            if (susceptibleProductsInsight != null)
            {
                foreach(var susceptibleProduct in susceptibleProductsInsight.SusceptibleProducts)
                Demands.Add(susceptibleProduct);
            }
        }
    }
}
