using Models;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    class CustomerTransactionDataSource
    {
        #region create
        public static async Task<TCustomerTransaction> CreateNewTransactionAsync(CustomerTransactionDTO transactionDTO)
        {
            string actionURI = API.CustomerTransactions;
            var transaction = await Utility.CreateAsync<TCustomerTransaction>(actionURI, transactionDTO);
            return transaction;
        }
        #endregion

        #region Read
        public static async Task<List<TCustomerTransaction>> RetrieveTransactionsAsync(CustomerTransactionFilterCriteriaDTO tfc)
        {
            List<TCustomerTransaction> transactions = await Utility.RetrieveAsync<TCustomerTransaction>(API.CustomerTransactions, null, tfc);
            return transactions;
        }
        #endregion
    }
}
