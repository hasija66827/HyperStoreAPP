using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate.View_Models;
using Models;
using SDKTemplate.DTO;

namespace SDKTemplate
{
    public class TransactionDataSource
    {
        #region create
        /// <summary>
        /// Creates a new transaction in database corresponding to the wholeseller.
        /// </summary>
        /// <param name="transactionViewModel"></param>
        /// <returns></returns>
        public static async Task<TTransaction> CreateNewTransactionAsync(TransactionDTO transactionDTO)
        {
            string actionURI = "transactions";
            var transaction = await Utility.CreateAsync<TTransaction>(actionURI, transactionDTO);
            return transaction;

        }
        #endregion

        #region Read
        /// <summary>
        /// Retrieves all the transaction corresponding to a wholeseller.
        /// </summary>
        /// <param name="wholeSellerId"></param>
        /// <returns></returns>
        public static async Task<List<TTransaction>> RetrieveTransactionsAsync(TransactionFilterCriteriaDTO tfc)
        {
            string actionURI = "transactions";
            List<TTransaction> transactions = await Utility.RetrieveAsync<TTransaction>(actionURI, tfc);
            return transactions;
        }
        #endregion
    }
}
