﻿using System;
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
        public static event SupplierEntityChangedDelegate SupplierBalanceUpdatedEvent;
        public static event ProductEntityChangedDelegate ProductStockUpdatedEvent;
        #region Create
        public static async Task<decimal?> CreateSupplierOrderAsync(SupplierOrderDTO supplierOrderDTO)
        {
            var deductedWalletAmount = await Utility.CreateAsync<decimal?>(BaseURI.HyperStoreService + API.SupplierOrders, supplierOrderDTO);
            if (deductedWalletAmount != null)
            {
                SupplierBalanceUpdatedEvent?.Invoke();
                ProductStockUpdatedEvent?.Invoke();
                //TODO: Send success notification
            }
            return deductedWalletAmount;
        }
        #endregion

        #region Read
        public static async Task<List<TSupplierOrder>> RetrieveSupplierOrdersAsync(SupplierOrderFilterCriteriaDTO sofc)
        {
            List<TSupplierOrder> supplierOrders = await Utility.RetrieveAsync<List<TSupplierOrder>>(BaseURI.HyperStoreService + API.SupplierOrders, null, sofc);
            return supplierOrders;
        }

        public static async Task<List<TSupplierOrderProduct>> RetrieveOrderDetailsAsync(Guid supplierOrderId)
        {
            List<TSupplierOrderProduct> orderDetails = await Utility.RetrieveAsync<List<TSupplierOrderProduct>>(BaseURI.HyperStoreService + API.SupplierOrderProducts, supplierOrderId.ToString(), null);
            return orderDetails;
        }
        #endregion
    }
}