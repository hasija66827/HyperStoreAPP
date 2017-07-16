using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class FilterProductCriteria
    {
        public IRange<float> DiscountPerRange { get; set; }
        public IRange<Int32> QuantityRange { get; set; }
        public bool IncludeDeficientItemsOnly { get; set; }

        public FilterProductCriteria(IRange<float> discounPerRange, IRange<Int32> quantityRange, bool? isChecked)
        {
            this.DiscountPerRange = discounPerRange;
            this.QuantityRange = quantityRange;
            this.IncludeDeficientItemsOnly = isChecked ?? false;
        }
    }
    public class IRange<T>
    {
        public T LB { get; set; }
        public T UB { get; set; }
        public IRange(T lb, T ub)
        {
            LB = lb;
            UB = ub;
        }
    }
    class ProductDataSource
    {
        private static List<ProductViewModelBase> _products = new List<ProductViewModelBase>();
        public static List<ProductViewModelBase> Products { get { return _products; } }
        public static void RetrieveProductDataAsync()
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                // Retrieving data from the database synchronously.
                _products = db.Products.Select(product => new ProductViewModelBase(
                      product.ProductId,
                      product.BarCode,
                      product.Name,
                      product.DisplayPrice,
                      product.DiscountPer,
                      product.Threshold,
                      product.TotalQuantity,
                      product.WholeSellerId)).ToList();
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
            return ProductDataSource._products.Max(p => p.TotalQuantity);
        }

        public static List<ProductViewModelBase> GetFilteredProducts(FilterProductCriteria filterProductCriteria, Guid? productId)
        {
            List<ProductViewModelBase> result;

            if (productId == null)
                result = ProductDataSource._products;
            else
                result = ProductDataSource.GetProductsById(productId);

            if (filterProductCriteria == null)
                return result;
            else
            {
                if (filterProductCriteria.IncludeDeficientItemsOnly == true)
                {
                    return result
                    .Where(p => p.DiscountPer >= filterProductCriteria.DiscountPerRange.LB &&
                              p.DiscountPer <= filterProductCriteria.DiscountPerRange.UB &&
                              p.TotalQuantity >= filterProductCriteria.QuantityRange.LB &&
                              p.TotalQuantity <= filterProductCriteria.QuantityRange.UB &&
                              p.TotalQuantity <= p.Threshold).ToList();
                }
                else
                {
                    return result
                    .Where(p => p.DiscountPer >= filterProductCriteria.DiscountPerRange.LB &&
                              p.DiscountPer <= filterProductCriteria.DiscountPerRange.UB &&
                              p.TotalQuantity >= filterProductCriteria.QuantityRange.LB &&
                              p.TotalQuantity <= filterProductCriteria.QuantityRange.UB).ToList();
                }
            }
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

        public static bool AddProduct(AddProductViewModel productViewModelBase)
        {
            var db = new DatabaseModel.RetailerContext();
            db.Products.Add(new DatabaseModel.Product(
                productViewModelBase.ProductId,
                productViewModelBase.Name,
                productViewModelBase.BarCode,
                false,
                productViewModelBase.Threshold,
                productViewModelBase.RefillTime,
                productViewModelBase.DisplayPrice,
                productViewModelBase.DiscountPer,
                productViewModelBase.TotalQuantity
                ));
            db.SaveChanges();
            _products.Add(productViewModelBase);
            return true;
        }


        public static bool UpdateProductDetails(AddProductViewModel addProductViewModel)
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

            var prd =_products.Where(p => p.ProductId == addProductViewModel.ProductId).FirstOrDefault();
            ProductDataSource._products.Remove(prd);
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

        public static bool UpdateProductStock(DatabaseModel.RetailerContext db, ProductListViewModel billingViewModel)
        {
            //#perf: You can query whole list in where clause.
            foreach (var productViewModel in billingViewModel.Products)
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


        // Step 2:
        public static bool UpdateProductStockByWholeSeller(DatabaseModel.RetailerContext db, List<WholeSellerProductListVieModel> purchasedProducts)
        {
            //#perf: You can query whole list in where clause.
            foreach (var purchasedProduct in purchasedProducts)
            {
                //TODO: check where clouse whether threough id or bar code.
                var products = db.Products.Where(p => p.BarCode == purchasedProduct.BarCode).ToList();
                var product = products.FirstOrDefault();
                if (product == null)
                    return false;
                product.TotalQuantity += purchasedProduct.QuantityPurchased;
                db.Update(product);
            }
            db.SaveChanges();
            return true;
        }

        public static bool UpdateWholSellerIdForProduct(Guid productId, Guid? wholeSellerId)
        {
            var db = new DatabaseModel.RetailerContext();
            var products = db.Products.Where(p => p.ProductId == productId).ToList();
            var product = products.FirstOrDefault();
            if (product == null)
                return false;
            product.WholeSellerId = wholeSellerId;
            db.Update(product);
            db.SaveChanges();
            return true;
        }
    }
}
