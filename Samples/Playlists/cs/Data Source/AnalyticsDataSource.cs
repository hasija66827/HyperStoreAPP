using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate.DTO;
using Models;

namespace SDKTemplate
{
    public class AnalyticsDataSource
    {
        public static async Task<List<TCustomerPurchaseTrend>> RetrieveCustomerPurchaseTrend(CustomerPurchaseTrendDTO customerPurchaseTrendDTO)
        {
            string actionURI = "CustomerPurchaseTrend";
            return await Utility.RetrieveAsync<TCustomerPurchaseTrend>(actionURI, customerPurchaseTrendDTO);
        }

        public static async Task<List<TProductConsumptionTrend>> RetrieveProductConsumptionTrend(ProductConsumptionTrendDTO productConsumptionTrendDTO)
        {
            string actionURI = "ProductConsumptionTrend";
            return await Utility.RetrieveAsync<TProductConsumptionTrend>(actionURI, productConsumptionTrendDTO);
        }

        /// <summary>
        /// Returns the wholeseller with the latest purchase price quoted by each of the Wholeseller
        /// for the given productID.
        /// This will guide him to choose best wholeseller for its product
        /// </summary>
        /// <param name="ProductId"></param>
        public static async Task<List<TPriceQuotedBySupplier>> RetrieveLatestPriceQuotedBySupplierAsync(Guid productId)
        {
            string actionURI = "PriceQuotedBySupplier/" + productId.ToString();
            return await Utility.RetrieveAsync<TPriceQuotedBySupplier>(actionURI, null);
        }
    }
}
