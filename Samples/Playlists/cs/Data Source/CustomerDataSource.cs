using Newtonsoft.Json;
using SDKTemplate.Data_Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate.DTO;
using Models;
using System.ComponentModel.DataAnnotations;

namespace SDKTemplate
{
    partial class CustomerDataSource
    {
        public class CustomerFilterCriteriaDTO
        {
            [Required]
            public IRange<decimal> WalletAmount { get; set; }
            public Guid? CustomerId { get; set; }
        }

        #region Create 
        /// <summary>
        /// Adds The customer into customer Data source as well as in sqllte database.
        /// </summary>
        /// <param name="newCustomer"></param>
        public static async void CreateNewCustomer(CustomerDTO customerDTO)
        {
            try
            {
                string actionURI = "customers";
                var content = JsonConvert.SerializeObject(customerDTO);
                var response = await Utility.HttpPost(actionURI, content);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region Read 
        public static async Task<List<TCustomer>> RetrieveCustomersAsync(CustomerFilterCriteriaDTO cfc)
        {
            string actionURI = "customers";
            string httpResponseBody = "";
            try
            {
                var response = await Utility.HttpGet(actionURI, cfc);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    httpResponseBody = await response.Content.ReadAsStringAsync();
                    var customers = JsonConvert.DeserializeObject<List<TCustomer>>(httpResponseBody);
                    return customers;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Gets the minimum wallet balance from the list of customers.
        /// Used by Filter box for its range control
        /// </summary>
        /// <returns></returns>
        public static decimal GetMinimumWalletBalance()
        {
            return -100000;
        }

        /// <summary>
        /// Gets the maximum wallet balance from the list of customers.
        /// Used by Filter box for its range control
        /// </summary>
        /// <returns></returns>
        public static decimal GetMaximumWalletBalance()
        {
            return 10000;
        }

        /// <summary>
        /// Check if Name is unique across the list of customers.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsNameExist(string name)
        {
            return false;
        }

        /// <summary>
        /// Checks if mobile number exist across the list of the customers.
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public static bool IsMobileNumberExist(string mobileNumber)
        {
            return false;
        }
        #endregion

        #region UpdateTransaction
        //#remove
        /// <summary>
        /// Update the wallet balance of the customer in memory and in database.
        /// </summary>
        /// <param name="db"></param>
        /// <param name="customerViewModel"></param>
        /// <param name="walletBalanceToBeDeducted"></param>
        /// <param name="walletBalanceToBeAdded"></param>
        /// <returns>Return the updated wallet balance of the customer.</returns>
        public static decimal UpdateWalletBalanceOfCustomer(DatabaseModel.RetailerContext db, TCustomer customerViewModel,
            decimal walletBalanceToBeDeducted, decimal walletBalanceToBeAdded)
        {
            /*
            var billingCustomer = (DatabaseModel.Customer)customerViewModel;
            var entityEntry = db.Attach(billingCustomer);
            billingCustomer.WalletBalance -= walletBalanceToBeDeducted;
            billingCustomer.WalletBalance += walletBalanceToBeAdded;
            var memberEntry = entityEntry.Member(nameof(DatabaseModel.Customer.WalletBalance));
            memberEntry.IsModified = true;
            db.SaveChanges();
            _UpdateWalletBalanceOfCustomerInMemory(customerViewModel, (decimal)billingCustomer.WalletBalance);
            return billingCustomer.WalletBalance;*/
            return 100;
        }

        /// <summary>
        /// Updates the wallet balance of the given customer in memory.
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <param name="walletBalance"></param>
        private static void _UpdateWalletBalanceOfCustomerInMemory(CustomerViewModel customerViewModel, decimal walletBalance)
        { }
        #endregion
    }
}
