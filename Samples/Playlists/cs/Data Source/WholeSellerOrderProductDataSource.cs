﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SDKTemplate
{
    public class WholeSellerOrderProductDataSource
    {
        public static List<WholeSellerOrderDetailViewModel> RetrieveOrderDetails(Guid? wholeSellerOrderId)
        {
            var db = new DatabaseModel.RetailerContext();
            var ret = db.WholeSellersOrderProducts
                        .Where(wop => wop.WholeSellerOrderId == wholeSellerOrderId);
            var product_wop = ret.Include(p => p.Product);
            var wholeSellerOrderDetails=
                product_wop.Select(wop => new WholeSellerOrderDetailViewModel(wop.Product.BarCode, wop.Product.Name, wop.PurchasePrice, wop.QuantityPurchased));
            return wholeSellerOrderDetails.ToList();
        }
        // Step4:
        public static void CreateWholeSellerOrderProduct(DatabaseModel.RetailerContext db, List<WholeSellerProductVieModel> purchasedProducts, Guid wholeSellerOrderId)
        {
            foreach (var purchasedProduct in purchasedProducts)
            {
                // Adding each product purchased in the order into the Entity WholeSellerOrderProduct.
                var wholeSellerOrderProduct = new DatabaseModel.WholeSellerOrderProduct(
                    
                    );
                db.WholeSellersOrderProducts.Add(wholeSellerOrderProduct);
            }
            // Saving the order.
            db.SaveChanges();
        }
    }
}
