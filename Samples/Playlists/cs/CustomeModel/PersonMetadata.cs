using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.CustomeModel
{
    public class PersonMetadata
    {
        public IRange<double> WalletBalanceRange;
        public int TotalRecords;
    }
    public class PersonMetadataDTO
    {
        [Required]
        public EntityType? EntityType { get; set; }
    }
}
