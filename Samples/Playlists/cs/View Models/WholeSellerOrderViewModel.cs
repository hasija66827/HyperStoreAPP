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
        public float IntrestRate { get { return this._intrestRate; } }
        private WholeSellerViewModel _wholeSeller;
        public WholeSellerViewModel WholeSeller { get { return this._wholeSeller; } }
        private Guid? _wholeSellerId;
        public Guid? WholesellerId { get { return this._wholeSellerId; } }
        private List<WholeSellerOrderDetail> _wholeSellerOrderDetails;
        public List<WholeSellerOrderDetail> WholeSellerOrderDetails {
            get { 
                //Retrieves the order details on demand
                if(this._wholeSellerOrderDetails.Count==0)
                    this._wholeSellerOrderDetails = WholeSellerOrderProductDataSource.RetrieveOrderDetails(_wholeSellerOrderId);
                return this._wholeSellerOrderDetails;

            } }

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

        public string FormattedPaidBillAmount
        {
            get { return this.PaidAmount.ToString() +"\u20b9" +"/" + this.BillAmount.ToString()+"\u20b9"; }
        }

        public WholeSellerOrderViewModel(DatabaseModel.WholeSellerOrder wo)
        {
            this._wholeSellerOrderId = wo.WholeSellerOrderId;
            this._wholeSellerOrderDetails = new List<WholeSellerOrderDetail>();
            this._orderDate = wo.OrderDate;
            this._dueDate = wo.DueDate;
            this._billAmount = wo.BillAmount;
            this._paidAmount = wo.PaidAmount;
            this._wholeSellerId = wo.WholeSellerId;
            this._wholeSeller = null;
        }

        public WholeSellerOrderViewModel(Guid _wholeSellerOrderId, DateTime orderDate, DateTime dueDate, float billAmount,
            float paidAmount, Guid wholeSellerId)
        {
            this._wholeSellerOrderId = _wholeSellerOrderId;
            this._wholeSellerOrderDetails = new List<WholeSellerOrderDetail>();
            this._orderDate = orderDate;
            this._dueDate = dueDate;
            this._billAmount = billAmount;
            this._paidAmount = paidAmount;
            this._wholeSellerId = wholeSellerId;
            this._wholeSeller = WholeSellerDataSource.GetWholeSellerById(wholeSellerId);
        }
    }

    public class WholeSellerOrderDetail
    {
        private string _barCode;
        public string BarCode
        {
            get { return this._barCode; }
            set { this._barCode = value; }
        }
        private string _name;
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        private float _purchasePrice;
        public float PurchasePrice
        {
            get { return this._purchasePrice; }
            set
            {
                this._purchasePrice = value;
            }
        }
        private Int32 _quantityPurchased;
        public Int32 QuantityPurchased
        {
            get { return this._quantityPurchased; }
            set
            {
                this._quantityPurchased = value;
            }
        }
        public float NetValue
        {
            get { return this._quantityPurchased * this._purchasePrice; }
        }
        public WholeSellerOrderDetail(string barcode ,string name,
            float purchasePrice, Int32 quantityPurchased)
        {
            this._barCode =barcode;
            this._name = name;
            this._purchasePrice = purchasePrice;
            this._quantityPurchased = quantityPurchased;
        }
        public WholeSellerOrderDetail() { }
    }
}
