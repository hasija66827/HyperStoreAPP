using SDKTemplate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperStoreServiceAPP.DTO
{
    public class SupplierDTO
    {
        [Required]
        public EntityType? EntityType { get; set; }
        public string Address { get; set; }
        public string GSTIN { get; set; }

        public DateTime? LastCalled { get; set; }

        [Required]
        [RegularExpression(@"[987]\d{9}")]
        public string MobileNo { get; set; }

        [Required]
        public string Name { get; set; }


        [Range(0, 5)]
        public int? Rating { get; set; }
    }

    public class SupplierFilterCriteriaDTO
    {
        [Required]
        public EntityType? EntityType { get; set; }
        public IRange<decimal> WalletAmount { get; set; }
        public Guid? SupplierId { get; set; }
    }
}
