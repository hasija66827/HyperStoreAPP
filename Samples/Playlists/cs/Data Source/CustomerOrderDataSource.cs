using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate;
using Models;
using SDKTemplate.DTO;
using Newtonsoft.Json;

namespace SDKTemp.Data
{
    public class CustomerOrderDataSource
    {
        #region Create
        public static async Task<bool> InitiateCustomerOrderCreationAsync(CustomerPageNavigationParameter PNP)
        {
            var customerOrderDTO = _CreateCustomerOrderDTO(PNP);
            var deductedWalletAmount = await _CreateCustomerOrderAsync(customerOrderDTO);
            if (deductedWalletAmount != null)
            {
                _SendOrderCreationNotification(PNP, deductedWalletAmount);
                return true;
            }
            return false;
        }

        private static CustomerOrderDTO _CreateCustomerOrderDTO(CustomerPageNavigationParameter PNP)
        {
            var productsConsumed = PNP.ProductsConsumed.Select(p => new ProductConsumedDTO()
            {
                ProductId = p.ProductId,
                QuantityConsumed = p.QuantityConsumed
            }).ToList();

            var customerOrderDTO = new CustomerOrderDTO()
            {
                CustomerBillingSummaryDTO = PNP.BillingSummaryViewModel,
                CustomerId = PNP.SelectedCustomer?.CustomerId,
                DueDate = PNP.CustomerCheckoutViewModel.DueDate,
                IntrestRate = Utility.TryToConvertToDecimal(PNP.CustomerCheckoutViewModel.IntrestRate),
                PayingAmount = Utility.TryToConvertToDecimal(PNP.CustomerCheckoutViewModel.PayingAmount),
                ProductsConsumed = productsConsumed
            };
            return customerOrderDTO;
        }

        private static async Task<decimal?> _CreateCustomerOrderAsync(CustomerOrderDTO customerOrderDTO)
        {
            MainPage.Current.ActivateProgressRing();
            var deductedWalletAmount = await Utility.CreateAsync<decimal?>(BaseURI.HyperStoreService + API.CustomerOrders, customerOrderDTO);
            MainPage.Current.DeactivateProgressRing();
            return deductedWalletAmount;
        }
        #endregion

        public static async Task<List<TCustomerOrder>> RetrieveCustomerOrdersAsync(CustomerOrderFilterCriteriaDTO cofc)
        {
            List<TCustomerOrder> customerOrders = await Utility.RetrieveAsync<List<TCustomerOrder>>(BaseURI.HyperStoreService + API.CustomerOrders, null, cofc);
            return customerOrders;
        }

        public static async Task<Int32> RetrieveTotalCustomerOrder()
        {
            Int32 totalOrders = await Utility.RetrieveAsync<Int32>(BaseURI.HyperStoreService + API.CustomerOrders, CustomAction.GetTotalRecordsCount, null);
            return totalOrders;
        }

        public static async Task<List<TCustomerOrderProduct>> RetrieveOrderDetailsAsync(Guid customerOrderId)
        {
            List<TCustomerOrderProduct> orderDetails = await Utility.RetrieveAsync<List<TCustomerOrderProduct>>(BaseURI.HyperStoreService + API.CustomerOrderProducts, customerOrderId.ToString(), null);
            return orderDetails;
        }

        #region SendNotification
        private static void _SendOrderCreationNotification(CustomerPageNavigationParameter PNP, decimal? deductedWalletAmount)
        {
            if (deductedWalletAmount != null)
            {
                var formattedDeductedWalletAmount = Utility.ConvertToRupee(Math.Abs((decimal)deductedWalletAmount));
                string firstMessage = "";
                if (deductedWalletAmount > 0)
                    firstMessage = String.Format("{0} has been Deducted from Wallet.", formattedDeductedWalletAmount);
                else if (deductedWalletAmount < 0)
                    firstMessage = String.Format("{0} has been Added to Wallet.", formattedDeductedWalletAmount);

                var updateWalletBalance = PNP.SelectedCustomer.WalletBalance - deductedWalletAmount;
                var formattedWalletBalance = Utility.ConvertToRupee(Math.Abs((decimal)updateWalletBalance));
                string secondMessage = "";
                if (updateWalletBalance > 0)
                    secondMessage = String.Format("You owe {0} to {1}.", formattedWalletBalance, PNP.SelectedCustomer.Name);
                else
                    secondMessage = String.Format("{0} owes you {1}.", PNP.SelectedCustomer.Name, formattedWalletBalance);

                SuccessNotification.PopUpHttpPostSuccessNotification(API.CustomerOrders, firstMessage + "\n" + secondMessage);
            }
        }
        #endregion
    }
}
