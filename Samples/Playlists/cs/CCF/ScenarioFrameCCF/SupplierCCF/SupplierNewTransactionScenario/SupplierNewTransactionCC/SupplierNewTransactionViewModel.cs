using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SupplierNewTransactionViewModel
    {
        public TSupplier Supplier { get; set; }
        public decimal PayingAmount { get; set; }
        public string OptionalDescription { get; set; }
        public decimal UpdatedWalletBalance { get { return this.Supplier.WalletBalance - this.PayingAmount; } }
        public string ProceedToPay { get { return "Proceed To Pay " + Utility.ConvertToRupee(PayingAmount); } }
        public SupplierNewTransactionViewModel() { }
    }
}
