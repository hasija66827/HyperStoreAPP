using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class TProductTag
    {
        [Required]
        public Guid? ProductTagId { get; set; }

        public TProductTag() { }

        [Required]
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public Guid? TagId { get; set; }
        public TTag Tag { get; set; }
    }
}