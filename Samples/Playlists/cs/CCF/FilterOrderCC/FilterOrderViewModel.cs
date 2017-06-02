using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class FilterOrderViewModel
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
            set { this._endDate = value;}
        }
        public FilterOrderViewModel()
        {
            _startDate = DateTime.Now;
            _endDate = DateTime.Now;
        }
    }
}
