using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate.View_Models;
namespace SDKTemplate
{


    public class TransactionDataSource
    {
        /*public static List<TransactionViewModel> RetrieveTransaction(Guid WholeSellerId)
        {

        }*/
        public static bool CreateTransaction(TransactionViewModel transactionViewModel)
        {
            var db = new DatabaseModel.RetailerContext();
            db.Transactions.Add(new DatabaseModel.Transaction(transactionViewModel));
            db.SaveChanges();
            return true;
        }
    }
}
