using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class PriceQuotedBySupplier
    {
        public Guid PersonId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal QuantityPurchased { get; set; }
        public decimal PurchasePrice { get; set; }
        public Person Person { get; set; }
    }
}