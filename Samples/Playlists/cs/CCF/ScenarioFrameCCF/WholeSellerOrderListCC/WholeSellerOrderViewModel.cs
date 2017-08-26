using Models;
using SDKTemplate.Data_Source;
using SDKTemplate.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;

namespace SDKTemplate
{
    /// <summary>
    /// This class represents the Master Detail View Model
    /// Detail view show detail of the order and the trnasactions which completed the payment of the order.
    /// </summary>
    public class WholeSellerOrderViewModel
    {
        private Guid _wholeSellerOrderId;
        public Guid WholeSellerOrderId { get { return this._wholeSellerOrderId; } }

        private string _wholeSellerOrderNo;
        public string WholeSellerOrderNo { get { return this._wholeSellerOrderNo; } }

        private decimal _billAmount;
        public decimal BillAmount { get { return this._billAmount; } }

        private DateTime _dueDate;
        public DateTime DueDate { get { return this._dueDate; } }

        private decimal _intrestRate;
        public decimal IntrestRate { get { return this._intrestRate; } }

        private DateTime _orderDate;
        public DateTime OrderDate { get { return this._orderDate; } }

        private decimal _paidAmount;
        public decimal PaidAmount { get { return this._paidAmount; } }

        private TSupplier _wholeSeller;
        public TSupplier WholeSeller { get { return this._wholeSeller; } }

        private List<WholeSellerOrderDetailViewModel> _wholeSellerOrderDetails;
        public List<WholeSellerOrderDetailViewModel> WholeSellerOrderDetails
        {
            get
            {
                //Retrieves the order details on demand
                if (this._wholeSellerOrderDetails.Count == 0)
                    this._wholeSellerOrderDetails = WholeSellerOrderProductDataSource.RetrieveOrderDetails(_wholeSellerOrderId);
                return this._wholeSellerOrderDetails;
            }
        }

        public List<SettledOrderOfTransactionViewModel> _transactions;
        public List<SettledOrderOfTransactionViewModel> Transactions
        {
            get
            {
                if (this._transactions.Count == 0)
                    this._transactions = WholeSellerOrderTransactionDataSource.RetrieveWholeSellerOrderTransactions(null, this._wholeSellerOrderId);
                return this._transactions;
            }
        }

        public string FormattedOrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
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
            get { return Utility.FloatToRupeeConverter(this.PaidAmount) + "/" + Utility.FloatToRupeeConverter(this.BillAmount); }
        }

        public WholeSellerOrderViewModel(DatabaseModel.WholeSellerOrder wo, DatabaseModel.WholeSeller wholeSeller = null)
        {
            this._wholeSellerOrderId = wo.WholeSellerOrderId;
            this._wholeSellerOrderNo = wo.WholeSellerOrderNo;
            this._wholeSellerOrderDetails = new List<WholeSellerOrderDetailViewModel>();
            this._billAmount = wo.BillAmount;
            this._dueDate = wo.DueDate;
            this._intrestRate = 0;//TODO: need to updated
            this._orderDate = wo.OrderDate;
            this._paidAmount = wo.PaidAmount;
            this._transactions = new List<SettledOrderOfTransactionViewModel>();
            this._wholeSeller = null;
        }
    }

    public class WholeSellerOrderDetailViewModel
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

        private decimal _purchasePrice;
        public decimal PurchasePrice
        {
            get { return this._purchasePrice; }
            set
            {
                this._purchasePrice = value;
            }
        }

        private decimal _quantityPurchased;
        public decimal QuantityPurchased
        {
            get { return this._quantityPurchased; }
            set
            {
                this._quantityPurchased = value;
            }
        }

        public decimal NetValue
        {
            get { return this._quantityPurchased * this._purchasePrice; }
        }

        public WholeSellerOrderDetailViewModel(string barcode, string name,
            decimal purchasePrice, decimal quantityPurchased)
        {
            this._barCode = barcode;
            this._name = name;
            this._purchasePrice = purchasePrice;
            this._quantityPurchased = quantityPurchased;
        }
        public WholeSellerOrderDetailViewModel() { }
    }
}
