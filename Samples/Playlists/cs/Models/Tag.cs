using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Models
{
    public class TTag
    {
        public Guid? TagId { get; set; }
        [Required]
        public string TagName { get; set; }

        public TTag() { }
    }
}