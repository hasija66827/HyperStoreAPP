using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class TCustomerOrder
    {
        public Guid CustomerOrderId { get; set; }

        [Required]
        public string CustomerOrderNo { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalQuantity { get; set; }
        public int TotalItems { get; set; }
        public decimal CartAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Tax { get; set; }

        // Amount Settled Up Till date, always -leq BillAmount.
        public decimal SettledPayedAmount { get; set; }
        public decimal PayedAmount { get; set; }

        public decimal InterestRate { get; set; }


        public TCustomerOrder()
        {
        }

        public Guid CustomerId { get; set; }
        public TCustomer Customer { get; set; }
    }
}