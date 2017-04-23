using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
namespace MasterDetailApp
{
    /*
    class Customer
    {
        public string MobileNo { get {return this._mobileNumber; }}
        private string _mobileNumber;
        public Customer(string mobileNumber) {
            _mobileNumber = mobileNumber;
        }
    }*/
    class CustomerDataSource
    {
        private static List<Customer> _customers = new List<Customer>();
        public static void RetrieveMobileNumberAsync()
        {
            using (var db = new RetailerContext())
            {
                // Retrieving data from the database synchronously.
                _customers = db.Customers.Select(customer => new Customer(customer.MobileNo)).ToList();  
            }
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on a pre-defined rule set
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of mobileNumber that matches the query</returns>
        public static IEnumerable<Customer> GetMatchingItems(string query)
        {
            return _customers
                .Where(item =>item.MobileNo.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(item => item.MobileNo.StartsWith(query, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
