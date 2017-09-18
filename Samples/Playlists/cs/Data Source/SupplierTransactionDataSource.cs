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
            var transaction = await Utility.CreateAsync<TSupplierTransaction>(BaseURI.HyperStoreService + API.SupplierTransactions, transactionDTO);
            _SendTransactionCreationNotification(transaction);
            return transaction;
        }
        #endregion

        private static void _SendTransactionCreationNotification(TSupplierTransaction transaction)
        {
            if (transaction == null)
                return;
            var supplierName = transaction.Supplier.Name;
            var formattedTranAmt = Utility.ConvertToRupee(transaction.TransactionAmount);
            var firstMessage = String.Format("You Payed {0} to {1}.", formattedTranAmt, supplierName);
            var secondMessage = "";
            var updatedWalletBalance = transaction.WalletSnapshot - transaction.TransactionAmount;
            var formattedUpdatedWalletBalance = Utility.ConvertToRupee(Math.Abs(updatedWalletBalance));
            if (updatedWalletBalance > 0)
                secondMessage = String.Format("You owe {0} to {1}.", formattedUpdatedWalletBalance, supplierName);
            else
                secondMessage = String.Format("{1} owes you {0}.", formattedUpdatedWalletBalance, supplierName);

            SuccessNotification.PopUpSuccessNotification(API.SupplierTransactions, firstMessage + "\n" + secondMessage);
        }


        #region Read
        public static async Task<List<TSupplierTransaction>> RetrieveTransactionsAsync(SupplierTransactionFilterCriteriaDTO tfc)
        {
            List<TSupplierTransaction> transactions = await Utility.RetrieveAsync<List<TSupplierTransaction>>(BaseURI.HyperStoreService + API.SupplierTransactions, null, tfc);
            return transactions;
        }
        #endregion
    }
}
