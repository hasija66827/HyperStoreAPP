using Models;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerSummaryCC : Page
    {
        private CustomerSummaryViewModel _CustomerSummaryViewModel { get; set; }
        public CustomerSummaryCC()
        {
            this.InitializeComponent();
            this._CustomerSummaryViewModel = new CustomerSummaryViewModel();
            CustomersCCF.Current.CustomerListUpdatedEvent += Current_CustomerListUpdatedEvent;
        }

        private void Current_CustomerListUpdatedEvent(List<TCustomer> customers)
        {
            this._CustomerSummaryViewModel.TotalWalletBalance = (decimal)customers.Sum(c => c.WalletBalance);
            this._CustomerSummaryViewModel.OnALLPropertyChanged();
        }
    }
}
