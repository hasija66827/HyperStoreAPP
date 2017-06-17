using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate;
using SDKTemp.ViewModel;
namespace SDKTemp.Data
{
    public enum PaymentMode
    {
        payNow = 1,
        payLater = 2
    }
    public class CustomerOrderDataSource
    {
        private static List<OrderViewModel> _Orders;
        public static List<OrderViewModel> Orders { get { return _Orders; } }
        public CustomerOrderDataSource()
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
                                (customer, customerOrder) => new OrderViewModel(customerOrder.CustomerOrderId,
                                                                                customerOrder.TotalBillAmount,
                                                                                customer.MobileNo,
                                                                                customerOrder.OrderDate,
                                                                                customerOrder.DiscountedAmount))
                        .OrderByDescending(order => order.OrderDate);
            _Orders = query.ToList();
        }

        public static List<OrderViewModel> GetOrders(CustomerViewModel selectedCustomer, FilterOrderViewModel selectedDateRange)
        {
            if (selectedDateRange == null)
                throw new Exception("A Date Range cannot be null");

            if (selectedCustomer == null && selectedDateRange != null)
            {
                var orderByDateRange = _Orders.
                    Where(order => order.OrderDate.Date >= selectedDateRange.StartDate.Date &&
                                   order.OrderDate.Date <= selectedDateRange.EndDate.Date);
                return orderByDateRange.ToList();
            }
            else
            {
                var orderByMobNoDateRange = _Orders.Where(order =>
                order.CustomerMobileNo == selectedCustomer.MobileNo &&
                order.OrderDate.Date >= selectedDateRange.StartDate.Date &&
                order.OrderDate.Date <= selectedDateRange.EndDate.Date
                );
                return orderByMobNoDateRange.ToList();
            }
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

        /// <summary>
        /// Returns the updated wallet balance of the customer
        /// </summary>
        /// <param name="pageNavigationParameter"></param>
        /// <returns></returns>
        public static float PlaceOrder(PageNavigationParameter pageNavigationParameter,
            SDKTemp.Data.PaymentMode paymentMode)
        {
            if (pageNavigationParameter.UseWallet == false && pageNavigationParameter.WalletBalanceToBeDeducted != 0)
                throw new Exception("assertion failed: wallet amount should not be deducted, if it is not checked, although money can be added into the wallet with uncheck checkbox");

            //TODO: See how can you make whole transaction atomic.
            ProductListViewModel billingViewModel = pageNavigationParameter.ProductListViewModel;
            CustomerViewModel customerViewModel = pageNavigationParameter.CustomerViewModel;
            var db = new DatabaseModel.RetailerContext();
            float updatedCustomerWalletBalance = 0;
            if (paymentMode.Equals(PaymentMode.payNow))
            {
                updatedCustomerWalletBalance = CustomerDataSource.UpdateWalletBalanceOfCustomer(db, customerViewModel,
                pageNavigationParameter.WalletBalanceToBeDeducted,
                pageNavigationParameter.WalletAmountToBeAddedNow);
            }
            else if (paymentMode.Equals(PaymentMode.payLater))
            {
                updatedCustomerWalletBalance = CustomerDataSource.UpdateWalletBalanceOfCustomer(db, customerViewModel,
                                pageNavigationParameter.WalletAmountToBePaidLater,
                                0);
            }
            else
            { throw new NotImplementedException(); }

            ProductDataSource.UpdateProductStock(db, billingViewModel);
            var customerOrderId = AddIntoCustomerOrder(db, pageNavigationParameter);
            AddIntoCustomerOrderProduct(db, billingViewModel, customerOrderId);
            return updatedCustomerWalletBalance;
        }


        // Step 3:
        private static Guid AddIntoCustomerOrder(DatabaseModel.RetailerContext db, PageNavigationParameter pageNavigationParameter)
        {
            var customerOrder = new DatabaseModel.CustomerOrder(pageNavigationParameter);
            // Creating Entity Record in customerOrder.
            db.CustomerOrders.Add(customerOrder);
            db.SaveChanges();
            return customerOrder.CustomerOrderId;
        }

        // Step4:
        private static void AddIntoCustomerOrderProduct(DatabaseModel.RetailerContext db, ProductListViewModel billingViewModel, Guid customerOrderId)
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
            }
            // Saving the order.
            db.SaveChanges();
        }

    }
}
