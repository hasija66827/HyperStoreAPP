using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    /// <summary>
    /// This class is used by Detail Page of Customer CCF.
    /// </summary>
    public sealed class CustomerPurchaseTrendCollectionViewModel
    {
        public List<TCustomerPurchaseTrend> CustomerPurchaseTrends { get; set; }
        public decimal NetWorthOfCustomer {
            get {
                if (CustomerPurchaseTrends.Count() == 0)
                    return 0;
                else
                    return CustomerPurchaseTrends.Sum(cpt => cpt.NetValue);
            } }
        public CustomerPurchaseTrendCollectionViewModel() { }
    }
}
