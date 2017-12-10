using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyperStoreServiceAPP.DTO.InsightsDTO;
using Models;
using HyperStoreService.CustomModels;

namespace SDKTemplate.Data_Source
{
    class InsightsDataSource
    {
        public static async Task<BusinessInsight> RetrieveBusinessInsight(BusinessInsightDTO businessInsightDTO)
        {
            return await Utility.RetrieveAsync<BusinessInsight>(BaseURI.HyperStoreService + API.BusinessInsight + "/" + CustomAction.GetBusinessInsight, null, businessInsightDTO);
        }

        public static async Task<SusceptibleProductsInsight> RetrieveSusceptibleProducts(SusceptibleProductsInsightDTO susceptibleProductsInsightDTO)
        {
            return await Utility.RetrieveAsync<SusceptibleProductsInsight>(BaseURI.HyperStoreService + API.ProductInsights + "/" + CustomAction.GetSusceptibleProducts, null, susceptibleProductsInsightDTO);
        }

        public static async Task<NewCustomerInsights> RetreiveNewCustomers(CustomerInsightsDTO customerInsightsDTO)
        {
            return await Utility.RetrieveAsync<NewCustomerInsights>(BaseURI.HyperStoreService + API.CustomerInsights + "/" + CustomAction.GetNewCustomers, null, customerInsightsDTO);
        }

        public static async Task<DetachedCustomerInsights> RetreiveDetachedCustomer(CustomerInsightsDTO customerInsightsDTO)
        {
            return await Utility.RetrieveAsync<DetachedCustomerInsights>(BaseURI.HyperStoreService + API.CustomerInsights + "/" + CustomAction.GetDetachedCustomers, null, customerInsightsDTO);
        }

        public static async Task<MapDay_ProductEstConsumption> RetrieveProductConsumptionTrend(Guid productId)
        {
            return await Utility.RetrieveAsync<MapDay_ProductEstConsumption>(BaseURI.HyperStoreService + API.ProductConsumptionInsights, productId.ToString(), null);
        }
    }
}
