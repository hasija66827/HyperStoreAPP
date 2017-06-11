using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class PriceQuotedByWholeSellerViewModel
    {
        public Guid? wholeSellerId;
        public DateTime orderDate;
        public Guid? productId;
        public float purchasePrice;

        public PriceQuotedByWholeSellerViewModel(Guid? wholeSellerId, DateTime orderDate, Guid? productId, float purchasePrice)
        {
            this.wholeSellerId = wholeSellerId;
            this.orderDate = orderDate;
            this.productId = productId;
            this.purchasePrice = purchasePrice;
        }

        public PriceQuotedByWholeSellerViewModel()
        {
            this.wholeSellerId = null;
            this.orderDate = DateTime.Now.AddYears(-100);
            this.productId = null;
            this.purchasePrice = 0;
        }
    }

    public class PriceQuotedByWholeSellerCollection
    {
        private List<PriceQuotedByWholeSellerViewModel> _priceQuotedByWholeSellers;
        public List<PriceQuotedByWholeSellerViewModel> PriceQuotedByWholeSellers
        {
            get { return this._priceQuotedByWholeSellers; }
            set
            {
                this._priceQuotedByWholeSellers = value;
            }
        }
        public PriceQuotedByWholeSellerCollection()
        {
        }

    }
}
