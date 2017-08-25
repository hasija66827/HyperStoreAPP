using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


using System.Threading.Tasks;


namespace Models
{
    public class TTransaction
    {
        public Guid TransactionId { get; set; }
        public bool IsCredit { get; set; }
        [Required]
        public string TransactionNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal WalletSnapshot { get; set; }
        public Guid SupplierId { get; set; }
        public TSupplier Supplier { get; set; }
        public TTransaction() {
        }
    }
}