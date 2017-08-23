using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace SDKTemplate
{
    partial class CustomerDataSource
    {
        public static List<CustomerViewModel> Customers { get { return _customers; } }
        private static List<CustomerViewModel> _customers = new List<CustomerViewModel>();
        public CustomerDataSource(){
        }

        #region Create Transaction
        /// <summary>
        /// Adds The customer into customer Data source as well as in sqllte database.
        /// </summary>
        /// <param name="newCustomer"></param>
        public static void CreateNewCustomer(CustomerViewModel newCustomer)
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                db.Customers.Add((DatabaseModel.Customer)newCustomer);
                db.SaveChanges();
            }
            _customers.Add(newCustomer);
        }
        #endregion

        #region Read Transactions
        public static void RetrieveCustomersAsync()
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                // Retrieving data from the database synchronously #combninng the statement gives the error while object creation.
                var dbCustomer = db.Customers.ToList();
                _customers = dbCustomer.
                    Select(c => new CustomerViewModel(c)).ToList();
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
        
       /// <summary>
       /// Retrieve the customer of given customerId
       /// </summary>
       /// <param name="customerId"></param>
       /// <returns></returns>
        public static CustomerViewModel GetCustomerById(Guid? customerId)
        {
            if (customerId == null)
                return null;
            return _customers
                  .Where(c => c.CustomerId.Equals(customerId)).FirstOrDefault();
        }

        /// <summary>
        /// Gets the minimum wallet balance from the list of customers.
        /// Used by Filter box for its range control
        /// </summary>
        /// <returns></returns>
        public static decimal GetMinimumWalletBalance()
        {
            if (_customers.Count == 0)
                return 0;
            return _customers.Min(c => c.WalletBalance);
        }

        /// <summary>
        /// Gets the maximum wallet balance from the list of customers.
        /// Used by Filter box for its range control
        /// </summary>
        /// <returns></returns>
        public static decimal GetMaximumWalletBalance()
        {
            if (_customers.Count == 0)
                return 0;
            return _customers.Max(c => c.WalletBalance);
        }
        
        /// <summary>
        /// Check if Name is unique across the list of customers.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsNameExist(string name)
        {
            var customers = CustomerDataSource._customers
                            .Where(c => c.Name.ToLower() == name.ToLower());
            if (customers.Count() == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Checks if mobile number exist across the list of the customers.
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public static bool IsMobileNumberExist(string mobileNumber)
        {
            var customers = CustomerDataSource._customers
                            .Where(c => c.MobileNo == mobileNumber);
            if (customers.Count() == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Retrieves the list of customers with the given customerId AND filterPersonCriteria.
        /// If customerId is null, then no customers are retrieved on the bases of filterPersonCriteria only.
        /// Used by FilterCustomerCC
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="filterCustomerCriteria"></param>
        /// <returns></returns>
        public static List<CustomerViewModel> GetFilteredCustomer(Guid? customerId, FilterPersonCriteria filterCustomerCriteria)
        {
            List<CustomerViewModel> result = new List<CustomerViewModel>();
            if (customerId == null)
                result = CustomerDataSource._customers;
            else
                result.Add(GetCustomerById(customerId));

            if (filterCustomerCriteria.WalletBalance == null)
                return result;
            else
            {
                return result
                    .Where(c => c.WalletBalance >= filterCustomerCriteria.WalletBalance.LB
                            && c.WalletBalance <= filterCustomerCriteria.WalletBalance.UB).ToList();
            }
        }
        #endregion

        #region UpdateTransaction

        /// <summary>
        /// Update the wallet balance of the customer in memory and in database.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="customerViewModel"></param>
        /// <param name="walletBalanceToBeDeducted"></param>
        /// <param name="walletBalanceToBeAdded"></param>
        /// <returns>Return the updated wallet balance of the customer.</returns>
        public static decimal UpdateWalletBalanceOfCustomer(DatabaseModel.RetailerContext db, CustomerViewModel customerViewModel,
            decimal walletBalanceToBeDeducted, decimal walletBalanceToBeAdded)
        {
            var billingCustomer = (DatabaseModel.Customer)customerViewModel;
            var entityEntry = db.Attach(billingCustomer);
            billingCustomer.WalletBalance -= walletBalanceToBeDeducted;
            billingCustomer.WalletBalance += walletBalanceToBeAdded;
            var memberEntry = entityEntry.Member(nameof(DatabaseModel.Customer.WalletBalance));
            memberEntry.IsModified = true;
            db.SaveChanges();
            _UpdateWalletBalanceOfCustomerInMemory(customerViewModel, (decimal)billingCustomer.WalletBalance);
            return billingCustomer.WalletBalance;
        }

        /// <summary>
        /// Updates the wallet balance of the given customer in memory.
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <param name="walletBalance"></param>
        private static void _UpdateWalletBalanceOfCustomerInMemory(CustomerViewModel customerViewModel, decimal walletBalance)
        {
            int index = _customers.FindIndex(c => c.CustomerId == customerViewModel.CustomerId);
            if (index < 0 || index >= _customers.Count())
                throw new Exception("Assert: Customer should be present inmemory customer data source");
            _customers[index].WalletBalance = walletBalance;
        }
        #endregion
    }
}
