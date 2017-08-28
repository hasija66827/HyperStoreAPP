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
        /// <summary>
        /// Creates a new transaction in database corresponding to the wholeseller.
        /// </summary>
        /// <param name="transactionViewModel"></param>
        /// <returns></returns>
        public static async Task<TSupplierTransaction> CreateNewTransactionAsync(SupplierTransactionDTO transactionDTO)
        {
            string actionURI = "SupplierTransactions";
            var transaction = await Utility.CreateAsync<TSupplierTransaction>(actionURI, transactionDTO);
            return transaction;
        }
        #endregion

        #region Read
        /// <summary>
        /// Retrieves all the transaction corresponding to a wholeseller.
        /// </summary>
        /// <param name="wholeSellerId"></param>
        /// <returns></returns>
        public static async Task<List<TSupplierTransaction>> RetrieveTransactionsAsync(TransactionFilterCriteriaDTO tfc)
        {
            string actionURI = "SupplierTransactions";
            List<TSupplierTransaction> transactions = await Utility.RetrieveAsync<TSupplierTransaction>(actionURI, tfc);
            return transactions;
        }
        #endregion
    }
}
