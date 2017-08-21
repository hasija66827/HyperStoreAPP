
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
        private static List<WholeSellerViewModel> _suppliers = new List<WholeSellerViewModel>();
        public static List<WholeSellerViewModel> WholeSellers { get { return _suppliers; } }
//#remove
        #region Create
        /// <summary>
        /// Adds The WholeSeller into WholeSeller Data source as well as in sqllte database.
        /// </summary>
        /// <param name="newWholeSeller"></param>
        public static void CreateWholeSeller(WholeSellerViewModel newWholeSeller)
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                db.WholeSellers.Add((DatabaseModel.WholeSeller)newWholeSeller);
                db.SaveChanges();
            }
            _suppliers.Add(newWholeSeller);
        }
        #endregion

        #region Read
        public static async Task RetrieveWholeSellersAsync()
        {
            string actionURI = "suppliers";
            string httpResponseBody = "";
            try
            {
                var response = await Utility.HttpGet(actionURI);
                if (response.IsSuccessStatusCode)
                {
                    httpResponseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<WholeSellerViewModel>>(httpResponseBody);
                    _suppliers = result;
                }
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
        public static List<WholeSellerViewModel> GetFilteredWholeSeller(Guid? wholeSellerId, FilterPersonCriteria filterWholeSellerCriteria)
        {
            List<WholeSellerViewModel> result = new List<WholeSellerViewModel>();
            if (wholeSellerId == null)
                result = WholeSellerDataSource._suppliers;
            else
                result.Add(GetWholeSellerById(wholeSellerId));

            if (filterWholeSellerCriteria.WalletBalance == null)
                return result;
            else
            {
                return result
                    .Where(c => c.WalletBalance >= filterWholeSellerCriteria.WalletBalance.LB
                            && c.WalletBalance <= filterWholeSellerCriteria.WalletBalance.UB).ToList();
            }
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on a pre-defined rule set
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of mobileNumber that matches the query</returns>
        public static IEnumerable<WholeSellerViewModel> GetMatchingWholeSellers(string query)
        {
            return _suppliers
                .Where(item => item.MobileNo.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1
                            || item.Address.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(item => item.MobileNo.StartsWith(query, StringComparison.CurrentCultureIgnoreCase));
        }

        public static WholeSellerViewModel GetWholeSellerById(Guid? wholeSellerId)
        {
            if (wholeSellerId == null)
                return null;
            return _suppliers
                 .Where(w => w.SupplierId.Equals(wholeSellerId)).FirstOrDefault();
        }

        public static bool IsNameExist(string name)
        {
            var WholeSellers = WholeSellerDataSource._suppliers
             .Where(c => c.Name.ToLower() == name.ToLower());
            if (WholeSellers.Count() == 0)
                return false;
            return true;
        }

        public static bool IsMobileNumberExist(string mobileNumber)
        {
            var WholeSellers = WholeSellerDataSource._suppliers
             .Where(c => c.MobileNo == mobileNumber);
            if (WholeSellers.Count() == 0)
                return false;
            return true;
        }

        public static float GetMinimumWalletBalance()
        {
            if (_suppliers.Count == 0)
                return 0;
            return _suppliers.Min(c => c.WalletBalance);
        }

        public static float GetMaximumWalletBalance()
        {
            if (_suppliers.Count == 0)
                return 0;
            return _suppliers.Max(c => c.WalletBalance);
        }
        #endregion

        //#remove
        #region Update
        public static float UpdateWalletBalanceOfWholeSeller(DatabaseModel.RetailerContext db, WholeSellerViewModel wholeSellerViewModel,
        float walletBalanceToBeAdded)
        {
            var wholeSeller = (DatabaseModel.WholeSeller)wholeSellerViewModel;
            var entityEntry = db.Attach(wholeSeller);
            wholeSeller.WalletBalance += walletBalanceToBeAdded;
            var memberEntry = entityEntry.Member(nameof(DatabaseModel.WholeSeller.WalletBalance));
            memberEntry.IsModified = true;
            db.SaveChanges();
            UpdateWalletBalanceOfWholeSellerInMemory(wholeSellerViewModel.SupplierId, wholeSeller.WalletBalance);
            return wholeSeller.WalletBalance;
        }

        private static void UpdateWalletBalanceOfWholeSellerInMemory(Guid wholeSellerId, float newWalletBalance)
        {
            int index = _suppliers.FindIndex(c => c.SupplierId == wholeSellerId);
            if (index < 0 || index >= _suppliers.Count())
                throw new Exception("Assert: WholeSeller should be present in wholeSeller data source");
            _suppliers[index].WalletBalance = newWalletBalance;
        }
        #endregion
    }
}
