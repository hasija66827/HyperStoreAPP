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
    public sealed partial class SupplierOrderSummary : Page
    {
        private SupplierOrderSummaryViewModel _OrderSummaryViewModel { get; set; }
        public SupplierOrderSummary()
        {
            this.InitializeComponent();
            this._OrderSummaryViewModel = new SupplierOrderSummaryViewModel();
            SupplierOrderCCF.Current.SupplierOrderListUpdatedEvent += Current_SupplierOrderListUpdatedEvent;
        }

        private void Current_SupplierOrderListUpdatedEvent(IEnumerable<TSupplierOrder> supplierOrders)
        {
            this._OrderSummaryViewModel.TotalBillAmount = supplierOrders.Sum(so => so.BillAmount);
            this._OrderSummaryViewModel.TotalPayedAmount = supplierOrders.Sum(so => so.PayedAmount);
            this._OrderSummaryViewModel.TotalPayedAmountIncTx = supplierOrders.Sum(so => so.PayedAmountIncTx);
            this._OrderSummaryViewModel.OnAllPropertyChanged();
        }
    }
}
