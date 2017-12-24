using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperStoreServiceAPP.DTO
{
    #region SupplierTransaction
    public class SupplierTransactionFilterCriteriaDTO
    {
        [Required]
        public Guid? SupplierId { get; set; }
    }

    public class TransactionDTO
    {
        [Required]
        public bool? IsCredit { get; set; }

        [Required]
        public Guid? PersonId { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public decimal? TransactionAmount { get; set; }

        public string Description { get; set; }

        [Required]
        public Guid? PaymentOptionId { get; set; }
    }
    #endregion
}
