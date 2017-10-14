
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
    public delegate void SupplierEntityChangedDelegate();
    class SupplierDataSource
    {
        public static event SupplierEntityChangedDelegate SupplierCreatedEvent;
        public static event SupplierEntityChangedDelegate SupplierUpdatedEvent;

        #region Create
        public static async Task<TSupplier> CreateNewSupplier(SupplierDTO supplierDTO)
        {
            var supplier = await Utility.CreateAsync<TSupplier>(BaseURI.HyperStoreService + API.Suppliers, supplierDTO);
            if (supplier != null)
            {
                var message = String.Format("You can Start placing Ordes to Supplier {0} ({1})", supplier.Name, supplier.MobileNo);
                SupplierCreatedEvent?.Invoke();
                SuccessNotification.PopUpHttpPostSuccessNotification(API.Suppliers, message);
            }
            return supplier;
        }
        #endregion

        #region Update
        public static async Task<TSupplier> UpdateSupplierAsync(Guid supplierId, SupplierDTO supplierDTO)
        {
            var supplier = await Utility.UpdateAsync<TSupplier>(BaseURI.HyperStoreService + API.Suppliers, supplierId.ToString(), supplierDTO);
            if (supplier != null)
            {
                //TODO: Success Notification
                SupplierUpdatedEvent?.Invoke();
            }
            return supplier;
        }

        #endregion

        #region Read
        public static async Task<List<TSupplier>> RetrieveSuppliersAsync(SupplierFilterCriteriaDTO sfc)
        {
            List<TSupplier> suppliers = await Utility.RetrieveAsync<List<TSupplier>>(BaseURI.HyperStoreService + API.Suppliers, null, sfc);
            return suppliers;
        }

        public static async Task<TSupplier> RetrieveTheSupplierAsync(Guid supplierId)
        {
            TSupplier supplier = await Utility.RetrieveAsync<TSupplier>(BaseURI.HyperStoreService + API.Suppliers, supplierId.ToString(), null);
            return supplier;
        }

        public static async Task<Int32> RetrieveTotalSuppliers()
        {
            Int32 totalSuppliers = await Utility.RetrieveAsync<Int32>(BaseURI.HyperStoreService + API.Suppliers, CustomAction.GetTotalRecordsCount, null);
            return totalSuppliers;
        }

        public static async Task<IRange<T>> RetrieveWalletRangeAsync<T>()
        {
            var IRange = await Utility.RetrieveAsync<IRange<T>>(BaseURI.HyperStoreService + API.Suppliers, CustomAction.GetWalletBalanceRange, null);
            return IRange;
        }
        #endregion 
    }
}
