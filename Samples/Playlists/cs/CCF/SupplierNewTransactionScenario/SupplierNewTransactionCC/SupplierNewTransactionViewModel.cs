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
        public decimal CreditAmount { get; set; }
        public SupplierNewTransactionViewModel()
        {
        }
    }
}
