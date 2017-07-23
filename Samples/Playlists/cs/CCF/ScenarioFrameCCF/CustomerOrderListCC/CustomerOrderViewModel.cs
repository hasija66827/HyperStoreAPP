using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
using Windows.Globalization.DateTimeFormatting;
using SDKTemp.Data;
namespace SDKTemplate
{
    public class CustomerOrderViewModel
    {
        private Guid _customerOrderId;
        public Guid CustomerOrderId { get { return this._customerOrderId; } }

        private float _billAmount;
        public float BillAmount { get { return this._billAmount; } }

        private string _customerOrderNo;
        public string CustomerOrderNo { get { return this._customerOrderNo; } }

        private float _discountedBillAmount;
        public float DiscountedBillAmount { get { return this._discountedBillAmount; } }

        private string _customerMobileNo;
        public string CustomerMobileNo { get { return this._customerMobileNo; } }

        private DateTime _orderDate;
        public DateTime OrderDate
        { get { return this._orderDate; } }

        private float _paidAmount;
        public float PaidAmount { get { return this._paidAmount; } }

        public string FormattedPaidBillAmount
        {
            get { return this.PaidAmount.ToString() + "\u20b9" + "/" + this.DiscountedBillAmount.ToString() + "\u20b9"; }
        }

        public string FormattedOrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month hour minute");
                return formatter.Format(this._orderDate);
            }
        }

        private List<CustomerOrderDetailViewModel> _orderDetails;
        public List<CustomerOrderDetailViewModel> OrderDetails
        {
            get
            {
                if (this._orderDetails.Count == 0)
                {
                    this._orderDetails = CustomerOrderDataSource.RetrieveOrderDetails(this.CustomerOrderId);
                }
                return this._orderDetails;
            }
        }

        public CustomerOrderViewModel()
        {
            this._customerOrderId = Guid.NewGuid();
            this._customerOrderNo = Utility.GenerateCustomerOrderNo();
            this._billAmount = 0;
            this._discountedBillAmount = 0;
            this._customerMobileNo = "";
            this._orderDate = DateTime.Now;
            this._orderDetails = new List<CustomerOrderDetailViewModel>();
            this._paidAmount = 0;
        }

        public CustomerOrderViewModel(DatabaseModel.Customer c, DatabaseModel.CustomerOrder co)
        {
            this._customerOrderId = co.CustomerOrderId;
            this._customerOrderNo = co.CustomerOrderNo;
            this._billAmount = co.BillAmount;
            this._discountedBillAmount = co.DiscountedAmount;
            this._customerMobileNo = c.MobileNo;
            this._orderDate = co.OrderDate;
            this._paidAmount = co.PayingNow;
            this._orderDetails = new List<CustomerOrderDetailViewModel>();
        }
    }

    public class CustomerOrderDetailViewModel : SDKTemplate.ProductViewModelBase
    {
        private Int32 _quantityPurchased;
        public Int32 QuantityPurchased { get { return this._quantityPurchased; } }

        private float _netValue;
        public float NetValue { get { return Utility.RoundInt32(this._netValue); } }

        public CustomerOrderDetailViewModel() : base()
        {
            this._quantityPurchased = 0;
        }

        public CustomerOrderDetailViewModel(Guid productId, string barCode, float discountPerSnapShot,
            float displayPriceSnapshot, string name, int qtyPurchased)
            : base(productId, barCode, name, displayPriceSnapshot, discountPerSnapShot, 0, 0, null)
        {
            this._quantityPurchased = qtyPurchased;
            this._netValue = this._sellingPrice * this._quantityPurchased;
        }
    }
}
