using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate;
using DatabaseModel;
namespace MasterDetailApp.Data
{
    public class OrderDataSource
    {
        public static List<Order> GetAllOrders()
        {
            List<CustomerOrder> _customerOrders = new List<CustomerOrder>();
            List<Customer> _customers = new List<Customer>();
            using (var db = new RetailerContext())
            {
                _customerOrders = db.CustomerOrders.ToList();
                _customers = db.Customers.ToList();
            }
            var query = _customers
                        .Join(_customerOrders,
                                customer => customer.CustomerId,
                                customerOrder => customerOrder.CustomerId,
                                (customer, customerOrder) => new Order(customerOrder.CustomerOrderId, customerOrder.BillAmount, customer.MobileNo, customerOrder.OrderDate, customerOrder.PaidAmount))
                        .OrderByDescending(order => order.OrderDate);
            return query.ToList();
        }
    }
}
