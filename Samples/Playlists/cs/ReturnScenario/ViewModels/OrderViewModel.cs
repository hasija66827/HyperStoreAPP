using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
using Windows.Globalization.DateTimeFormatting;
using MasterDetailApp.Data;
namespace MasterDetailApp.ViewModel
{
    public class OrderViewModel
    {
        public Guid CustomerOrderId { get; set; }
        public string BillAmount { get { return "Bill \u20b9" + this._billAmount; } }
        private float _billAmount;

        public string CustomerMobileNo { get; set; }
        public string PaidAmount{ get { return "Paid \u20b9" + this._paidAmount; } }
        private float _paidAmount;
        public string OrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month hour minute");
                return formatter.Format(this._orderDate);
            }
        }
        private DateTime _orderDate;
        private List<OrderDetaiViewModel> _orderDetails;
        public List<OrderDetaiViewModel> OrderDetails
        {
            get
            {
                if (this._orderDetails.Count == 0)
                {
                    this._orderDetails = OrderDataSource.RetrieveOrderDetails(this.CustomerOrderId);
                }
                return this._orderDetails;
            }
        }
        public OrderViewModel(Guid customerOrderId, float billAmount, string customerMobileNo, DateTime orderDate, float paidAmount)
        {
            this.CustomerOrderId = customerOrderId;
            this._billAmount = billAmount;
            this.CustomerMobileNo = customerMobileNo;
            this._orderDate = orderDate;
            this._paidAmount = paidAmount;
            this._orderDetails = new List<OrderDetaiViewModel>();
        }
    }
    public class OrderDetaiViewModel
    {
        public float DiscountPerSnapShot { get; set; }
        public float DisplayPriceSnapShot { get; set; }
        public string ProductName { get; set; }
        public int QtyPurchased { get; set; }
        public OrderDetaiViewModel()
        {
            DisplayPriceSnapShot = 0;
            DiscountPerSnapShot = 0;
            ProductName = "xxxxx";
            QtyPurchased = 0;
        }
        public OrderDetaiViewModel(float discountPerSnapShot, float displayPriceSnapshot, string productName, int qtyPurchased)
        {
            this.DiscountPerSnapShot = discountPerSnapShot;
            this.DisplayPriceSnapShot = displayPriceSnapshot;
            this.ProductName = productName;
            this.QtyPurchased = qtyPurchased;
        }
    }
}
