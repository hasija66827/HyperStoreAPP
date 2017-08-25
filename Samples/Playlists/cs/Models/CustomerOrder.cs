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
        
        public DateTime OrderDate { get; set; } 

       
        public decimal BillAmount { get; set; }
        
        public decimal DiscountedAmount { get; set; }

        // PayingNow = DiscountedBillAmount + AddingMoneyToWallet - UsingWalletAmount
      
        public bool IsPayingNow { get; set; }
        
        public bool IsUsingWallet { get; set; }
        
        public decimal PayingAmount { get; set; }
        public decimal UsingWalletAmount { get; set; }
        
        public TCustomerOrder()
        {
        }

        public Guid CustomerId { get; set; }
        public TCustomer Customer { get; set; }
    }
}