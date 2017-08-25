using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Models
{
    public class TCustomer
    {
        public Guid? CustomerId { get; set; }
        public string Address { get; set; }
        public string GSTIN { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal? WalletBalance { get; set; }

     //TODO: #DB, Name and MobileNo should be unique, customerId should not be null in database, although in model it can be null
    }
}