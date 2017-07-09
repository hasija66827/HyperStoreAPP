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
    public sealed partial class WholeSellerOrderSummary : Page
    {
        public WholeSelleOrderSummaryViewModel WholeSelleOrderSummaryViewModel { get; set; }
        public WholeSellerOrderSummary()
        {
            this.InitializeComponent();
            this.WholeSelleOrderSummaryViewModel = new WholeSelleOrderSummaryViewModel();
            WholeSalerOrderCC.Current.WholeSellerProductListUpdatedEvent += UpdateSummary;
            UpdateSummary(); //To compute the values for the first time.
        }
        private void UpdateSummary()
        {
            this.WholeSelleOrderSummaryViewModel.TotalBillAmount = WholeSalerOrderCC.Current.WholeSellerOrdersViewModel.Sum(wo => wo.BillAmount);
            this.WholeSelleOrderSummaryViewModel.TotalPaidAmount = WholeSalerOrderCC.Current.WholeSellerOrdersViewModel.Sum(wo => wo.PaidAmount);
        }
    }
}
