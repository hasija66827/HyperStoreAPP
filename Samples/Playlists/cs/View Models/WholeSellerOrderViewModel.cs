using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;

namespace SDKTemplate
{
    public class WholeSellerOrderViewModel
    {
        private Guid _wholeSellerOrderId;
        public Guid WholeSellerOrderId { get { return this._wholeSellerOrderId; } }

        public DateTime OrderDate { get { return this._orderDate; } }
        private DateTime _orderDate;

        public DateTime DueDate { get { return this._dueDate; } }
        private DateTime _dueDate;
       
        private float _billAmount;
        public float BillAmount { get { return this._billAmount; } }
        private float _paidAmount;
        public float PaidAmount { get { return this._paidAmount; } }
        private float _intrestRate;
        public float IntrestRate
        {
            get
            { return this._intrestRate; }
        }

        private WholeSellerViewModel _wholeSeller;
        public WholeSellerViewModel WholeSeller{ get { return this._wholeSeller; } }

        private Guid _wholeSellerId;
        public Guid WholesellerId { get { return this._wholeSellerId; } }


        public string FormattedOrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month hour minute");
                return formatter.Format(this._orderDate);
            }
        }

        public string FormattedDueDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this._dueDate);
            }
        }

        public string FormattedPaidBillAmount {
           get { return this.PaidAmount.ToString() + "/" + this.BillAmount.ToString(); }
        }
        public WholeSellerOrderViewModel(Guid _wholeSellerOrderId, DateTime orderDate, DateTime dueDate, float billAmount,
            float paidAmount, Guid wholeSellerId)
        {
            this._wholeSellerOrderId = _wholeSellerOrderId;
            this._orderDate = orderDate;
            this._dueDate = dueDate;
            this._billAmount = billAmount;
            this._paidAmount = paidAmount;
            this._wholeSellerId = wholeSellerId;
            this._wholeSeller = WholeSellerDataSource.GetWholeSellerById(wholeSellerId);
        }
    }
}
