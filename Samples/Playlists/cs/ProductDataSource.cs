using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
namespace SDKTemplate
{
    class ProductDataSource
    {
        private static List<Product> _products = new List<Product>();
        public static void RetrieveProductDataAsync()
        {
            // Don't need to do this more than once.
            if (_products.Count > 0)
            {
                return;
            }
            using (var db = new RetailerContext())
            {

                db.Products.ToList();  
             
            }
            _products.Add(new Product("4333", "shampoo", 200, 200));
            _products.Add(new Product("1111", "Maggi", 12, 2));
            _products.Add(new Product("2222", "Ghee", 310, 10));
            _products.Add(new Product("3333", "Rice", 520, 20));
            _products.Add(new Product("5454", "wheat flour", 605, 4));
            _products.Add(new Product("9898", "patanjali aloe vera", 210, 110));
            _products.Add(new Product("7878", "cashew and almonds", 1000, 100));
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on a pre-defined rule set
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of Product that matches the query</returns>
        public static IEnumerable<Product> GetMatchingProducts(string query)
        {
            return ProductDataSource._products
                .Where(p => p.Id.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1 ||
                            p.Name.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(c => c.Id.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c.Name.StartsWith(query, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
