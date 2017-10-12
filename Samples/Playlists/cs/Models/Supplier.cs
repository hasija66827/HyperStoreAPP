using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class TSupplier: supplierInterface
    {
        public Guid SupplierId { get; set; }
        public string Address { get; set; }
        public string GSTIN { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal WalletBalance { get; set; }

        public TSupplier() {
        }
    }

    public interface supplierInterface
    {
         string Address { get; set; }
         string GSTIN { get; set; }
        [Required]
         string MobileNo { get; set; }
        [Required]
         string Name { get; set; }
         decimal WalletBalance { get; set; }
    }

}