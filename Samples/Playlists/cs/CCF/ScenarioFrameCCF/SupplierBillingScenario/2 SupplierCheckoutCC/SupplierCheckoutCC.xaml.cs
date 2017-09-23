using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        private SupplierCheckoutViewModel _SCV { get; set; }
        private SupplierPageNavigationParameter SupplierPageNavigationParameter { get; set; }
        public SupplierCheckoutCC()
        {
            this.InitializeComponent();
            PlaceOrderBtn.Click += _PlaceOrderBtn_Click;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.SupplierPageNavigationParameter = (SupplierPageNavigationParameter)e.Parameter;
            var PNP = this.SupplierPageNavigationParameter;
            _SCV = DataContext as SupplierCheckoutViewModel;
            _SCV.ErrorsChanged += _SCV_ErrorsChanged;
            _SCV.AmountToBePaid = PNP.SupplierBillingSummaryViewModel.BillAmount;
            _SCV.PayingAmount = null;
            _SCV.DueDate = DateTime.Now.AddDays(45);
            _SCV.IntrestRate = null;
        }

        private void _SCV_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {

        }

        private async void _PlaceOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            var IsValid = _SCV.ValidateProperties();
            if (IsValid)
            {
                var IsVerified = await _InitiateOTPVerification();
                if (IsVerified)
                {
                    var supplierOrderDTO = _CreateSupplierOrderDTO();
                    var usingWalletAmount = await SupplierOrderDataSource.CreateSupplierOrderAsync(supplierOrderDTO);
                    _SendOrderCreationNotification(this.SupplierPageNavigationParameter, usingWalletAmount);
                    this.Frame.Navigate(typeof(SupplierPurchasedProductListCC));
                }
            }
        }

        private SupplierOrderDTO _CreateSupplierOrderDTO()
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
                DueDate = _SCV.DueDate,
                IntrestRate = Utility.TryToConvertToDecimal(_SCV.IntrestRate),
                ProductsPurchased = productPurchased,
                PayingAmount = Utility.TryToConvertToDecimal(_SCV.PayingAmount),
                SupplierId = PNP.SelectedSupplier?.SupplierId,
                SupplierBillingSummary = PNP.SupplierBillingSummaryViewModel
            };
            return supplierOrderDTO;
        }

        private async Task<bool> _InitiateOTPVerification()
        {
            var SMSContent = OTPVConstants.SMSContents[ScenarioType.PlacingSupplierOrder_Credit];
            var formattedSMSContent = String.Format(SMSContent, _SCV.AmountToBePaidLater,
                                                              SupplierPageNavigationParameter?.SelectedSupplier?.Name,
                                                              OTPVConstants.OTPLiteral);
            var OTPVerificationDTO = new OTPVerificationDTO()
            {
                UserID = BaseURI.User.UserId,
                ReceiverMobileNo = BaseURI.User.MobileNo,
                SMSContent = formattedSMSContent
            };
            var IsVerified = await OTPDataSource.VerifyTransactionByOTPAsync(OTPVerificationDTO);
            return IsVerified;
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
