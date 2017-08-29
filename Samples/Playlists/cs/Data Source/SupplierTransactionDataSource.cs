using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using SDKTemplate.DTO;

namespace SDKTemplate
{
    public class SupplierTransactionDataSource
    {
        #region create
        public static async Task<TSupplierTransaction> CreateNewTransactionAsync(SupplierTransactionDTO transactionDTO)
        {
            string actionURI = "SupplierTransactions";
            var transaction = await Utility.CreateAsync<TSupplierTransaction>(actionURI, transactionDTO);
            return transaction;
        }
        #endregion

        #region Read
        public static async Task<List<TSupplierTransaction>> RetrieveTransactionsAsync(SupplierTransactionFilterCriteriaDTO tfc)
        {
            string actionURI = "SupplierTransactions";
            List<TSupplierTransaction> transactions = await Utility.RetrieveAsync<TSupplierTransaction>(actionURI, tfc);
            return transactions;
        }
        #endregion
    }
}
