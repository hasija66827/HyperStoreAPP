using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SDKTemplate.Data_Source;
using Models;
using SDKTemplate.DTO;

namespace SDKTemplate
{
    public class SupplierOrderDataSource
    {
        #region Create
        public static async Task<decimal> CreateSupplierOrderAsync(SupplierOrderDTO supplierOrderDTO)
        {
            string actionURI = "supplierorders";
            return await Utility.CreateAsync<decimal>(actionURI, supplierOrderDTO);
        }
        #endregion

        #region Read
        public static async Task<List<TSupplierOrder>> RetrieveSupplierOrdersAsync(SupplierOrderFilterCriteriaDTO sofc)
        {
                string actionURI = "SupplierOrders";
                List<TSupplierOrder> supplierOrders = await Utility.RetrieveAsync<TSupplierOrder>(actionURI, sofc);
                return supplierOrders;            
        }

        public static async Task<List<TSupplierOrderProduct>> RetrieveOrderDetailsAsync(Guid supplierOrderId)
        {
            string actionURI = "SupplierOrderProducts/" + supplierOrderId.ToString();
            List<TSupplierOrderProduct> orderDetails = await Utility.RetrieveAsync<TSupplierOrderProduct>(actionURI, null);
            return orderDetails;
        }
        #endregion
    }
}