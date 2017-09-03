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
        public static async Task<decimal> PlaceOrderAsync(CustomerPageNavigationParameter PNP, decimal payingAmount)
        {
            var productsConsumed = PNP.ProductsConsumed.Select(p => new ProductConsumedDTO()
            {
                ProductId = p.ProductId,
                QuantityConsumed = p.QuantityConsumed
            }).ToList();

            var customerOrderDTO = new CustomerOrderDTO()
            {
                ProductsConsumed = productsConsumed,
                CustomerBillingSummary = PNP.BillingSummaryViewModel,
                CustomerId = PNP.SelectedCustomer?.CustomerId,
                IsPayingNow = PNP.SelectPaymentModeViewModelBase.IsPayingNow,
                IsUsingWallet = PNP.SelectPaymentModeViewModelBase.IsUsingWallet,
                PayingAmount = payingAmount
            };

            return await CreateCustomerOrderAsync(customerOrderDTO);
        }

        private static async Task<decimal> CreateCustomerOrderAsync(CustomerOrderDTO customerOrderDTO)
        {
            string actionURI = API.CustomerOrders;
            var x = await Utility.CreateAsync<decimal>(actionURI, customerOrderDTO);
            return x;
        }

        public static async Task<List<TCustomerOrder>> RetrieveCustomerOrdersAsync(CustomerOrderFilterCriteriaDTO cofc)
        {
            string actionURI = API.CustomerOrders;
            List<TCustomerOrder> customerOrders = await Utility.RetrieveAsync<TCustomerOrder>(API.CustomerOrders,null, cofc);
            return customerOrders;
        }

        public static async Task<List<TCustomerOrderProduct>> RetrieveOrderDetailsAsync(Guid customerOrderId)
        {
            List<TCustomerOrderProduct> orderDetails = await Utility.RetrieveAsync<TCustomerOrderProduct>(API.CustomerOrderProducts, customerOrderId.ToString(), null);
            return orderDetails;
        }
    }
}
