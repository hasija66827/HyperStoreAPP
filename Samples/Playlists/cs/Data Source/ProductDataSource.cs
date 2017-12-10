using HyperStoreService.CustomModels;
using HyperStoreServiceAPP.DTO;
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
    public delegate void ProductEntityChangedDelegate();
    class ProductDataSource
    {
        public static event ProductEntityChangedDelegate ProductCreatedEvent;
        #region Create
        public static async Task<Product> CreateNewProductAsync(ProductDTO productDTO)
        {
            var product = await Utility.CreateAsync<Product>(BaseURI.HyperStoreService + API.Products, productDTO);
            if (product != null)
            {
                var message = String.Format("You should update the stock of the product {0} ({1}) as the quantity is {2} right now.", product.Name, product.Code, product.TotalQuantity);
                ProductCreatedEvent?.Invoke();
                SuccessNotification.PopUpHttpPostSuccessNotification(API.Products, message);
            }
            return product;
        }
        #endregion

        #region Read
        public static async Task<List<ProductInsight>> RetrieveProductsAsync(ProductFilterCriteriaDTO pfc)
        {
            var products = await Utility.RetrieveAsync<List<ProductInsight>>(BaseURI.HyperStoreService + API.Products, null, pfc);
            return products;
        }

        public static async Task<Product> RetrieveTheProductAsync(Guid? productId)
        {
            var product = await Utility.RetrieveAsync<Product>(BaseURI.HyperStoreService + API.Products, productId.ToString(), null);
            return product;
        }

        public static async Task<Int32> RetrieveTotalProducts()
        {
            Int32 totalProducts = await Utility.RetrieveAsync<Int32>(BaseURI.HyperStoreService + API.Products, CustomAction.GetTotalRecordsCount, null);
            return totalProducts;
        }

        public static async Task<ProductMetadata> RetrieveProductMetadataAsync()
        {
           var productMetadata = await Utility.RetrieveAsync<ProductMetadata>(BaseURI.HyperStoreService + API.Products, "GetProductMetadata", null);
            return productMetadata;
        }
        #endregion
    }
}
