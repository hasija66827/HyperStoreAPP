using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
namespace MasterDetailApp
{
    class MobileNumber
    {
        public string MobileNo { get {return this._mobileNumber; }}
        private string _mobileNumber;
        public MobileNumber(string mobileNumber) {
            _mobileNumber = mobileNumber;
        }
    }
    class MobileNumberDataSource
    {
        private static List<MobileNumber> _mobileNumbers = new List<MobileNumber>();
        public static void RetrieveMobileNumberAsync()
        {
            // Don't need to do this more than once.
            if (_mobileNumbers.Count > 0)
            {
                return;
            }
            using (var db = new RetailerContext())
            {
                // Retrieving data from the database synchronously.
                _mobileNumbers = db.Customers.Select(customer => new MobileNumber(customer.MobileNo)).ToList();  
                
            }
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on a pre-defined rule set
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of mobileNumber that matches the query</returns>
        public static IEnumerable<MobileNumber> GetMatchingProducts(string query)
        {
            return _mobileNumbers
                .Where(item =>item.MobileNo.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(item => item.MobileNo.StartsWith(query, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
