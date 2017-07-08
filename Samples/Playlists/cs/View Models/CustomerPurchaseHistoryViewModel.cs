using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class CustomerPurchaseHistoryViewModel
    {
        public Guid? ProductId;
        public ProductViewModelBase ProductViewModelBase { get; set; }
        public Int32 TotalQuantity { get; set; }
        public CustomerPurchaseHistoryViewModel(Guid? productId, Int32 totalQuantity) {
            this.ProductId = productId;
            this.ProductViewModelBase = ProductDataSource.GetProductsById(productId).FirstOrDefault();
            this.TotalQuantity = totalQuantity;
        }
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
