using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperStoreServiceAPP.DTO
{
    #region CustomerPurchaseTrend
    public class CustomerPurchaseTrendDTO
    {
        [Required]
        public Guid? CustomerId { get; set; }
        [Required]
        public int? MonthsCount { get; set; }
    }
    #endregion

    #region ProductConsumption Trend
    public class ProductConsumptionTrendDTO
    {
        [Required]
        public Guid? ProductId { get; set; }
        [Required]
        public int? MonthsCount { get; set; }
    }
    #endregion
}
