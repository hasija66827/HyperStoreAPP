
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SupplierFilterCriteria
    {
        [Required]
        public IRange<decimal> WalletAmount { get; set; }
        public Guid? SupplierId { get; set; }
    }

    class SupplierDataSource
    {   
        #region Create
        public static async void CreateNewSupplier(WholeSellerViewModel newWholeSeller)
        {
            try
            {
                string actionURI = "suppliers";
                var content = JsonConvert.SerializeObject(newWholeSeller);
                var response = await Utility.HttpPost(actionURI, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region Read
        public static async Task<List<WholeSellerViewModel>> RetrieveSuppliersAsync(SupplierFilterCriteria sfc)
        {
            string actionURI = "suppliers";
            string httpResponseBody = "";
            try
            {
                var response = await Utility.HttpGet(actionURI, sfc);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    httpResponseBody = await response.Content.ReadAsStringAsync();
                    var suppliers = JsonConvert.DeserializeObject<List<WholeSellerViewModel>>(httpResponseBody);
                    return suppliers;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static decimal GetMinimumWalletBalance()
        {
           
            return -10000;
        }

        public static decimal GetMaximumWalletBalance()
        {
           
            return 20000;
        }
        #endregion

        //#remove
        #region Update
        public static decimal UpdateWalletBalanceOfWholeSeller(DatabaseModel.RetailerContext db, WholeSellerViewModel wholeSellerViewModel,
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
