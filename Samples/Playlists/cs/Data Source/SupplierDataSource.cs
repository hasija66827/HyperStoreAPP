
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
            var supplier = await Utility.CreateAsync<TSupplier>(API.Suppliers, supplierDTO);
            if (supplier != null)
            {
                var message = String.Format("You can Start placing Ordes to {0} ({1})", supplier.Name, supplier.MobileNo);
                SuccessNotification.PopUpSuccessNotification(API.Suppliers, message);
            }
            return supplier;
        }
        #endregion

        #region Read
        public static async Task<List<TSupplier>> RetrieveSuppliersAsync(SupplierFilterCriteriaDTO sfc)
        {
            List<TSupplier> suppliers = await Utility.RetrieveAsync<TSupplier>(API.Suppliers, null, sfc);
            return suppliers;
        }

        //TODO
        public static decimal GetMinimumWalletBalance()
        {

            return -10000;
        }

        //TODO
        public static decimal GetMaximumWalletBalance()
        {

            return 20000;
        }
        #endregion 
    }
}
