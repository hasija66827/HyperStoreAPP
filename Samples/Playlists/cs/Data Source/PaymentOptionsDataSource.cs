using SDKTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.Data_Source
{
    class PaymentOptionsDataSource
    {
        public static async Task<List<PaymentOption>> RetrievePaymentOptionsAsync()
        {
            return await Utility.RetrieveAsync<List<PaymentOption>>(BaseURI.HyperStoreService + API.PaymentOptions, null, null);
        }
    }
}
