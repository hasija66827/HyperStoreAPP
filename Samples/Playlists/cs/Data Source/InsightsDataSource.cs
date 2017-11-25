using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyperStoreServiceAPP.DTO.InsightsDTO;

namespace SDKTemplate.Data_Source
{
    class InsightsDataSource
    {
        public static async Task<SusceptibleProductsInsight> RetrieveSusceptibleProducts(SusceptibleProductsInsightDTO susceptibleProductsInsightDTO)
        {
            return await Utility.RetrieveAsync<SusceptibleProductsInsight>(BaseURI.HyperStoreService + API.ProductInsights + "/" + CustomAction.GetSusceptibleProducts, null, susceptibleProductsInsightDTO);
        }
    }
}
