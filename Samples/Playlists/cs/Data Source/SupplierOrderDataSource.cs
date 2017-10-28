using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using SDKTemplate.DTO;

namespace SDKTemplate
{
    public class SupplierOrderDataSource
    {
        public async static Task<bool> InitiateSupplierOrderCreationAsync(SupplierPageNavigationParameter PNP)
        {
            var IsVerified = await _InitiatePasscodeVerificationAsync();
            if (IsVerified)
            {
                var supplierOrderDTO = _CreateSupplierOrderDTO(PNP);
                var usingWalletAmount = await SupplierOrderDataSource.CreateSupplierOrderAsync(supplierOrderDTO);
                if (usingWalletAmount != null)
                {
                    _SendOrderCreationNotification(PNP, usingWalletAmount);
                    return true;
                }
            }
            return false;
        }

        private static SupplierOrderDTO _CreateSupplierOrderDTO(SupplierPageNavigationParameter PNP)
        {
            var productPurchased = PNP.ProductPurchased.Select(p => new ProductPurchasedDTO()
            {
                ProductId = p.ProductId,
                QuantityPurchased = p.QuantityPurchased,
                PurchasePricePerUnit = p.PurchasePrice,
            }
            ).ToList();

            var supplierOrderDTO = new SupplierOrderDTO()
            {
                DueDate = PNP.SupplierCheckoutViewModel.DueDate,
                IntrestRate = Utility.TryToConvertToDecimal(PNP.SupplierCheckoutViewModel.IntrestRate),
                PayingAmount = Utility.TryToConvertToDecimal(PNP.SupplierCheckoutViewModel.PayingAmount),
                ProductsPurchased = productPurchased,
                SupplierId = PNP.SelectedSupplier?.SupplierId,
                SupplierBillingSummaryDTO = PNP.SupplierBillingSummaryViewModel
            };
            return supplierOrderDTO;
        }

        #region Create
        private static async Task<decimal?> CreateSupplierOrderAsync(SupplierOrderDTO supplierOrderDTO)
        {
            MainPage.Current.ActivateProgressRing();
            var deductedWalletAmount = await Utility.CreateAsync<decimal?>(BaseURI.HyperStoreService + API.SupplierOrders, supplierOrderDTO);
            MainPage.Current.DeactivateProgressRing();
            return deductedWalletAmount;
        }
        #endregion

        #region Read
        public static async Task<List<TSupplierOrder>> RetrieveSupplierOrdersAsync(SupplierOrderFilterCriteriaDTO sofc)
        {
            List<TSupplierOrder> supplierOrders = await Utility.RetrieveAsync<List<TSupplierOrder>>(BaseURI.HyperStoreService + API.SupplierOrders, null, sofc);
            return supplierOrders;
        }

        public static async Task<Int32> RetrieveTotalSupplierOrder()
        {
            Int32 totalOrders = await Utility.RetrieveAsync<Int32>(BaseURI.HyperStoreService + API.SupplierOrders, CustomAction.GetTotalRecordsCount, null);
            return totalOrders;
        }


        public static async Task<List<TSupplierOrderProduct>> RetrieveOrderDetailsAsync(Guid supplierOrderId)
        {
            List<TSupplierOrderProduct> orderDetails = await Utility.RetrieveAsync<List<TSupplierOrderProduct>>(BaseURI.HyperStoreService + API.SupplierOrderProducts, supplierOrderId.ToString(), null);
            return orderDetails;
        }
        #endregion

        #region Notification
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

                SuccessNotification.PopUpHttpPostSuccessNotification(API.SupplierOrders, firstMessage + "\n" + secondMessage);
            }
        }
        #endregion

        #region PassCodeVerification
        private static async Task<bool> _InitiatePasscodeVerificationAsync()
        {
            var passcodeDialog = new PasscodeDialogCC.PasscodeDialogCC(BaseURI.User.Passcode);
            await passcodeDialog.ShowAsync();
            return passcodeDialog.IsVerified;
        }
        #endregion

    }
}