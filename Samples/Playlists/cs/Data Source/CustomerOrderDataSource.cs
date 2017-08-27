using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate;
using SDKTemplate.Data_Source;
using Models;
using SDKTemplate.DTO;
using Newtonsoft.Json;

namespace SDKTemp.Data
{
    public enum PaymentMode
    {
        payNow = 1,
        payLater = 2
    }
    public class CustomerOrderDataSource
    {
        public static async void PlaceOrder(CustomerOrderDTO customerOrderDTO)
        {
            string actionURI = "customerorders";
            var x = await Utility.CreateAsync<bool>(actionURI, customerOrderDTO);
        }

        public static async Task<List<TCustomerOrder>> RetrieveCustomerOrdersAsync(CustomerOrderFilterCriteriaDTO cofc)
        {
            string actionURI = "customerorders";
            List<TCustomerOrder> customerOrders = await Utility.RetrieveAsync<TCustomerOrder>(actionURI, cofc);
            return customerOrders;
        }

        public static async Task<List<TCustomerOrderProduct>> RetrieveOrderDetailsAsync(Guid customerOrderId)
        {
            string actionURI = "customerorderproducts/" + customerOrderId.ToString();
            List<TCustomerOrderProduct> orderDetails = await Utility.RetrieveAsync<TCustomerOrderProduct>(actionURI, null);
            return orderDetails;
        }
    }
}
