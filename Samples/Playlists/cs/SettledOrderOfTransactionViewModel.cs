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
        public SettledOrderOfTransactionViewModel() { }
        public SettledOrderOfTransactionViewModel(WholeSellerOrderViewModel wholsellerOrder, float creditedAmountFromTransaction ) {
            this.WholeSellerOrder = wholsellerOrder;
            this.CreditedAmountFromTransaction = creditedAmountFromTransaction;
        }
    }
}
