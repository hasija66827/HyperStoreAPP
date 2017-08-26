
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
        public static async Task<bool> CreateNewSupplier(SupplierDTO supplier)
        {
            try
            {
                string actionURI = "suppliers";
                var content = JsonConvert.SerializeObject(supplier);
                var response = await Utility.HttpPost(actionURI, content);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region Read
        public static async Task<List<TSupplier>> RetrieveSuppliersAsync(SupplierFilterCriteriaDTO sfc)
        {
            string actionURI = "suppliers";
            List<TSupplier> suppliers = await Utility.Retrieve<TSupplier>(actionURI, sfc);
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

        //#remove
        #region Update
        public static decimal UpdateWalletBalanceOfWholeSeller(DatabaseModel.RetailerContext db, Models.TSupplier wholeSellerViewModel,
        decimal walletBalanceToBeAdded)
        {
            /* var wholeSeller = (DatabaseModel.WholeSeller)wholeSellerViewModel;
             var entityEntry = db.Attach(wholeSeller);
             wholeSeller.WalletBalance += walletBalanceToBeAdded;
             var memberEntry = entityEntry.Member(nameof(DatabaseModel.WholeSeller.WalletBalance));
             memberEntry.IsModified = true;
             db.SaveChanges();
             UpdateWalletBalanceOfWholeSellerInMemory(wholeSellerViewModel.SupplierId, wholeSeller.WalletBalance);
             return wholeSeller.WalletBalance;*/
            return 0;
        }
        #endregion
    }
}
