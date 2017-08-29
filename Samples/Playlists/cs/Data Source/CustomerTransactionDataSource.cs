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
            string actionURI = "CustomerTransactions";
            var transaction = await Utility.CreateAsync<TCustomerTransaction>(actionURI, transactionDTO);
            return transaction;
        }
        #endregion

        #region Read
        public static async Task<List<TCustomerTransaction>> RetrieveTransactionsAsync(CustomerTransactionFilterCriteriaDTO tfc)
        {
            string actionURI = "CustomerTransactions";
            List<TCustomerTransaction> transactions = await Utility.RetrieveAsync<TCustomerTransaction>(actionURI, tfc);
            return transactions;
        }
        #endregion
    }
}
