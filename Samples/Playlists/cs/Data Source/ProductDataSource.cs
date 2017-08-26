using Models;
using Newtonsoft.Json;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class ProductDataSource
    {
        #region Create
        public static async Task<bool> CreateNewProductAsync(ProductDTO productDTO)
        {
            try
            {
                string actionURI = "products";
                var content = JsonConvert.SerializeObject(productDTO);
                var response = await Utility.HttpPost(actionURI, content);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region Read
        public static async Task<List<TProduct>> RetrieveProductDataAsync(ProductFilterCriteriaDTO pfc)
        {
            string actionURI = "products";
            List<TProduct> products = await Utility.Retrieve<TProduct>(actionURI, pfc);
            return products;
        }

        public static List<ProductViewModelBase> GetProductsById(Guid? productId)
        {
            return null;
        }

        public static double GetMaximumQuantity()
        {

            return 90000;
        }

        public static bool IsProductCodeExist(string barCode)
        {

            return false;

        }

        public static bool IsProductNameExist(string name)
        {

            return false;

        }
        #endregion


        public static List<ProductListToPurchaseViewModel> RetreiveProductListToPurchaseByRespectiveWholeSellers()
        {
            var items = new List<ProductListToPurchaseViewModel>();
            /*
            var db = new DatabaseModel.RetailerContext();
            var groups = db.Products.GroupBy(p => p.WholeSellerId);
            foreach (var group in groups)
            {
                var item = new ProductListToPurchaseViewModel();
                item.WholeSellerViewModel = WholeSellerDataSource.GetWholeSellerById(group.Key);
                if (item.WholeSellerViewModel == null)
                {
                    item.WholeSellerViewModel = new WholeSellerViewModel();
                    item.WholeSellerViewModel.Name = "Not In Cart";
                }
                item.Products = group.Select(p => new ProductViewModelBase(p)).ToList();
                items.Add(item);
            }*/
            return items;
        }

        #region Update
        /// <summary>
        /// Update the detail of the product in product entity.
        /// </summary>
        /// <param name="addProductViewModel"></param>
        /// <returns></returns>
        public static bool UpdateProductDetails(object addProductViewModel)
        {
            /*
            var db = new DatabaseModel.RetailerContext();
            var products = db.Products.Where(p => p.ProductId == addProductViewModel.ProductId).ToList();
            var product = products.FirstOrDefault();
            if (product == null)
                return false;
            product.Threshold = addProductViewModel.Threshold;
            product.RefillTime = addProductViewModel.RefillTime;
            product.DiscountPer = addProductViewModel.DiscountPer;
            product.DisplayPrice = addProductViewModel.DisplayPrice;
            db.Update(product);
            db.SaveChanges();
            ProductDataSource._products.Add(new ProductViewModelBase(product));

            var prd = _products.Where(p => p.ProductId == addProductViewModel.ProductId).FirstOrDefault();
            ProductDataSource._products.Remove(prd);*/
            return true;
        }

        ///#remove
        /// <summary>
        /// Reduces the number of products from the product entity during purchase of product by Customer.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="productListToBePurchased"></param>
        /// <returns></returns>
        public static bool UpdateProductStock(DatabaseModel.RetailerContext db, List<CustomerProductViewModel> productListToBePurchased)
        {
            //#perf: You can query whole list in where clause.
            foreach (var productViewModel in productListToBePurchased)
            {
                var products = db.Products.Where(p => p.ProductId == productViewModel.ProductId).ToList();
                var product = products.FirstOrDefault();
                if (product == null)
                    return false;
               
                db.Update(product);
            }
            db.SaveChanges();
            return true;
        }


        /// <summary>
        /// Adds the quantity in product present in product entity, during purchase of product by wholeseller.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="purchasedProducts"></param>
        /// <returns></returns>
        public static bool UpdateProductStockByWholeSeller(DatabaseModel.RetailerContext db, List<WholeSellerProductVieModel> purchasedProducts)
        {
            //#perf: You can query whole list in where clause.
            foreach (var purchasedProduct in purchasedProducts)
            {
                //TODO: check where clouse whether threough id or bar code.
                var products = db.Products
                                .Where(p => p.ProductId == purchasedProduct.ProductId).ToList();
                var product = products.FirstOrDefault();
                if (product == null)
                    throw new Exception(string.Format("Product {0} not found while updating the product", product.Name));
                product.TotalQuantity += purchasedProduct.QuantityPurchased;
                db.Update(product);
            }
            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// Updates the product with the wholeseller from which we have to purchase the product.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="wholeSellerId"></param>
        /// <returns></returns>
        public static bool UpdateWholSellerIdForProduct(Guid productId, Guid? wholeSellerId)
        {
            var db = new DatabaseModel.RetailerContext();
            var products = db.Products.Where(p => p.ProductId == productId).ToList();
            var product = products.FirstOrDefault();
            if (product == null)
                throw new Exception(string.Format("Product {0} not found while updating the product", product.Name));
            product.WholeSellerId = wholeSellerId;
            db.Update(product);
            db.SaveChanges();
            return true;
        }
        #endregion
    }
}
