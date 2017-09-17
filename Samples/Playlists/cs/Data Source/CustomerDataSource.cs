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

            var customer = await Utility.CreateAsync<TCustomer>(BaseURI.HyperStoreService + API.Customers, customerDTO);
            if (customer != null)
            {
                var message = String.Format("You can Start taking Orders from Customer {0} ({1})", customer.Name, customer.MobileNo);
                SuccessNotification.PopUpSuccessNotification(API.Customers, message);
            }
            return customer;
        }
        #endregion

        #region Update
        public static async Task<TCustomer> UpdateCustomerAsync(Guid customerId, CustomerDTO customerDTO)
        {
            var customer = await Utility.UpdateAsync<TCustomer>(BaseURI.HyperStoreService + API.Customers, customerId.ToString(), customerDTO);
            if (customer != null)
            {
                //TODO: succes notification
            }
            return customer;
        }

        #endregion

        #region Read 
        public static async Task<List<TCustomer>> RetrieveCustomersAsync(CustomerFilterCriteriaDTO cfc)
        {
            List<TCustomer> customers = await Utility.RetrieveAsync<TCustomer>(BaseURI.HyperStoreService + API.Customers, null, cfc);
            return customers;
        }

        public static async Task<IRange<T>> RetrieveWalletRangeAsync<T>()
        {
            var IRanges = await Utility.RetrieveAsync<IRange<T>>(BaseURI.HyperStoreService + API.Customers, "GetWalletBalanceRange", null);
            if (IRanges != null && IRanges.Count == 1)
                return IRanges[0];
            return null;
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
