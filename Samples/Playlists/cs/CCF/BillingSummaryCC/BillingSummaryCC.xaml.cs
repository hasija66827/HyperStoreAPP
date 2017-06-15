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
        public BillingSummaryViewModel BillingSummaryViewModel;
        public BillingSummaryCC()
        {
            Current = this;
            this.InitializeComponent();
            BillingSummaryViewModel = new BillingSummaryViewModel();
            //Subscribing to product list changed event of ProductListCC
            ProductListChangedDelegate d = new ProductListChangedDelegate(
                (sender, totalProducts, totalBillAmount) =>
                {
                    this.BillingSummaryViewModel.TotalProducts = totalProducts;
                    this.BillingSummaryViewModel.TotalBillAmount = totalBillAmount;
                });
            ProductListCC.Current.ProductListChangedEvent += d;
        }
    }
}
