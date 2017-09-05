using Models;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SupplierBillingSummaryCC : Page
    {
        public static SupplierBillingSummaryCC Current;
        public SupplierBillingSummaryViewModelBase BillingSummaryViewModel { get { return this._BillingSummaryViewModel; } }
        private SupplierBillingSummaryViewModelBase _BillingSummaryViewModel { get; set; }

        public SupplierBillingSummaryCC()
        {
            Current = this;
            this.InitializeComponent();
            this._BillingSummaryViewModel = new SupplierBillingSummaryViewModelBase();
            SupplierPurchasedProductListCC.Current.SupplierProductListUpdatedEvent += _ComputeBillSummary;
        }

        private void _ComputeBillSummary(List<SupplierBillingProductViewModelBase>products)
        {
            this._BillingSummaryViewModel.PayAmount = products.Sum(p => (decimal)p.NetValue);
            this._BillingSummaryViewModel.TotalQuantity = products.Sum(p => (decimal)p.QuantityPurchased);
            this._BillingSummaryViewModel.TotalItems = products.Count();
            this._BillingSummaryViewModel.OnALLPropertyChanged();
        }
    }
}
