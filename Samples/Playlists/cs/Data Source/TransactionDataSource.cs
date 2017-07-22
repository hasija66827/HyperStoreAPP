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
        /// <summary>
        /// Retrieves all the transaction corresponding to a wholeseller.
        /// </summary>
        /// <param name="wholeSellerId"></param>
        /// <returns></returns>
        public static List<TransactionViewModel> RetreiveTransactionWholeSellerId(Guid wholeSellerId)
        {
            var db = new DatabaseModel.RetailerContext();
            var transactions = db.Transactions.Where(t => t.WholeSellerId == wholeSellerId).ToList();
            var res = transactions.Select(trans => new TransactionViewModel(trans));
            return res.ToList();
        }

        /// <summary>
        /// Creates a new transaction in database corresponding to the wholeseller.
        /// </summary>
        /// <param name="transactionViewModel"></param>
        /// <returns></returns>
        public static bool CreateTransaction(TransactionViewModel transactionViewModel, DatabaseModel.RetailerContext db=null)
        {
            if(db ==null)
               db= new DatabaseModel.RetailerContext();
            db.Transactions.Add(new DatabaseModel.Transaction(transactionViewModel));
            db.SaveChanges();
            return true;
        }


        public static float MakeTransaction(TransactionViewModel transactionViewModel, WholeSellerViewModel wholeSeller, DatabaseModel.RetailerContext db)
        {
            var creditAmount = transactionViewModel.CreditAmount;
            TransactionDataSource.CreateTransaction(transactionViewModel, db);
            var updatedWholeSellerWalletBalance = WholeSellerDataSource.UpdateWalletBalanceOfWholeSeller(db, wholeSeller, -creditAmount);
            WholeSellerOrderDataSource.SettleUpOrders(transactionViewModel, wholeSeller, db);
            return updatedWholeSellerWalletBalance;
        }
    }
}
