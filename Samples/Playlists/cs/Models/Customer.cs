using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Models
{
    public class TCustomer : ICustomer
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

        [Required]
        public decimal? NetWorth { get; set; }
    }

    public interface ICustomer
    {
        string Address { get; set; }
        string GSTIN { get; set; }
        [Required]
        string MobileNo { get; set; }
        [Required]
        string Name { get; set; }
    }
}