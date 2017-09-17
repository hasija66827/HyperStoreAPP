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
            var product = await Utility.CreateAsync<TProduct>(BaseURI.HyperStoreService + API.Products, productDTO);
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
            List<TProduct> products = await Utility.RetrieveAsync<TProduct>(BaseURI.HyperStoreService, API.Products, null, pfc);
            return products;
        }

        public static List<ProductViewModelBase> GetProductsById(Guid? productId)
        {
            return null;
        }

        public static async Task<TProductMetadata> RetrieveProductMetadataAsync()
        {
            List<TProductMetadata> productMetadata = await Utility.RetrieveAsync<TProductMetadata>(BaseURI.HyperStoreService, API.Products, "GetProductMetadata", null);
            if (productMetadata != null && productMetadata.Count == 1)
                return productMetadata[0];
            return new TProductMetadata()
            {
                DiscountPerRange = new IRange<decimal?>(0, 100),
                QuantityRange = new IRange<decimal>(0, 1000),
            };
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
