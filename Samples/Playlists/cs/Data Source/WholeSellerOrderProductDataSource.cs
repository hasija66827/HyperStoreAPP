using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SDKTemplate
{
    public class WholeSellerOrderProductDataSource
    {
        public static List<WholeSellerOrderDetail> RetrieveOrderDetails(Guid? wholeSellerOrderId)
        {
            var db = new DatabaseModel.RetailerContext();
            var ret = db.WholeSellersOrderProducts
                        .Where(wop => wop.WholeSellerOrderId == wholeSellerOrderId);
            var product_wop = ret.Include(p => p.Product);
            var wholeSellerOrderDetails=
                product_wop.Select(wop => new WholeSellerOrderDetail(wop.Product.BarCode, wop.Product.Name, wop.PurchasePrice, wop.QuantityPurchased));
            return wholeSellerOrderDetails.ToList();
        }
    }
}
