using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class TSupplierOrderProduct
    {
        public Guid SupplierOrderProductId { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal QuantityPurchased { get; set; }

        public TSupplierOrderProduct() {
        }

        [Required]
        public Guid? SupplierOrderId { get; set; }
        public TSupplierOrder SupplierOrder { get; set; }

        [Required]
        public Guid? ProductId { get; set; }
        public TProduct Product { get; set; }
    }
}