using SDKTemp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class WholeSellerDataSource
    {
        private static List<WholeSellerViewModel> _WholeSellers = new List<WholeSellerViewModel>();
        public static List<WholeSellerViewModel> WholeSellers { get { return _WholeSellers; } }
        public WholeSellerDataSource()
        {
            RetrieveWholeSellersAsync();
        }
        public static void RetrieveWholeSellersAsync()
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                // Retrieving data from the database synchronously.
                _WholeSellers = db.WholeSellers.Select(WholeSeller => new WholeSellerViewModel(
                    WholeSeller.WholeSellerId,
                    WholeSeller.Name,
                    WholeSeller.MobileNo,
                    WholeSeller.Address,
                    WholeSeller.WalletBalance,
                    WholeSeller.IsVerifiedWholeSeller)).ToList();
            }
        }

        public static List<WholeSellerViewModel> GetFilteredWholeSeller(Guid? wholeSellerId, FilterPersonCriteria filterWholeSellerCriteria)
        {
            List<WholeSellerViewModel> result = new List<WholeSellerViewModel>();
            if (wholeSellerId == null)
                result = WholeSellerDataSource._WholeSellers;
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
            return _WholeSellers
                .Where(item => item.MobileNo.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1
                            || item.Address.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(item => item.MobileNo.StartsWith(query, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// returns the WholeSeller having matching mobile number from WholeSeller datasource.
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public static WholeSellerViewModel GetWholeSellerByMobileNumber(string mobileNumber)
        {
            try
            {
                return _WholeSellers
                     .Where(c => c.MobileNo.Equals(mobileNumber)).First();
            }
            catch
            {
                return null;
            }
        }

        public static WholeSellerViewModel GetWholeSellerById(Guid? wholeSellerId)
        {
            if (wholeSellerId == null)
                return null;
            try
            {
                return _WholeSellers
                     .Where(w => w.WholeSellerId.Equals(wholeSellerId)).First();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Adds The WholeSeller into WholeSeller Data source as well as in sqllte database.
        /// </summary>
        /// <param name="newWholeSeller"></param>
        public static void AddWholeSeller(WholeSellerViewModel newWholeSeller)
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                db.WholeSellers.Add((DatabaseModel.WholeSeller)newWholeSeller);
                db.SaveChanges();
            }
            _WholeSellers.Add(newWholeSeller);
        }

        public static bool IsNameExist(string name)
        {
            var WholeSellers = WholeSellerDataSource._WholeSellers
             .Where(c => c.Name.ToLower() == name.ToLower());
            if (WholeSellers.Count() == 0)
                return false;
            return true;
        }

        public static bool IsMobileNumberExist(string mobileNumber)
        {
            var WholeSellers = WholeSellerDataSource._WholeSellers
             .Where(c => c.MobileNo == mobileNumber);
            if (WholeSellers.Count() == 0)
                return false;
            return true;
        }


        public static float GetMinimumWalletBalance()
        {
            if (_WholeSellers.Count == 0)
                return 0;
            return _WholeSellers.Min(c => c.WalletBalance);
        }

        public static float GetMaximumWalletBalance()
        {
            if (_WholeSellers.Count == 0)
                return 0;
            return _WholeSellers.Max(c => c.WalletBalance);
        }

    }
}
