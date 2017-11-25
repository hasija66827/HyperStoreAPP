using System;
using System.Collections.Generic;
using System.Linq;
using Models;
namespace HyperStoreServiceAPP.DTO.InsightsDTO
{
    public class SalesInsightsDTO : InsightsDTO
    {
        public SalesInsightsDTO(IRange<DateTime> dateRange) : base(dateRange) { }
    }

    public class SalesInsight
    {
        public List<SalesOrderInsight> SalesOrderInsight;
        public List<TransactionInsight> TransactionInsight;
    }

    public class SalesOrderInsight
    {
        public decimal? MoneyIn;
        public decimal? MoneyOut;
        public DateTime? Date;
        public decimal? TotalSales;
        public decimal? TotalPurchase;
    }

    public class TransactionInsight
    {
        public decimal? MoneyIn;
        public decimal? MoneyOut;
        public DateTime? Date;
    }

}