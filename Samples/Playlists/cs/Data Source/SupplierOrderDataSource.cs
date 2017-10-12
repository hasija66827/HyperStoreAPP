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
            MainPage.Current.ActivateProgressRing();
            var deductedWalletAmount = await Utility.CreateAsync<decimal?>(BaseURI.HyperStoreService + API.SupplierOrders, supplierOrderDTO);
            if (deductedWalletAmount != null)
            {
                //Notification is sent by the caller of this method.
            }
            MainPage.Current.DeactivateProgressRing();
            return deductedWalletAmount;
        }
        #endregion

        #region Read
        public static async Task<List<TSupplierOrder>> RetrieveSupplierOrdersAsync(SupplierOrderFilterCriteriaDTO sofc)
        {
            List<TSupplierOrder> supplierOrders = await Utility.RetrieveAsync<List<TSupplierOrder>>(BaseURI.HyperStoreService + API.SupplierOrders, null, sofc);
            return supplierOrders;
        }

        public static async Task<Int32> RetrieveTotalSupplierOrder()
        {
            Int32 totalOrders = await Utility.RetrieveAsync<Int32>(BaseURI.HyperStoreService + API.SupplierOrders, CustomAction.GetTotalRecordsCount, null);
            return totalOrders;
        }


        public static async Task<List<TSupplierOrderProduct>> RetrieveOrderDetailsAsync(Guid supplierOrderId)
        {
            List<TSupplierOrderProduct> orderDetails = await Utility.RetrieveAsync<List<TSupplierOrderProduct>>(BaseURI.HyperStoreService + API.SupplierOrderProducts, supplierOrderId.ToString(), null);
            return orderDetails;
        }
        #endregion
    }
}