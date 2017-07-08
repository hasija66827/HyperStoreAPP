using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemp.ViewModel;
namespace SDKTemplate
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
                _customers = db.Customers.Select(customer => new CustomerViewModel(
                    customer.CustomerId,
                    customer.Name,
                    customer.MobileNo,
                    customer.Address,
                    customer.WalletBalance,
                    customer.IsVerifiedCustomer)).ToList();
            }
        }

        // Step 1:
        /// <summary>
        /// Return the updated wallet balance of the customer.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="customerViewModel"></param>
        /// <param name="walletBalanceToBeDeducted"></param>
        /// <param name="walletBalanceToBeAdded"></param>
        /// <returns></returns>
        public static float UpdateWalletBalanceOfCustomer(DatabaseModel.RetailerContext db, CustomerViewModel customerViewModel,
            float walletBalanceToBeDeducted, float walletBalanceToBeAdded)
        {
            var billingCustomer = (DatabaseModel.Customer)customerViewModel;
            var entityEntry = db.Attach(billingCustomer);
            billingCustomer.WalletBalance -= walletBalanceToBeDeducted;
            billingCustomer.WalletBalance += walletBalanceToBeAdded;
            var memberEntry = entityEntry.Member(nameof(DatabaseModel.Customer.WalletBalance));
            memberEntry.IsModified = true;
            db.SaveChanges();
            int index = _customers.FindIndex(c => c.CustomerId == customerViewModel.CustomerId);
            if (index < 0 || index >= _customers.Count())
                throw new Exception("Assert: Customer should be present in customer data source");
            _customers[index].WalletBalance = billingCustomer.WalletBalance;
            return billingCustomer.WalletBalance;
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
        /// returns the customer having matching mobile number from customer datasource.
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public static CustomerViewModel GetCustomerByMobileNumber(string mobileNumber)
        {
            try
            {
                return _customers
                     .Where(c => c.MobileNo.Equals(mobileNumber)).First();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// returns the customer having matching mobile number from customer datasource.
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public static CustomerViewModel GetCustomerById(Guid? customerId)
        {
            if (customerId == null)
                return null;
            try
            {
                return _customers
                     .Where(c => c.CustomerId.Equals(customerId)).First();
            }
            catch
            {
                return null;
            }
        }

        public static float GetMinimumWalletBalance()
        {
            return _customers.Min(c => c.WalletBalance);
        }

        public static float GetMaximumWalletBalance()
        {
            return _customers.Max(c => c.WalletBalance);
        }

        /// <summary>
        /// Adds The customer into customer Data source as well as in sqllte database.
        /// </summary>
        /// <param name="newCustomer"></param>
        public static void AddCustomer(CustomerViewModel newCustomer)
        {
            using (var db = new DatabaseModel.RetailerContext())
            {
                db.Customers.Add((DatabaseModel.Customer)newCustomer);
                db.SaveChanges();
            }
            _customers.Add(newCustomer);
        }

        public static bool IsNameExist(string name)
        {
            var customers = CustomerDataSource._customers
             .Where(c => c.Name.ToLower() == name.ToLower());
            if (customers.Count() == 0)
                return false;
            return true;
        }
        public static bool IsMobileNumberExist(string mobileNumber)
        {
            var customers = CustomerDataSource._customers
             .Where(c => c.MobileNo == mobileNumber);
            if (customers.Count() == 0)
                return false;
            return true;
        }

        public static List<CustomerViewModel> GetFilteredCustomer(Guid? customerId, FilterCustomerCriteria filterCustomerCriteria)
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
    }
}
