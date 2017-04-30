﻿using System;
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
        public IRange(T lb, T ub){
            LB = lb;
            UB = ub;
        }
    }
    class ProductDataSource
    {
        private static List<ProductViewModel> _products = new List<ProductViewModel>();
        public static List<ProductViewModel> Products { get { return _products; } }
        public static void RetrieveProductDataAsync()
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                // Retrieving data from the database synchronously.
                _products = db.Products.Select(product => new ProductViewModel(
                      product.ProductId,
                      product.BarCode,
                      product.Name,
                      product.DisplayPrice,
                      product.DiscountPer,
                      product.Threshold )).ToList();

            }
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on product name or barcode
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of Product that matches the query</returns>
        public static IEnumerable<ProductViewModel> GetMatchingProducts(string query)
        {
            return ProductDataSource._products
                .Where(p => p.BarCode.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1 ||
                            p.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(c => c.BarCode.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c.Name.StartsWith(query, StringComparison.CurrentCultureIgnoreCase));
        }
        public static List<ProductViewModel> GetProducts(FilterProductCriteria filterProductCriteria)
        {
            // TODO: filter By Quantity and threshold
            if (filterProductCriteria.IncludeDeficientItemsOnly == true)
            {
                return ProductDataSource._products
                .Where(p => p.DiscountPer >= filterProductCriteria.DiscountPerRange.LB &&
                          p.DiscountPer <= filterProductCriteria.DiscountPerRange.UB &&
                          1<=p.Threshold).ToList();
            }
            else
            {
                return ProductDataSource._products
                .Where(p => p.DiscountPer >= filterProductCriteria.DiscountPerRange.LB &&
                      p.DiscountPer <= filterProductCriteria.DiscountPerRange.UB).ToList();
            }

        }

    }
}
