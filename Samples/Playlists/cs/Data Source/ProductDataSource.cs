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
        public static async Task<TProduct> CreateNewProductAsync(ProductDTO productDTO)
        {
            string actionURI = "products";
            var x = await Utility.CreateAsync<TProduct>(actionURI, productDTO);
            return x;
        }
        #endregion

        #region Read
        public static async Task<List<TProduct>> RetrieveProductDataAsync(ProductFilterCriteriaDTO pfc)
        {
            string actionURI = "products";
            List<TProduct> products = await Utility.RetrieveAsync<TProduct>(actionURI, pfc);
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
    }
}
