using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class OrderSummaryViewModel
    {
        private float _totalSales;
        public float TotalSales { get { return this._totalSales; } }
        private float _totalSalesWithDiscount;
        public float TotalSalesWithDiscount { get { return this._totalSalesWithDiscount; } }
        private float _receivedNow;
        public float ReceivedNow { get { return this._receivedNow; } }
        private float _receivedLater;
        public float ReceivedLater { get { return this._receivedLater; } }
        public OrderSummaryViewModel()
        {
            _totalSales = 0;
            _totalSalesWithDiscount = 0;
            _receivedNow = 0;
            _receivedLater = 0;
        }
    }
}
