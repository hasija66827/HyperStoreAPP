using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSellerOrderViewModel
    {
        private Guid _wholeSellerOrderId;
        public Guid WholeSellerOrderId { get { return this._wholeSellerOrderId; } }
        private DateTime _orderDate;
        public DateTime OrderDate { get { return this._orderDate; } }
        private DateTime _dueDate;
        public DateTime DueDate { get { return this._dueDate; } }
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
        private Guid _wholeSellerId;
        public Guid WholesellerId { get { return this._wholeSellerId; } }
        public WholeSellerOrderViewModel(Guid _wholeSellerOrderId, DateTime orderDate, DateTime dueDate, float billAmount,
            float paidAmount, Guid wholeSellerId)
        {
            this._wholeSellerOrderId = _wholeSellerOrderId;
            this._orderDate = orderDate;
            this._dueDate = dueDate;
            this._billAmount = billAmount;
            this._paidAmount = paidAmount;
            this._wholeSellerId = wholeSellerId;
        }
    }
}
