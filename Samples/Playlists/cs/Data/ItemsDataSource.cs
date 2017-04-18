using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate;
using DatabaseModel;
namespace MasterDetailApp.Data
{
    public class ItemsDataSource
    {
        private static List<Item> _items = new List<Item>()
        {
            
        };

        public static IList<Item> GetAllItems()
        {
            return _items;
        }

        public static IList<Item> GetAllOrders()
        {
            List<CustomerOrder> _customerOrders = new List<CustomerOrder>();
            List<Customer> _customers = new List<Customer>();
            using (var db = new RetailerContext())
            {
                _customerOrders = db.CustomerOrders.ToList();
                _customers = db.Customers.ToList();
            }
            var query = _customers.Join(_customerOrders,
                                customer => customer.CustomerId,
                                customerOrder => customerOrder.CustomerId,
                                (customer, customerOrder) => new Item(customer.MobileNo, customerOrder.BillAmount, customerOrder.OrderDate));


            return query.ToList();
        }
        public static Item GetItemById(int id)
        {
            return _items[id];
        }
    }
}
