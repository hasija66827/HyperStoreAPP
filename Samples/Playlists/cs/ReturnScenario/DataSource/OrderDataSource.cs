using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate;
using MasterDetailApp.ViewModel;
namespace MasterDetailApp.Data
{
    public class OrderDataSource
    {
        private static List<OrderViewModel> _Orders;
        public static List<OrderViewModel> Orders { get { return _Orders; } }
        public OrderDataSource()
        {
            // Initializing member variable all orders.
            RetrieveOrdersAsync();
        }
        public static void RetrieveOrdersAsync()
        {
            List<DatabaseModel.CustomerOrder> _customerOrders;
            List<DatabaseModel.Customer> _customers;
            using (var db = new DatabaseModel.RetailerContext())
            {
                _customerOrders = db.CustomerOrders.ToList();
                _customers = db.Customers.ToList();
            }
            var query = _customers
                        .Join(_customerOrders,
                                customer => customer.CustomerId,
                                customerOrder => customerOrder.CustomerId,
                                (customer, customerOrder) => new OrderViewModel(customerOrder.CustomerOrderId, customerOrder.BillAmount, customer.MobileNo, customerOrder.OrderDate, customerOrder.PaidAmount))
                        .OrderByDescending(order => order.OrderDate);
            _Orders = query.ToList();
        }
     
        public static List<OrderViewModel> RetrieveOrdersByMobileNumber(string MobileNumber)
        {
            var orderByMobileNumber = _Orders.Where(order => order.CustomerMobileNo == MobileNumber);
            return orderByMobileNumber.ToList();
        }

        // #perf Please retrieve required attribute from the database only and if possible merge the three query into one.
        //Retrieving specific order Details from customerOrderProducts using customerOrderID as an input.
        public static List<OrderDetaiViewModel> RetrieveOrderDetails(Guid customerOrderId)
        {
            var db = new DatabaseModel.RetailerContext();
            var customerOrderProducts = db.CustomerOrderProducts
                    .Where(customerOrderProduct => customerOrderProduct.CustomerOrderId == customerOrderId).ToList();
            var products = db.Products.ToList();
            var orderDetails=customerOrderProducts.Join(products,
                                                       customerOrderProduct => customerOrderProduct.ProductId,
                                                       product => product.ProductId,
                                                       (customerOrderProduct, product) => new OrderDetaiViewModel(
                                                                       customerOrderProduct.DiscountPerSnapShot,
                                                                       customerOrderProduct.DisplayCostSnapShot,
                                                                       product.Name,
                                                                       customerOrderProduct.QuantityPurchased)).ToList();
            return orderDetails;
        }
    }
}
