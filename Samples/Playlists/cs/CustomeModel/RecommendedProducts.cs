using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperStoreService.CustomModels
{
    public class RecommendedProduct
    {
        public Product Product { get; set; }
        public float Rating { get; set; }
        public DateTime LatestPurchaseDate { get; set; }
    }

    public class RecommendedProductForCustomer : RecommendedProduct
    {
        public double? ExpiredByDays { get; set; }
    }

    public class RecommendedProductForSupplier : RecommendedProduct
    {
        public double DeficientByNumber { get; set; }
    }
}
