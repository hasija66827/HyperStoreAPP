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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate.CCF.Dashboard.CustomerInsight
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerInsightCC : Page
    {
        public ObservableCollection<Person> NewCustomers { get; set; }
        public ObservableCollection<Person> DetachedCustomers { get; set; }
        public CustomerInsightCC()
        {
            this.InitializeComponent();      
            this.NewCustomers = new ObservableCollection<Person>();
            this.DetachedCustomers = new ObservableCollection<Person>();
            this.DataContext = this;
            LoadNewCustomerChartControl();
            LoadDetachedCustomerChartControl();
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
            var customerInsightDTO = new CustomerInsightsDTO(new IRange<DateTime>(DateTime.Now.AddDays(-21), DateTime.Now), 25);
            var detachedCustomerInsights = await InsightsDataSource.RetreiveDetachedCustomer(customerInsightDTO);
            if (detachedCustomerInsights != null && detachedCustomerInsights.DetachedCustomerCount != 0)
            {
                foreach (var detachedCustomer in detachedCustomerInsights?.Customer)
                    DetachedCustomers.Add(detachedCustomer);
            }
        }
    }
}
