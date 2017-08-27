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
    public class CustomerPurchaseTrendCollection
    {
        private List<TCustomerPurchaseTrend> _customerPurchaseTrends;
        public List<TCustomerPurchaseTrend> CustomerPurchaseTrends
        {
            get { return this._customerPurchaseTrends; }
            set { this._customerPurchaseTrends = value; }
        }
        public CustomerPurchaseTrendCollection() { }
    }
}
