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
            var product = await Utility.CreateAsync<TProduct>(API.Products, productDTO);
            if (product != null)
            {
                var message = String.Format("You should Update the stock of the product {0} ({1}) as the Quantity is {2} right now.", product.Name, product.Code, product.TotalQuantity);
                SuccessNotification.PopUpSuccessNotification(API.Products, message);
            }
            return product;
        }
        #endregion

        #region Read
        public static async Task<List<TProduct>> RetrieveProductDataAsync(ProductFilterCriteriaDTO pfc)
        {
            List<TProduct> products = await Utility.RetrieveAsync<TProduct>(API.Products, null, pfc);
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
