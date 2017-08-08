using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class KeyEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, object> keyExtractor;

        public KeyEqualityComparer(Func<T, object> keyExtractor)
        {
            this.keyExtractor = keyExtractor;
        }

        public bool Equals(T x, T y)
        {
            return this.keyExtractor(x).Equals(this.keyExtractor(y));
        }

        public int GetHashCode(T obj)
        {
            return this.keyExtractor(obj).GetHashCode();
        }
    }

    class ProductDataSource
    {
        private static List<ProductViewModelBase> _products = new List<ProductViewModelBase>();
        public static List<ProductViewModelBase> Products { get { return _products; } }

        #region Read
        public static void RetrieveProductDataAsync()
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                // Retrieving data from the database synchronously.
                var dbProduct=db.Products.ToList();
                _products = dbProduct.Select(product => new ProductViewModelBase(product)).ToList();
            }
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on product name or barcode
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of Product that matches the query</returns>
        public static IEnumerable<ProductViewModelBase> GetMatchingProducts(string query)
        {
            return ProductDataSource._products
                .Where(p => p.BarCode.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1 ||
                            p.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(c => c.BarCode.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c.Name.StartsWith(query, StringComparison.CurrentCultureIgnoreCase));
        }

        public static List<ProductViewModelBase> GetProductsById(Guid? productId)
        {
            return ProductDataSource._products
                .Where(p => p.ProductId == productId).ToList();
        }

        public static Int32 GetMaximumQuantity()
        {
            if (_products.Count == 0)
                return 0;
            return ProductDataSource._products.Max(p => p.TotalQuantity);
        }

        public static List<ProductViewModelBase> GetFilteredProducts(FilterProductCriteria filterProductCriteria, Guid? productId, List<Guid?> tagIds)
        {
            List<ProductViewModelBase> result;

            if (productId == null)
                result = ProductDataSource._products;
            else
                result = ProductDataSource.GetProductsById(productId);

            if (filterProductCriteria != null)
            {
                if (filterProductCriteria.IncludeDeficientItemsOnly == true)
                {
                    result = result
                            .Where(p => p.DiscountPer >= filterProductCriteria.DiscountPerRange.LB &&
                              p.DiscountPer <= filterProductCriteria.DiscountPerRange.UB &&
                              p.TotalQuantity >= filterProductCriteria.QuantityRange.LB &&
                              p.TotalQuantity <= filterProductCriteria.QuantityRange.UB &&
                              p.TotalQuantity <= p.Threshold
                              ).ToList();
                }
                else
                {
                    result = result
                            .Where(p => p.DiscountPer >= filterProductCriteria.DiscountPerRange.LB &&
                              p.DiscountPer <= filterProductCriteria.DiscountPerRange.UB &&
                              p.TotalQuantity >= filterProductCriteria.QuantityRange.LB &&
                              p.TotalQuantity <= filterProductCriteria.QuantityRange.UB).ToList();
                }
            }
       
            if (tagIds != null && tagIds.Count!=0)
            {
                var tagProductIds = TagProductDataSource.RetrieveProductId(tagIds);
                result = result
                    .Where(res => tagProductIds.Contains(res.ProductId))
                    .ToList();
            }

            return result;

        }

        public static bool IsProductCodeExist(string barCode)
        {
            var products = ProductDataSource._products
                .Where(p => p.BarCode == barCode);
            if (products.Count() == 0)
                return false;
            return true;
        }

        public static bool IsProductNameExist(string name)
        {
            var products = ProductDataSource._products
                .Where(p => p.Name.ToLower() == name.ToLower());
            if (products.Count() == 0)
                return false;
            return true;
        }
        #endregion

        #region Create
        public static bool CreateNewProduct(ProductDetailViewModel productViewModel)
        {
            var db = new DatabaseModel.RetailerContext();
            db.Products.Add(new DatabaseModel.Product(productViewModel));
            db.SaveChanges();
            _products.Add(productViewModel);
            return true;
        }

        public static List<ProductListToPurchaseViewModel> RetreiveProductListToPurchaseByRespectiveWholeSellers()
        {
            var items = new List<ProductListToPurchaseViewModel>();
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
            }
            return items;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update the detail of the product in product entity.
        /// </summary>
        /// <param name="addProductViewModel"></param>
        /// <returns></returns>
        public static bool UpdateProductDetails(ProductDetailViewModel addProductViewModel)
        {
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
            ProductDataSource._products.Remove(prd);
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
                product.TotalQuantity -= productViewModel.QuantityPurchased;
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
