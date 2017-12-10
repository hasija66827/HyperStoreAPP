using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.DTO
{
    public class TagDTO
    {
        [Required]
        [StringLength(24)]
        public string TagName { get; set; }
    }
}
