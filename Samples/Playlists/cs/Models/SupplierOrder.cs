using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
  public class TSupplierOrder
    {
        public Guid SupplierOrderId { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal PayingAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalQuantity { get; set; }
        [Required]
        public string SupplierOrderNo { get; set; }
        public Guid SupplierId { get; set; }
        public TSupplier Supplier { get; set; }
    }
}