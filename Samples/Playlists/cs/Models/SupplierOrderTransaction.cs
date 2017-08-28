using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class TSupplierOrderTransaction
    {
        public Guid SupplierOrderTransactionId { get; set; }
        //It is amount paid from the transaction amount.
        //It is null, if the transaction was credit transaction o.w. it is less than equal to transaction amount.
        public decimal? PaidAmount { get; set; }

        public bool IsPaymentComplete { get; set; }

        public TSupplierOrderTransaction()
        {
        }

        public Guid TransactionId { get; set; }
        public TSupplierTransaction Transaction { get; set; }

        public Guid SupplierOrderId { get; set; }
        public TSupplierOrder SupplierOrder { get; set; }
    }
}