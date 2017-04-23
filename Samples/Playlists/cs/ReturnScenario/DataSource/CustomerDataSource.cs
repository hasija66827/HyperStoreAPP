using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterDetailApp.ViewModel;
namespace MasterDetailApp.Data
{
    class CustomerDataSource
    {
        private static List<CustomerViewModel> _customers = new List<CustomerViewModel>();
        public CustomerDataSource()
        {
            RetrieveCustomersAsync();
        }
        public static void RetrieveCustomersAsync()
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                // Retrieving data from the database synchronously.
                _customers = db.Customers.Select(customer => new CustomerViewModel(customer.MobileNo, customer.Address)).ToList();
            }
        }

        /// <summary>
        /// Do a fuzzy search on all Product and order results based on a pre-defined rule set
        /// </summary>
        /// <param name="query">The part of the name or company to look for</param>
        /// <returns>An ordered list of mobileNumber that matches the query</returns>
        public static IEnumerable<CustomerViewModel> GetMatchingCustomers(string query)
        {
            return _customers
                .Where(item => item.MobileNo.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1
                            || item.Address.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(item => item.MobileNo.StartsWith(query, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
