using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class TProduct
    {
        public Guid ProductId { get; set; }
        public float? CGSTPer { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public decimal? DisplayPrice { get; set; }
        public float DiscountPer { get; set; }
        [Required]
        public string Name { get; set; }
        public Int32 RefillTime { get; set; }
        public float? SGSTPer { get; set; }
        public Int32 Threshold { get; set; }
        public float TotalQuantity { get; set; }
    }
}