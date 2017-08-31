using Newtonsoft.Json;
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
        public static async Task<TCustomer> CreateNewCustomerAsync(CustomerDTO customerDTO)
        {
            string actionURI = "customers";
            var x = await Utility.CreateAsync<TCustomer>(actionURI, customerDTO);
            return x;
        }
        #endregion

        #region Read 
        public static async Task<List<TCustomer>> RetrieveCustomersAsync(CustomerFilterCriteriaDTO cfc)
        {
            string actionURI = "customers";
            List < TCustomer > customers= await Utility.RetrieveAsync<TCustomer>(actionURI,cfc);
            return customers;
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
    }
}
