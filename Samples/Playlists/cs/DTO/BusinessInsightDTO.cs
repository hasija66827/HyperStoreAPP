using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Windows.Globalization.DateTimeFormatting;

namespace HyperStoreServiceAPP.DTO.InsightsDTO
{
    public class BusinessInsightDTO : InsightsDTO
    {
        public BusinessInsightDTO(IRange<DateTime> dateRange) : base(dateRange) { }
    }

    public class BusinessInsight
    {
        public List<OrderInsight> OrderInsight;
        public List<TransactionInsight> TransactionInsight;
    }

    public class OrderInsight
    {
        public decimal? MoneyIn;
        public decimal? MoneyOut;
        public DateTime Date;
        public decimal? TotalSales;
        public decimal? TotalPurchase;
        public string FormattedOrderDate
        {
            get
            {
                return this.Date.ToString("MMM dd");
            }
        }
    }

    public class TransactionInsight
    {
        public decimal? MoneyIn;
        public decimal? MoneyOut;
        public DateTime Date;
        public string FormattedTransactionDate
        {
            get
            {
                return this.Date.ToString("MMM dd");
            }
        }
    }
}