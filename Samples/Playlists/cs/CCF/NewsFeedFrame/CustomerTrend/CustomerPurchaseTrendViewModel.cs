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
        public string CustomerName { get; set; }
        private decimal _NetWorthOfCustomer
        {
            get
            {
                if (CustomerPurchaseTrends== null)
                    return 0;
                else
                    return CustomerPurchaseTrends.Sum(cpt => cpt.NetValue);
            }
        }
        public string FormattedNetWorth { get { return String.Format("Net Worth of {0} is \u20b9 {1}", CustomerName, _NetWorthOfCustomer); } }
        public CustomerPurchaseTrendCollectionViewModel() { }
    }
}
