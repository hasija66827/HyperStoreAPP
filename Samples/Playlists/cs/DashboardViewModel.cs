using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class SalesInsightViewModel
    {
        public decimal? TotalSales { get; set; }
        public decimal? MoneyInBySales { get; set; }
        public decimal? MoneyInByExplicitTransaction { get; set; }

    }

    public class PurchaseInsightViewModel{
        public decimal? TotalPurchase { get; set; }
        public decimal? MoneyOutByPurchase { get; set; }
        public decimal? MoneyOutByExplicitTransactions { get; set; } 
    }
}
