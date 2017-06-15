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
            ProductListCCUpdatedDelegate d = new ProductListCCUpdatedDelegate(
                (products) =>
                {
                    double sum = 0;
                    foreach (ProductViewModel product in products)
                        sum += product.NetValue;
                    Int32 count = 0;
                    foreach (ProductViewModel product in products)
                        count += product.QuantityPurchased;
                    
                    this.BillingSummaryViewModel.TotalProducts = count;
                    this.BillingSummaryViewModel.TotalBillAmount = Utility.RoundInt32((float)sum); 
                });
            ProductListCC.Current.ProductListCCUpdatedEvent += d;
        }
    }
}
