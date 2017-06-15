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
        public BillingSummaryCC Current;
        private BillingSummaryViewModel billingSummaryViewModel;
        public BillingSummaryCC()
        {
            Current = this;
            this.InitializeComponent();
            billingSummaryViewModel = new BillingSummaryViewModel();
            //Subscribing to product list changed event of ProductListCC
            ProductListChangedDelegate d = new ProductListChangedDelegate(
                (sender, totalProducts, totalBillAmount) =>
                {
                    this.billingSummaryViewModel.TotalProducts = totalProducts;
                    this.billingSummaryViewModel.TotalBillAmount = totalBillAmount;
                });
            ProductListCC.Current.ProductListChangedEvent += d;
        }
    }
}
