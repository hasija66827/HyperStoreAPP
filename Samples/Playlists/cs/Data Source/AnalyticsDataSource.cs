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
        public static async Task<List<CustomerPurchaseTrend>> RetrieveCustomerPurchaseTrend(CustomerPurchaseTrendDTO customerPurchaseTrendDTO)
        {
            return await Utility.RetrieveAsync<List<CustomerPurchaseTrend>>(BaseURI.HyperStoreService + API.CustomerPurchaseTrend, null, customerPurchaseTrendDTO);
        }

        public static async Task<List<ProductConsumptionDeficientTrend>> RetrieveProductConsumptionTrend(ProductConsumptionTrendDTO productConsumptionTrendDTO)
        {
            return await Utility.RetrieveAsync<List<ProductConsumptionDeficientTrend>>(BaseURI.HyperStoreService + API.ProductConsumptionTrend, null, productConsumptionTrendDTO);
        }

        /// <summary>
        /// Returns the wholeseller with the latest purchase price quoted by each of the Wholeseller
        /// for the given productID.
        /// This will guide him to choose best wholeseller for its product
        /// </summary>
        /// <param name="ProductId"></param>
        public static async Task<List<PriceQuotedBySupplier>> RetrieveLatestPriceQuotedBySupplierAsync(Guid productId)
        {
            return await Utility.RetrieveAsync<List<PriceQuotedBySupplier>>(BaseURI.HyperStoreService + API.PriceQuotedBySupplier, productId.ToString(), null);
        }

        public static async Task<List<RecommendedProduct>> RetrieveRecommendedProductAsync(Guid customerId)
        {
            return await Utility.RetrieveAsync<List<RecommendedProduct>>(BaseURI.HyperStoreService + API.RecommendedProducts, customerId.ToString(), null);
        }

    }
}
