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
        // Retrieves all the orders.
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

        // #perf if possible merge the three query into one.
        //Retrieving specific order Details from customerOrderProducts using customerOrderID as an input.
        public static List<OrderDetailViewModel> RetrieveOrderDetails(Guid customerOrderId)
        {
            var db = new DatabaseModel.RetailerContext();
            var customerOrderProducts = db.CustomerOrderProducts
                    .Where(customerOrderProduct => customerOrderProduct.CustomerOrderId == customerOrderId).ToList();
            var products = db.Products.ToList();
            var orderDetails = customerOrderProducts.Join(products,
                                                       customerOrderProduct => customerOrderProduct.ProductId,
                                                       product => product.ProductId,
                                                       (customerOrderProduct, product) => new OrderDetailViewModel(
                                                                       product.ProductId,
                                                                       product.BarCode,
                                                                       customerOrderProduct.DiscountPerSnapShot,
                                                                       customerOrderProduct.DisplayCostSnapShot,
                                                                       product.Name,
                                                                       customerOrderProduct.QuantityPurchased)).ToList();
            return orderDetails;
        }

        public static void PlaceOrder(PageNavigationParameter pageNavigationParameter)
        {
            BillingViewModel billingViewModel = pageNavigationParameter.BillingViewModel;
            CustomerViewModel customerViewModel = pageNavigationParameter.CustomerViewModel;
            var db = new DatabaseModel.RetailerContext();
            var customerOrder = new DatabaseModel.CustomerOrder(customerViewModel.CustomerId,
                billingViewModel.TotalBillAmount,
                billingViewModel.DiscountedBillAmount,
                customerViewModel.WalletBalance);
            // Creating Entity Record in customerOrder.
            db.CustomerOrders.Add(customerOrder);
            //TODO: without doing additional saving, foreign key constraints failed. See how can you make whole transaction atomic.
            db.SaveChanges();
            foreach (var product in billingViewModel.Products)
            {
                // Adding each product purchased in the order into the Entity CustomerOrderProduct.
                var customerOrderProduct = new DatabaseModel.CustomerOrderProduct(customerOrder.CustomerOrderId,
                    product.ProductId,
                    product.DiscountPer,
                    product.DisplayPrice,
                    product.QuantityPurchased);
                db.CustomerOrderProducts.Add(customerOrderProduct);
                // TODO: Update the product entity with new total quantity.
            }
            // Saving the order.
            db.SaveChanges();
        }
    }
}
