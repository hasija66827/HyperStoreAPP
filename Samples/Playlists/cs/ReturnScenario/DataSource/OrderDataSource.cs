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
        // Step 1:
        private static bool UpdateProductStock(DatabaseModel.RetailerContext db, BillingViewModel billingViewModel)
        {
            //#perf: You can query whole list in where clause.
            foreach (var productViewModel in billingViewModel.Products)
            {
                var products = db.Products.Where(p => p.ProductId == productViewModel.ProductId).ToList();
                var product = products.FirstOrDefault();
                if (product == null)
                    return false;
                product.TotalQuantity -= productViewModel.QuantityPurchased;
                db.Update(product);
            }
            db.SaveChanges();
            return true;
        }
        // Step 2:
        private static void UpdateWalletBalanceOfCustomer(DatabaseModel.RetailerContext db, CustomerViewModel customerViewModel,
            float walletBalanceToBeDeducted, float walletBalanceToBeAdded)
        {
            var customer = (DatabaseModel.Customer)customerViewModel;
            var entityEntry = db.Attach(customer);
            customer.WalletBalance -= walletBalanceToBeDeducted;
            customer.WalletBalance += walletBalanceToBeAdded;
            var memberEntry = entityEntry.Member(nameof(DatabaseModel.Customer.WalletBalance));
            memberEntry.IsModified = true;
            db.SaveChanges();
        }
        // Step3:
        private static Guid AddIntoCustomerOrder(DatabaseModel.RetailerContext db, BillingViewModel billingViewModel, CustomerViewModel customerViewModel)
        {
            var customerOrder = new DatabaseModel.CustomerOrder(customerViewModel.CustomerId,
                                     billingViewModel.TotalBillAmount,
                                     billingViewModel.DiscountedBillAmount,
                                    customerViewModel.WalletBalance);
            // Creating Entity Record in customerOrder.
            db.CustomerOrders.Add(customerOrder);

            db.SaveChanges();
            return customerOrder.CustomerOrderId;
        }
        // Step4:
        private static void AddIntoCustomerOrderProduct(DatabaseModel.RetailerContext db, BillingViewModel billingViewModel, Guid customerOrderId)
        {
            foreach (var productViewModel in billingViewModel.Products)
            {
                // Adding each product purchased in the order into the Entity CustomerOrderProduct.
                var customerOrderProduct = new DatabaseModel.CustomerOrderProduct(customerOrderId,
                    productViewModel.ProductId,
                    productViewModel.DiscountPer,
                    productViewModel.DisplayPrice,
                    productViewModel.QuantityPurchased);
                db.CustomerOrderProducts.Add(customerOrderProduct);
                // TODO: Update the product entity with new total quantity.
            }
            // Saving the order.
            db.SaveChanges();
        }
        public static void PlaceOrder(PageNavigationParameter pageNavigationParameter)
        {
            //TODO: without doing additional saving, foreign key constraints failed. See how can you make whole transaction atomic.
            BillingViewModel billingViewModel = pageNavigationParameter.BillingViewModel;
            CustomerViewModel customerViewModel = pageNavigationParameter.CustomerViewModel;
            var db = new DatabaseModel.RetailerContext();


            UpdateProductStock(db, billingViewModel);
            if (pageNavigationParameter.UseWallet == false && pageNavigationParameter.WalletBalanceToBeDeducted != 0)
                throw new Exception("assertion failed: wallet amount should not be deducted, if it is not checked, although money can be added into the wallet with uncheck checkbox");    
            UpdateWalletBalanceOfCustomer(db, customerViewModel,
                pageNavigationParameter.WalletBalanceToBeDeducted,
                pageNavigationParameter.Change);

            var customerOrderId = AddIntoCustomerOrder(db, billingViewModel, customerViewModel);
            AddIntoCustomerOrderProduct(db, billingViewModel, customerOrderId);
        }
    }
}
