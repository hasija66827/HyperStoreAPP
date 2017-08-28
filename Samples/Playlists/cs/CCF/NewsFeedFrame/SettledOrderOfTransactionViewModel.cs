using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SettledOrderOfTransactionViewModel
    {
        public SupplierOrderViewModel WholeSellerOrder { get; set; }
        public decimal CreditedAmountFromTransaction { get; set; }
        public SupplierTransactionViewModel Transaction { get; set; }
        public SettledOrderOfTransactionViewModel() { }
        
        public SettledOrderOfTransactionViewModel(decimal creditedAmountFromTransaction, DatabaseModel.Transaction transaction)
        {
            /*
            this.WholeSellerOrder =null;
            this.Transaction = new SupplierTransactionViewModel(transaction);
            this.CreditedAmountFromTransaction = creditedAmountFromTransaction;*/
        }

        public SettledOrderOfTransactionViewModel(DatabaseModel.WholeSellerOrder wholsellerOrder, decimal creditedAmountFromTransaction)
        {
            /*
            this.WholeSellerOrder = new SupplierOrderViewModel(wholsellerOrder);
            this.Transaction = null;
            this.CreditedAmountFromTransaction = creditedAmountFromTransaction;*/
        }

    }
}
