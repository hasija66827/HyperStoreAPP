using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
   public class AnalyticsDataSource
    {
        /// <summary>
        /// Returns the wholeseller with the latest purchase price quoted by each of the Wholeseller
        /// for the given productID.
        /// This will guide him to choose best wholeseller for its product
        /// </summary>
        /// <param name="ProductId"></param>
        public static List<PriceQuotedByWholeSellerViewModel> GetWholeSellersForProduct(Guid ProductId)
        {
            var db = new DatabaseModel.RetailerContext();
            var wholeSellerOrderProducts = db.WholeSellersOrderProducts
                .Where(wholeSellerOrderProduct => wholeSellerOrderProduct.ProductId == ProductId).ToList();
            var wholeSellerOrders = db.WholeSellersOrders.ToList();
            var ret = wholeSellerOrderProducts.Join(wholeSellerOrders,
                wop => wop.WholeSellerOrderId,
                wo => wo.WholeSellerOrderId,
                (wop, wo) => new PriceQuotedByWholeSellerViewModel(wo.WholeSellerId, wo.OrderDate,
                                                        wop.ProductId, wop.PurchasePrice)).ToList();
            
            var wholeSellers_purchasePrices = ret.GroupBy(w => w.wholeSellerId);
            var wholeSellers_purchasePrice = wholeSellers_purchasePrices.Select(w_ps => SelectLatestPriceQuoted(w_ps));
            return wholeSellers_purchasePrice.ToList();
        }
        /// <summary>
        /// Returns the latest price Quoted by 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static PriceQuotedByWholeSellerViewModel SelectLatestPriceQuoted(IGrouping<Guid?, PriceQuotedByWholeSellerViewModel> items)
        {
            DateTime maxOrderDate=DateTime.Now.AddYears(-100);
            PriceQuotedByWholeSellerViewModel ret=new PriceQuotedByWholeSellerViewModel();
            foreach (var item in items)
            {
                if (maxOrderDate < item.orderDate)
                {
                    maxOrderDate = item.orderDate;
                    ret = item;
                }
            }
            return ret;
        }
    }
}
