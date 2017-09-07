﻿using System;
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
            return await Utility.RetrieveAsync<TCustomerPurchaseTrend>(API.CustomerPurchaseTrend, null, customerPurchaseTrendDTO);
        }

        public static async Task<List<TProductConsumptionDeficientTrend>> RetrieveProductConsumptionTrend(ProductConsumptionTrendDTO productConsumptionTrendDTO)
        {
            return await Utility.RetrieveAsync<TProductConsumptionDeficientTrend>(API.ProductConsumptionTrend, null, productConsumptionTrendDTO);
        }

        /// <summary>
        /// Returns the wholeseller with the latest purchase price quoted by each of the Wholeseller
        /// for the given productID.
        /// This will guide him to choose best wholeseller for its product
        /// </summary>
        /// <param name="ProductId"></param>
        public static async Task<List<TPriceQuotedBySupplier>> RetrieveLatestPriceQuotedBySupplierAsync(Guid productId)
        {
            return await Utility.RetrieveAsync<TPriceQuotedBySupplier>(API.PriceQuotedBySupplier, productId.ToString(), null);
        }

        public static async Task<List<TRecommendedProduct>> RetrieveRecommendedProductAsync(Guid customerId)
        {
            return await Utility.RetrieveAsync<TRecommendedProduct>(API.RecommendedProducts, customerId.ToString(), null);
        }

    }
}
