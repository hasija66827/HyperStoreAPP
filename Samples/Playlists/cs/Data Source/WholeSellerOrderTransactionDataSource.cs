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
        public static bool CreateWholeSellerOrderTransaction(Guid transactionId, Guid wholeSellerOrderId, float paidAmount, bool isPaymentComplete, DatabaseModel.RetailerContext db)
        {
            db.WholeSellerOrderTransactions.Add(new DatabaseModel.WholeSellerOrderTransaction(transactionId, wholeSellerOrderId, paidAmount, isPaymentComplete));
            return true;
        }

        public static IEnumerable<SettledOrderOfTransactionViewModel> RetrieveWholeSellerOrderTransaction(Guid? transactionId = null, Guid? wholeSellerOrderId = null, DatabaseModel.RetailerContext db = null)
        {
            List<DatabaseModel.WholeSellerOrderTransaction> ret;

            if (db == null)
                db = new DatabaseModel.RetailerContext();
            if ((transactionId == null && wholeSellerOrderId == null) || (transactionId != null && wholeSellerOrderId != null))
                return null;
            else if (transactionId != null)
                ret = db.WholeSellerOrderTransactions.Where(wot => wot.TransactionId == transactionId).ToList();
            else
                ret = db.WholeSellerOrderTransactions.Where(wot => wot.WholeSellerOrderId == wholeSellerOrderId).ToList();
            var list= ret.Select(wot => 
                        new SettledOrderOfTransactionViewModel(
                                WholeSellerOrderDataSource.RetrieveWholeSellerOrder(wot.WholeSellerOrderId,db), 
                                wot.PaidAmount)
                       );
            return list;
        }
    }
}
