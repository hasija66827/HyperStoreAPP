
using Models;
using Newtonsoft.Json;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class SupplierDataSource
    {
        #region Create
        public static async Task<TSupplier> CreateNewSupplier(SupplierDTO supplierDTO)
        {
            var supplier = await Utility.CreateAsync<TSupplier>(BaseURI.HyperStoreService + API.Suppliers, supplierDTO);
            if (supplier != null)
            {
                var message = String.Format("You can Start placing Ordes to Supplier {0} ({1})", supplier.Name, supplier.MobileNo);
                SuccessNotification.PopUpSuccessNotification(API.Suppliers, message);
            }
            return supplier;
        }
        #endregion

        #region Update
        public static async Task<TSupplier> UpdateSupplierAsync(Guid supplierId, SupplierDTO supplierDTO)
        {
            var supplier = await Utility.UpdateAsync<TSupplier>(API.Suppliers, supplierId.ToString(), supplierDTO);
            if (supplier != null)
            {
                //TODO: Success Notification
            }
            return supplier;
        }

        #endregion

        #region Read
        public static async Task<List<TSupplier>> RetrieveSuppliersAsync(SupplierFilterCriteriaDTO sfc)
        {
            List<TSupplier> suppliers = await Utility.RetrieveAsync<TSupplier>(BaseURI.HyperStoreService, API.Suppliers, null, sfc);
            return suppliers;
        }

        public static async Task<IRange<T>> RetrieveWalletRangeAsync<T>()
        {
            var IRanges = await Utility.RetrieveAsync<IRange<T>>(BaseURI.HyperStoreService, API.Suppliers, "GetWalletBalanceRange", null);
            if (IRanges != null && IRanges.Count == 1)
                return IRanges[0];
            return null;
        }
        #endregion 
    }
}
