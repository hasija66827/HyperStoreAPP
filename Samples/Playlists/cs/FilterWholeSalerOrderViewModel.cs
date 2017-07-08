using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class FilterWholeSalerOrderCriteria
    {
        private DateTime _startDate;
        public DateTime StartDate
        {
            get { return this._startDate; }
            set { this._startDate = value; }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get { return this._endDate; }
            set { this._endDate = value; }
        }

        private DateTime _dueDate;
        public DateTime DueDate
        {
            get { return this._dueDate; }
            set { this._dueDate = value; }
        }

        public bool? IncludePartiallyPaidOrdersOnly
        {
            get
            {
                var ret = this._includePartiallyPaidOrdersOnly != null ? this._includePartiallyPaidOrdersOnly : false;
                return ret;
            }
            set { this._includePartiallyPaidOrdersOnly = value; }
        }
        private bool? _includePartiallyPaidOrdersOnly;

        public FilterWholeSalerOrderCriteria()
        {
            _startDate = DateTime.Now.AddMonths(-2);
            _endDate = DateTime.Now;
            _dueDate = DateTime.Now.AddMonths(2);
            _includePartiallyPaidOrdersOnly = false;
        }
    }
}
