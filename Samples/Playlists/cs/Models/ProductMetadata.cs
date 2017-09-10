using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    class TProductMetadata
    {
        public IRange<decimal?> DiscountPerRange { get; set; }
        public IRange<decimal> QuantityRange { get; set; }
    }
}
