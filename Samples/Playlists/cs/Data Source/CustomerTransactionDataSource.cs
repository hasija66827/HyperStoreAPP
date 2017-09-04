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
            _SendTransactionCreationNotification(transaction);
            return transaction;
        }
        #endregion

        private static void _SendTransactionCreationNotification(TCustomerTransaction transaction)
        {
            if (transaction == null)
                return;
            var customerName = transaction.Customer.Name;
            var formattedTranAmt = Utility.ConvertToRupee(transaction.TransactionAmount);
            var firstMessage = String.Format("You Received {0} from {1}.", formattedTranAmt, customerName);
            var secondMessage = "";
            var updatedWalletBalance = transaction.TransactionAmount + transaction.WalletSnapshot;
            var formattedUpdatedWalletBalance = Utility.ConvertToRupee(Math.Abs(updatedWalletBalance));
            if (updatedWalletBalance > 0)
                secondMessage = String.Format("You owe {0} to {1}.", formattedUpdatedWalletBalance, customerName);
            else
                secondMessage = String.Format("{1} owes you {0}.", formattedUpdatedWalletBalance, customerName);

            SuccessNotification.PopUpSuccessNotification(API.CustomerTransactions, firstMessage + "\n" + secondMessage);
        }

        #region Read
        public static async Task<List<TCustomerTransaction>> RetrieveTransactionsAsync(CustomerTransactionFilterCriteriaDTO tfc)
        {
            List<TCustomerTransaction> transactions = await Utility.RetrieveAsync<TCustomerTransaction>(API.CustomerTransactions, null, tfc);
            return transactions;
        }
        #endregion
    }
}
