using HyperStoreServiceAPP.DTO;
using SDKTemplate;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperStoreService.CustomModels
{
    class ProductMetadata
    {
        public IRange<decimal?> DiscountPerRange { get; set; }
        public IRange<float?> QuantityRange { get; set; }
    }
}
