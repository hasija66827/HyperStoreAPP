using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate.View_Models;
using SDKTemplate;
namespace SDKTemplate.Data_Source
{
    public class WholeSellerOrderTransactionDataSource
    {
        /// <summary>
        /// creates a many to many relationship between wholesellerorder and transaction.
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="wholeSellerOrderId"></param>
        /// <returns></returns>
        public static bool CreateNewWholeSellerOrderTransaction(Guid transactionId, Guid wholeSellerOrderId, decimal paidAmount, bool isPaymentComplete, DatabaseModel.RetailerContext db)
        {
            db.WholeSellerOrderTransactions.Add(new DatabaseModel.WholeSellerOrderTransaction(transactionId, wholeSellerOrderId, paidAmount, isPaymentComplete));
            return true;
        }

        public static List<SettledOrderOfTransactionViewModel> RetrieveWholeSellerOrderTransactions(Guid? transactionId = null, Guid? wholeSellerOrderId = null, DatabaseModel.RetailerContext db = null)
        {
            List<DatabaseModel.WholeSellerOrderTransaction> wholeSellerOrderTransactions;
            IEnumerable<SettledOrderOfTransactionViewModel> result;
            if (db == null)
                db = new DatabaseModel.RetailerContext();
            if ((transactionId == null && wholeSellerOrderId == null) || (transactionId != null && wholeSellerOrderId != null))
                return null;
            else if (transactionId != null)
            {
                wholeSellerOrderTransactions = db.WholeSellerOrderTransactions
                    .Where(wot => wot.TransactionId == transactionId).ToList();
                var wholeSellerOrder = db.WholeSellersOrders.ToList();
                result = wholeSellerOrderTransactions
                        .Join(wholeSellerOrder,
                            wot => wot.WholeSellerOrderId,
                             wo => wo.WholeSellerOrderId,
                             (wot, wo) => new SettledOrderOfTransactionViewModel(wo, wot.PaidAmount));
            }
            else
            {
                wholeSellerOrderTransactions = db.WholeSellerOrderTransactions
                    .Where(wot => wot.WholeSellerOrderId == wholeSellerOrderId).ToList();
                var transactions = db.Transactions.ToList();
                result = wholeSellerOrderTransactions
                   .Join(transactions,
                       wot => wot.TransactionId,
                       t => t.TransactionId,
                       (wot, t) => new SettledOrderOfTransactionViewModel(wot.PaidAmount, t));
            }
            return result.ToList();
        }
    }
}
