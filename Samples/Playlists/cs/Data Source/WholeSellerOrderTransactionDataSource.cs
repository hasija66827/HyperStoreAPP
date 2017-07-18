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
        public static bool CreateWholeSellerOrderTransaction(Guid transactionId, Guid wholeSellerOrderId, DatabaseModel.RetailerContext db)
        {
            db.WholeSellerOrderTransactions.Add(new DatabaseModel.WholeSellerOrderTransaction(transactionId, wholeSellerOrderId));
            return true;
        }
    }
}
