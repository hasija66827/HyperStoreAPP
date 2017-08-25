using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class TProduct
    {
        public Guid ProductId { get; set; }
        public virtual decimal? CGSTPer { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public virtual decimal? DisplayPrice { get; set; }
        [Required]
        public virtual decimal? DiscountPer { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal RefillTime { get; set; }
        public virtual decimal? SGSTPer { get; set; }
        public decimal Threshold { get; set; }
        public decimal TotalQuantity { get; set; }
    }
}