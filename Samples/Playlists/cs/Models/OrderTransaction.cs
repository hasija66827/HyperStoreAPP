using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class OrderTransaction
    {
        public Guid OrderTransactionId { get; set; }
        //It is amount paid from the transaction amount.
        //It is null, if the transaction was credit transaction o.w. it is less than equal to transaction amount.
        public decimal? PaidAmount { get; set; }

        public bool IsPaymentComplete { get; set; }

        public OrderTransaction()
        {
        }

        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}