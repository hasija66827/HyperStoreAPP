using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using SDKTemplate.DTO;

namespace SDKTemplate
{
    public class SupplierOrderDataSource
    {
        #region Create
        public static async Task<decimal?> CreateSupplierOrderAsync(SupplierOrderDTO supplierOrderDTO)
        {
            var x= await Utility.CreateAsync<decimal?>(API.SupplierOrders, supplierOrderDTO);
            return x;
        }
        #endregion

        #region Read
        public static async Task<List<TSupplierOrder>> RetrieveSupplierOrdersAsync(SupplierOrderFilterCriteriaDTO sofc)
        {
            List<TSupplierOrder> supplierOrders = await Utility.RetrieveAsync<TSupplierOrder>(BaseURI.HyperStoreService, API.SupplierOrders, null, sofc);
            return supplierOrders;
        }

        public static async Task<List<TSupplierOrderProduct>> RetrieveOrderDetailsAsync(Guid supplierOrderId)
        {
            List<TSupplierOrderProduct> orderDetails = await Utility.RetrieveAsync<TSupplierOrderProduct>(BaseURI.HyperStoreService, API.SupplierOrderProducts, supplierOrderId.ToString(), null);
            return orderDetails;
        }
        #endregion
    }
}