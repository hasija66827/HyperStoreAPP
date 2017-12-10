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
namespace SDKTemplate
{
    public sealed partial class BillingSummaryCC : Page
    {
        public static BillingSummaryCC Current;
        public CustomerBillingSummaryViewModelBase BillingSummaryViewModel { get { return this._BillingSummaryViewModel; } }
        public CustomerBillingSummaryViewModelBase _BillingSummaryViewModel;

        public BillingSummaryCC()
        {
            Current = this;
            this.InitializeComponent();
            _BillingSummaryViewModel = new CustomerBillingSummaryViewModelBase();
            //Subscribing to product list changed event of ProductListCC
            ProductQtyUpdatedDelegate d = new ProductQtyUpdatedDelegate(
                () =>
                {
                    var products = CustomerProductListCC.Current.Products;
                        
                    this._BillingSummaryViewModel.TotalItems = (int?)products.Count();
                    this._BillingSummaryViewModel.TotalQuantity = (float?)products.Sum(p => p.QuantityPurchased);
                    this._BillingSummaryViewModel.CartAmount = (decimal?)products.Sum(p => (float)p.MRP * p.QuantityPurchased);
                    this._BillingSummaryViewModel.DiscountAmount = (decimal?)products.Sum(p => (float)p.DiscountAmount * p.QuantityPurchased);
                    this._BillingSummaryViewModel.Tax = (decimal?)products.Sum(p => ((float)p.TotalGSTAmount) * p.QuantityPurchased);
                    this._BillingSummaryViewModel.BillAmount = (decimal?)products.Sum(p => p.NetValue);
                    this._BillingSummaryViewModel.OnALLPropertyChanged();
                });
            CustomerProductListCC.Current.ProductQtyUpdatedEvent += d;
        }
    }
}
