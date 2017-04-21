using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
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
                    var db = new RetailerContext();
                    // #perf Please retrieve required attribute from the database only.
                    //Retrieving specific order Details from customerOrderProducts using customerOrderID as an input.
                    var customerOrderProducts = db.CustomerOrderProducts
                     .Where(customerOrderProduct => customerOrderProduct.CustomerOrderId == this.CustomerOrderId).ToList();
                    var products = db.Products.ToList();
                     this._orderDetails=customerOrderProducts.Join(products,
                                                        customerOrderProduct => customerOrderProduct.ProductId,
                                                        product => product.ProductId,
                                                        (customerOrderProduct, product) => new OrderDetail(
                                                                        customerOrderProduct.DiscountPerSnapShot,
                                                                        customerOrderProduct.DisplayCostSnapShot,
                                                                        product.Name,
                                                                        customerOrderProduct.QuantityPurchased)).ToList();
   
                    // Explicitly converting each record in customerOrderProduct into OrderDetail.
                    //customerOrderProducts.ForEach(cop => this._orderDetails.Add((OrderDetail)cop));
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
        public float DiscountPerSnapShot { get; set; }
        public float DisplayPriceSnapShot { get; set; }
        public string ProductName { get; set; }
        public int QtyPurchased { get; set; }
        public OrderDetail()
        {
            DisplayPriceSnapShot = 0;
            DiscountPerSnapShot = 0;
            ProductName = "xxxxx";
            QtyPurchased = 0;
        }
        public OrderDetail(float discountPerSnapShot, float displayPriceSnapshot, string productName, int qtyPurchased)
        {
            this.DiscountPerSnapShot = discountPerSnapShot;
            this.DisplayPriceSnapShot = displayPriceSnapshot;
            this.ProductName = productName;
            this.QtyPurchased = qtyPurchased;
        }
        //#not required 
        public static explicit operator MasterDetailApp.Data.OrderDetail(DatabaseModel.CustomerOrderProduct c)
        {
            OrderDetail orderDetail = new OrderDetail();
            orderDetail.DiscountPerSnapShot = c.DiscountPerSnapShot;
            orderDetail.DisplayPriceSnapShot = c.DisplayCostSnapShot;
            orderDetail.QtyPurchased = c.QuantityPurchased;
            orderDetail.ProductName = "Intializing";
            return orderDetail;
        }
    }
}
