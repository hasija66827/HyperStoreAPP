﻿using System;
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
        private static List<Order> _allOrders;
        public static List<Order> AllOrders { get { return _allOrders; } }
        // Make it as an constructor
        public static void RetrieveAllOrdersAsync()
        {
            List<CustomerOrder> _customerOrders = new List<CustomerOrder>();
            List<DatabaseModel.Customer> _customers = new List<DatabaseModel.Customer>();
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
            _allOrders = query.ToList();
        }
        public static List<Order> RetrieveOrdersByMobileNumber(string MobileNumber)
        {
            var orderByMobileNumber = _allOrders.Where(order => order.CustomerMobileNo == MobileNumber);
            return orderByMobileNumber.ToList();
        }
    }
}
