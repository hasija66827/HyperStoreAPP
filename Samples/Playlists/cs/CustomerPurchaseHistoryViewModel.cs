using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class CustomerPurchaseHistoryViewModel
    {
        Int32 BarCode;
        Guid productName;
        Guid TotalQuantity;
        public CustomerPurchaseHistoryViewModel() { }
    }
    /// <summary>
    /// This class is used by Detail Page of Customer CCF.
    /// </summary>
    public class CustomerPurchaseHistoryCollection
    {
        private List<CustomerPurchaseHistoryViewModel> _customerPurchaseHistories;
        public List<CustomerPurchaseHistoryViewModel> CustomerPurchaseHistories
        {
            get { return this._customerPurchaseHistories; }
            set { this._customerPurchaseHistories = value; }
        }
        public CustomerPurchaseHistoryCollection() { }
    }

}
