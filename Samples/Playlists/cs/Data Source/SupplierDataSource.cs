﻿
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
    public class SupplierFilterCriteriaDTO
    {
        [Required]
        public IRange<decimal> WalletAmount { get; set; }
        public Guid? SupplierId { get; set; }
    }

    class SupplierDataSource
    {
        #region Create
        public static async Task<TSupplier> CreateNewSupplier(SupplierDTO supplierDTO)
        {
            string actionURI = "suppliers";
            var x = await Utility.CreateAsync<TSupplier>(actionURI, supplierDTO);
            return x;
        }
        #endregion

        #region Read
        public static async Task<List<TSupplier>> RetrieveSuppliersAsync(SupplierFilterCriteriaDTO sfc)
        {
            string actionURI = "suppliers";
            List<TSupplier> suppliers = await Utility.RetrieveAsync<TSupplier>(actionURI, sfc);
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
