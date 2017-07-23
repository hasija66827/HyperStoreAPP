using SDKTemplate.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SettledOrderOfTransactionViewModel
    {
        public WholeSellerOrderViewModel WholeSellerOrder { get; set; }
        public float CreditedAmountFromTransaction { get; set; }
        public TransactionViewModel Transaction { get; set; }
        public SettledOrderOfTransactionViewModel() { }
        
        public SettledOrderOfTransactionViewModel(float creditedAmountFromTransaction, DatabaseModel.Transaction transaction)
        {
            this.WholeSellerOrder =null;
            this.Transaction = new TransactionViewModel(transaction);
            this.CreditedAmountFromTransaction = creditedAmountFromTransaction;
        }

        public SettledOrderOfTransactionViewModel(DatabaseModel.WholeSellerOrder wholsellerOrder, float creditedAmountFromTransaction)
        {
            this.WholeSellerOrder = new WholeSellerOrderViewModel(wholsellerOrder);
            this.Transaction = null;
            this.CreditedAmountFromTransaction = creditedAmountFromTransaction;
        }

    }
}
