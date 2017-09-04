using SDKTemplate.DTO;
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
    public sealed partial class SupplierCheckoutCC : Page
    {
        private SupplierCheckoutViewModel _SupplierCheckoutViewModel { get; set; }
        private SupplierPageNavigationParameter SupplierPageNavigationParameter { get; set; }
        public SupplierCheckoutCC()
        {
            this.InitializeComponent();
            PlaceOrderBtn.Click += PlaceOrderBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SupplierPageNavigationParameter = (SupplierPageNavigationParameter)e.Parameter;
            var PNP = this.SupplierPageNavigationParameter;
            this._SupplierCheckoutViewModel = new SupplierCheckoutViewModel()
            {
                AmountToBePaid = PNP.WholeSellerBillingSummaryViewModel.BillAmount,
                PaidAmount = PNP.WholeSellerBillingSummaryViewModel.BillAmount,
                DueDate = DateTime.Now.AddDays(20),
                IntrestRate = 0
            };
        }

        private async void PlaceOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            var PNP = this.SupplierPageNavigationParameter;

            var productPurchased = PNP.ProductPurchased.Select(p => new ProductPurchasedDTO()
            {
                ProductId = p.ProductId,
                QuantityPurchased = p.QuantityPurchased,
                PurchasePricePerUnit = p.PurchasePrice,
            }
            ).ToList();

            var supplierOrderDTO = new SupplierOrderDTO()
            {
                BillAmount = PNP.WholeSellerBillingSummaryViewModel.BillAmount,
                ProductsPurchased = productPurchased,
                SupplierId = PNP.SelectedSupplier?.SupplierId,
                PaidAmount = _SupplierCheckoutViewModel.PaidAmount,
                IntrestRate = _SupplierCheckoutViewModel.IntrestRate,
                DueDate = _SupplierCheckoutViewModel.DueDate,
            };

            var usingWalletAmount = await SupplierOrderDataSource.CreateSupplierOrderAsync(supplierOrderDTO);

            _SendOrderCreationNotification(PNP, usingWalletAmount);

            this.Frame.Navigate(typeof(SupplierPurchasedProductListCC));
        }

        private static void _SendOrderCreationNotification(SupplierPageNavigationParameter PNP, decimal? usingWalletAmount)
        {
            if (usingWalletAmount != null)
            {
                string formattedUsingWalletAmount = Utility.ConvertToRupee(Math.Abs((decimal)usingWalletAmount));
                string firstMessage = String.Format("{0} has been added to wallet.", formattedUsingWalletAmount);

                string secondMessage = "";
                decimal updatedWalletBalance = PNP.SelectedSupplier.WalletBalance + (decimal)usingWalletAmount;
                var formattedWalletBalance = Utility.ConvertToRupee(Math.Abs(updatedWalletBalance));
                if (updatedWalletBalance > 0)
                    secondMessage = String.Format("You owe {0} to {1}.", formattedWalletBalance, PNP.SelectedSupplier.Name);
                else
                    secondMessage = String.Format("{0} owes you {1}.", PNP.SelectedSupplier.Name, formattedWalletBalance);

                SuccessNotification.PopUpSuccessNotification(API.SupplierOrders, firstMessage + "\n" + secondMessage);
            }
        }

    }
}
