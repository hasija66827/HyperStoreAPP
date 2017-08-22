
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class WholeSellerDataSource
    {   
        #region Create
        public static async void CreateWholeSeller(WholeSellerViewModel newWholeSeller)
        {
            string actionURI = "suppliers";
            var content = JsonConvert.SerializeObject(newWholeSeller);
            var response = await Utility.HttpPost(actionURI, content);
            response.EnsureSuccessStatusCode();
        }
        #endregion

        #region Read
        public static async Task<List<WholeSellerViewModel>> RetrieveWholeSellersAsync()
        {
            string actionURI = "suppliers";
            string httpResponseBody = "";
            try
            {
                var response = await Utility.HttpGet(actionURI);
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

        /// <summary>
        /// Retrieves the list of wholesellers with the given wholesellerId AND filterPersonCriteria.
        /// If wholesellerId is null, then no wholesellers are retrieved on the bases of filterPersonCriteria only.
        /// Used by FilterwholesellerCC
        /// </summary>
        /// <param name="wholeSellerId"></param>
        /// <param name="filterWholeSellerCriteria"></param>
        /// <returns></returns>
        public static async Task<List<WholeSellerViewModel>> GetFilteredWholeSeller(Guid? wholeSellerId, FilterPersonCriteria filterWholeSellerCriteria)
        {
            return await RetrieveWholeSellersAsync();
        }


        public static float GetMinimumWalletBalance()
        {
           
            return 100;
        }

        public static float GetMaximumWalletBalance()
        {
           
            return 2000;
        }
        #endregion

        //#remove
        #region Update
        public static float UpdateWalletBalanceOfWholeSeller(DatabaseModel.RetailerContext db, WholeSellerViewModel wholeSellerViewModel,
        float walletBalanceToBeAdded)
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
