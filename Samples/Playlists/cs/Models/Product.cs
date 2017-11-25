using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class Product
    {
        [Required]
        public Guid? ProductId { get; set; }

        [Required]
        public decimal? CGSTPer { get; set; }

        [Required]
        [StringLength(15)]
        public string Code { get; set; }

        [Required]
        public decimal? MRP { get; set; }

        [Required]
        public decimal? DiscountPer { get; set; }

        [Required]
        [StringLength(24)]
        public string Name { get; set; }

        public Int32? HSN { get; set; }

        [Required]
        public decimal? SGSTPer { get; set; }
        [Required]
        public decimal? Threshold { get; set; }
        public decimal TotalQuantity { get; set; }

        public Guid? LatestSupplierId { get; set; }
        public Person LatestSupplier { get; set; }
    }
}