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
        public static async Task<decimal> CreateSupplierOrderAsync(SupplierOrderDTO supplierOrderDTO)
        {
            string actionURI = API.SupplierOrders;
            return await Utility.CreateAsync<decimal>(actionURI, supplierOrderDTO);
        }
        #endregion

        #region Read
        public static async Task<List<TSupplierOrder>> RetrieveSupplierOrdersAsync(SupplierOrderFilterCriteriaDTO sofc)
        {
            List<TSupplierOrder> supplierOrders = await Utility.RetrieveAsync<TSupplierOrder>(API.SupplierOrders, null, sofc);
            return supplierOrders;
        }

        public static async Task<List<TSupplierOrderProduct>> RetrieveOrderDetailsAsync(Guid supplierOrderId)
        {
            List<TSupplierOrderProduct> orderDetails = await Utility.RetrieveAsync<TSupplierOrderProduct>(API.SupplierOrderProducts, supplierOrderId.ToString(), null);
            return orderDetails;
        }
        #endregion
    }
}