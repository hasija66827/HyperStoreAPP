using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate;
using SDKTemplate.Data_Source;
using Models;
using SDKTemplate.DTO;
using Newtonsoft.Json;

namespace SDKTemp.Data
{
    public enum PaymentMode
    {
        payNow = 1,
        payLater = 2
    }
    public class CustomerOrderDataSource
    {
        public static async Task<List<TCustomerOrder>> RetrieveCustomerOrdersAsync(CustomerOrderFilterCriteriaDTO cofc)
        {
            string actionURI = "customerorders";
            List<TCustomerOrder> customerOrders = await Utility.Retrieve<TCustomerOrder>(actionURI, cofc);
            return customerOrders;
        }

        public static async Task<List<TCustomerOrderProduct>> RetrieveOrderDetailsAsync(Guid customerOrderId)
        {
            string actionURI = "customerorderproducts/" + customerOrderId.ToString();
            List<TCustomerOrderProduct> orderDetails = await Utility.Retrieve<TCustomerOrderProduct>(actionURI, null);
            return orderDetails;
        }

        //#remove
        /// <summary>
        /// Returns the updated wallet balance of the customer
        /// </summary>
        /// <param name="pageNavigationParameter"></param>
        /// <returns></returns>
        public static decimal PlaceOrder(PageNavigationParameter pageNavigationParameter,
            SDKTemp.Data.PaymentMode paymentMode)
        {
            if (pageNavigationParameter.UseWallet == false && pageNavigationParameter.WalletBalanceToBeDeducted != 0)
                throw new Exception("assertion failed: wallet amount should not be deducted, if it is not checked, although money can be added into the wallet with uncheck checkbox");

            //TODO: See how can you make whole transaction atomic.
            var productListTobePurchased = pageNavigationParameter.ProductsToBePurchased;
            var customerViewModel = pageNavigationParameter.CustomerViewModel;
            var db = new DatabaseModel.RetailerContext();
            decimal updatedCustomerWalletBalance = 0;
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

            ProductDataSource.UpdateProductStock(db, productListTobePurchased);
            var customerOrderId = AddIntoCustomerOrder(db, pageNavigationParameter);
            AddIntoCustomerOrderProduct(db, productListTobePurchased, customerOrderId);
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
        private static void AddIntoCustomerOrderProduct(DatabaseModel.RetailerContext db, List<CustomerProductViewModel> productsToBePurchased, Guid customerOrderId)
        {
            foreach (var product in productsToBePurchased)
            {
                // Adding each product purchased in the order into the Entity CustomerOrderProduct.
                var customerOrderProduct = new DatabaseModel.CustomerOrderProduct();
                db.CustomerOrderProducts.Add(customerOrderProduct);
            }
            // Saving the order.
            db.SaveChanges();
        }

    }
}
