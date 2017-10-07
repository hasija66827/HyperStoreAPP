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
    public delegate void CustomerEntityChangedDelegate();

    public class CustomerDataSource
    {
        public static event CustomerEntityChangedDelegate CustomerCreatedEvent;
        public static event CustomerEntityChangedDelegate CustomerUpdatedEvent;

        #region Create 
        public static async Task<TCustomer> CreateNewCustomerAsync(CustomerDTO customerDTO)
        {
            var customer = await Utility.CreateAsync<TCustomer>(BaseURI.HyperStoreService + API.Customers, customerDTO);
            if (customer != null)
            {
                CustomerCreatedEvent?.Invoke();
                var message = String.Format("You can start taking orders from Customer {0} ({1}).", customer.Name, customer.MobileNo);
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
                CustomerUpdatedEvent?.Invoke();
            }
            return customer;
        }

        #endregion

        #region Read 
        public static async Task<List<TCustomer>> RetrieveCustomersAsync(CustomerFilterCriteriaDTO cfc)
        {
            List<TCustomer> customers = await Utility.RetrieveAsync<List<TCustomer>>(BaseURI.HyperStoreService + API.Customers, null, cfc);
            return customers;
        }

        public static async Task<Int32> RetrieveTotalCustomers()
        {
            Int32 totalCustomer = await Utility.RetrieveAsync<Int32>(BaseURI.HyperStoreService + API.Customers, CustomAction.GetTotalRecordsCount, null);
            return totalCustomer;
        }

        public static async Task<IRange<T>> RetrieveWalletRangeAsync<T>()
        {
            var IRange = await Utility.RetrieveAsync<IRange<T>>(BaseURI.HyperStoreService + API.Customers, CustomAction.GetWalletBalanceRange, null);
            return IRange;
        }
        #endregion
    }
}
