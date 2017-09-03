
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
            string actionURI = API.Suppliers;
            var x = await Utility.CreateAsync<TSupplier>(actionURI, supplierDTO);
            return x;
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
