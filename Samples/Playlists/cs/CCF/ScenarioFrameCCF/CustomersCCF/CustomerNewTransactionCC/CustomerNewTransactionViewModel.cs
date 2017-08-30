using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public sealed class CustomerNewTransactionViewModel
    {
        public TCustomer Customer { get; set; }
        public decimal ReceivingAmount { get; set; }
        public string OptionalDescription { get; set; }
        public decimal UpdatedWalletBalance { get { return (decimal)this.Customer.WalletBalance + this.ReceivingAmount; } }
        public string ProceedToReceive { get { return "Proceed To Receive " + Utility.ConvertToRupee(ReceivingAmount); } }
        public CustomerNewTransactionViewModel() { }
    }
}
