using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetailApp.Data
{
    public class Order
    {
        public Guid CustomerOrderId { get; set; }
        public float BillAmount { get; set; }
        public string CustomerMobileNo { get; set; }
        public float PaidAmount { get; set; }
        public DateTime OrderDate { get; set; }
        private List<OrderDetail> _orderDetails;
        public List<OrderDetail> OrderDetails
        {
            get
            {
                if (this._orderDetails.Count == 0)
                {
                    //TODO: make database call with a customerOrderID
                    this._orderDetails.Add(new OrderDetail());
                    this._orderDetails.Add(new OrderDetail());
                }
                return this._orderDetails;
            }
        }
        public Order(Guid customerOrderId, float billAmount, string customerMobileNo, DateTime orderDate, float paidAmount)
        {
            this.CustomerOrderId = customerOrderId;
            this.BillAmount = billAmount;
            this.CustomerMobileNo = customerMobileNo;
            this.OrderDate = orderDate;
            this.PaidAmount = paidAmount;
            this._orderDetails = new List<OrderDetail>();
        }
    }
    public class OrderDetail
    {
        public float DisplayPriceSnapShot { get; set; }
        public float DiscountPerSnapShot { get; set; }
        public string ProductName { get; set; }
        public int QtyPurchased { get; set; }
        public OrderDetail() {
            DisplayPriceSnapShot = 0;
            DiscountPerSnapShot = 0;
            ProductName = "xxxxx";
            QtyPurchased = 0;
        }

    }
}
