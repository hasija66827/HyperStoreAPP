using SDKTemplate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public bool IsCredit { get; set; }
        [Required]
        public string TransactionNo { get; set; }
        public string OrderNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }
        public decimal WalletSnapshot { get; set; }
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public Guid? PaymentOptionId { get; set; }
        public PaymentOption PaymentOption { get; set; }
        public Transaction() {
        }
    }
}