using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class PriceQuotedByWholeSeller
    {
        public Guid? wholeSellerId;
        public DateTime orderDate;
        public Guid? productId;
        public float purchasePrice;

        public PriceQuotedByWholeSeller(Guid? wholeSellerId, DateTime orderDate, Guid? productId, float purchasePrice)
        {
            this.wholeSellerId = wholeSellerId;
            this.orderDate = orderDate;
            this.productId = productId;
            this.purchasePrice = purchasePrice;
        }

        public PriceQuotedByWholeSeller()
        {
            this.wholeSellerId = null;
            this.orderDate = DateTime.Now.AddYears(-100);
            this.productId = null;
            this.purchasePrice = 0;
        }
    }

    public class AnalyticsDataSource
    {
        /// <summary>
        /// Computes the distinct products purchased by the customer from all the orders in last months
        /// </summary>
        /// <param name="CustomerId">The id of the customer</param>
        /// <param name="months">History in number of months</param>
        /// <returns>returns the ordered list on the TotalQuantity </returns>
        public static List<CustomerPurchaseHistoryViewModel> GetPurchasedProductForCustomer(Guid CustomerId, int months)
        {
            var db = new DatabaseModel.RetailerContext();
            var customerOrders = db.CustomerOrders
                .Where(co => co.CustomerId == CustomerId
                            &&co.OrderDate >= DateTime.Now.AddMonths(-months)).ToList();
            var customerOrderProduct = db.CustomerOrderProducts.ToList();
            var joinRes = customerOrderProduct
                    .Join(customerOrders,
                    cop => cop.CustomerOrderId,
                    co => co.CustomerOrderId,
                    (cop, co) => new CustomerPurchaseHistoryViewModel(cop.ProductId, cop.QuantityPurchased));
            var products_quantities = joinRes.GroupBy(cphv => cphv.ProductId);
            var product_totalQuantity = products_quantities.Select(cphv => AggregateQuantity(cphv)).ToList();
            return product_totalQuantity;
        }

        private static CustomerPurchaseHistoryViewModel AggregateQuantity(IGrouping<Guid?, CustomerPurchaseHistoryViewModel> items)
        {
            return new CustomerPurchaseHistoryViewModel(items.Key, items.Sum(p => p.TotalQuantity));
        }

        /// <summary>
        /// Returns the wholeseller with the latest purchase price quoted by each of the Wholeseller
        /// for the given productID.
        /// This will guide him to choose best wholeseller for its product
        /// </summary>
        /// <param name="ProductId"></param>
        public static List<PriceQuotedByWholeSeller> GetWholeSellersForProduct(Guid ProductId)
        {
            var db = new DatabaseModel.RetailerContext();
            var wholeSellerOrderProducts = db.WholeSellersOrderProducts
                .Where(wholeSellerOrderProduct => wholeSellerOrderProduct.ProductId == ProductId).ToList();
            var wholeSellerOrders = db.WholeSellersOrders.ToList();
            var ret = wholeSellerOrderProducts.Join(wholeSellerOrders,
                wop => wop.WholeSellerOrderId,
                wo => wo.WholeSellerOrderId,
                (wop, wo) => new PriceQuotedByWholeSeller(wo.WholeSellerId, wo.OrderDate,
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
        private static PriceQuotedByWholeSeller SelectLatestPriceQuoted(IGrouping<Guid?, PriceQuotedByWholeSeller> items)
        {
            DateTime maxOrderDate=DateTime.Now.AddYears(-100);
            PriceQuotedByWholeSeller ret=new PriceQuotedByWholeSeller();
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
