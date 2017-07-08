using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class WholeSellerOrderDataSource
    {
        private static List<WholeSellerOrderViewModel> _Orders;
        public static List<WholeSellerOrderViewModel> Orders { get { return _Orders; } }

        public WholeSellerOrderDataSource()
        {
        }

        //Retrieves all the wholeSaler orders.
        public static void RetrieveOrdersAsync()
        {
            List<DatabaseModel.WholeSellerOrder> _wholeSellerOrders;
            List<DatabaseModel.WholeSeller> _wholeSellers;
            using (var db = new DatabaseModel.RetailerContext())
            {
                _wholeSellerOrders = db.WholeSellersOrders.ToList();
                _wholeSellers = db.WholeSellers.ToList();
            }
            var query = _wholeSellers
                        .Join(_wholeSellerOrders,
                                wholeSeller => wholeSeller.WholeSellerId,
                                wholeSellerOrder => wholeSellerOrder.WholeSellerId,
                                (wholeseller, wholeSellerOrder) => new WholeSellerOrderViewModel(wholeSellerOrder.WholeSellerOrderId,
                                                                                wholeSellerOrder.OrderDate,
                                                                                wholeSellerOrder.DueDate,
                                                                                wholeSellerOrder.BillAmount,
                                                                                wholeSellerOrder.PaidAmount,
                                                                                wholeseller.WholeSellerId
                                                                                ))
                        .OrderByDescending(order => order.OrderDate);
            _Orders = query.ToList();
        }

        /// <summary>
        /// Return the ordered filtered by filter criteria and the wholesellerId.
        /// </summary>
        /// <param name="filterWholeSalerOrderCriteria"></param>
        /// <param name="wholesellerId"></param>
        /// <returns></returns>
        public static List<WholeSellerOrderViewModel> GetFilteredOrder(FilterWholeSalerOrderCriteria filterWholeSalerOrderCriteria, Guid? wholesellerId)
        {
            List<WholeSellerOrderViewModel> result = new List<WholeSellerOrderViewModel>();
            if (wholesellerId == null)
                result = WholeSellerOrderDataSource.Orders;
            else
                result = WholeSellerOrderDataSource.Orders.Where(o => o.WholeSeller.WholeSellerId == wholesellerId).ToList();
            if (filterWholeSalerOrderCriteria == null)
                return result;
            else
            {
                var ret = result
                    .Where(r => r.DueDate.Date <= filterWholeSalerOrderCriteria.DueDate.Date
                              && r.OrderDate.Date >= filterWholeSalerOrderCriteria.StartDate
                              && r.OrderDate.Date <= filterWholeSalerOrderCriteria.EndDate);
                if (filterWholeSalerOrderCriteria.IncludePartiallyPaidOrdersOnly == true)
                {
                    ret = ret.Where(r => r.PaidAmount < r.BillAmount);
                }
                return ret.ToList();
            }
        }

        public static bool PlaceOrder(WholeSellerPurchaseNavigationParameter pageNavigationParameter)
        {
            //TODO: See how can you make whole transaction atomic.
            var productViewModelList = pageNavigationParameter.productViewModelList;
            var wholeSellerViewModel = pageNavigationParameter.WholeSellerViewModel;

            var db = new DatabaseModel.RetailerContext();
            var updatedWholeSellerWalletBalance = UpdateWalletBalanceOfWholeSeller(db, wholeSellerViewModel,
            pageNavigationParameter.WholeSellerPurchaseCheckoutViewModel.RemainingAmount);

            UpdateProductStock(db, productViewModelList);
            var wholeSellerOrderId = AddIntoWholeSellerOrder(db, pageNavigationParameter);
            AddIntoWholeSellerOrderProduct(db, productViewModelList, wholeSellerOrderId);
            return true;
        }

        //Step 1:
        private static float UpdateWalletBalanceOfWholeSeller(DatabaseModel.RetailerContext db, WholeSellerViewModel wholeSellerViewModel,
          float walletBalanceToBeAdded)
        {
            var wholeSeller = (DatabaseModel.WholeSeller)wholeSellerViewModel;
            var entityEntry = db.Attach(wholeSeller);
            wholeSeller.WalletBalance += walletBalanceToBeAdded;
            var memberEntry = entityEntry.Member(nameof(DatabaseModel.WholeSeller.WalletBalance));
            memberEntry.IsModified = true;
            db.SaveChanges();
            return wholeSeller.WalletBalance;
        }

        // Step 2:
        private static bool UpdateProductStock(DatabaseModel.RetailerContext db, List<WholeSellerProductListVieModel> purchasedProducts)
        {
            //#perf: You can query whole list in where clause.
            foreach (var purchasedProduct in purchasedProducts)
            {
                //TODO: check where clouse whether threough id or bar code.
                var products = db.Products.Where(p => p.BarCode == purchasedProduct.BarCode).ToList();
                var product = products.FirstOrDefault();
                if (product == null)
                    return false;
                product.TotalQuantity += purchasedProduct.QuantityPurchased;
                db.Update(product);
            }
            db.SaveChanges();
            return true;
        }

        // Step 3:
        private static Guid AddIntoWholeSellerOrder(DatabaseModel.RetailerContext db, WholeSellerPurchaseNavigationParameter navigationParameter)
        {
            var wholeSellerOrder = new DatabaseModel.WholeSellerOrder(navigationParameter);
            // Creating Entity Record in customerOrder.
            db.WholeSellersOrders.Add(wholeSellerOrder);
            db.SaveChanges();
            return wholeSellerOrder.WholeSellerOrderId;
        }

        // Step4:
        private static void AddIntoWholeSellerOrderProduct(DatabaseModel.RetailerContext db, List<WholeSellerProductListVieModel> purchasedProducts, Guid wholeSellerOrderId)
        {
            foreach (var purchasedProduct in purchasedProducts)
            {
                // Adding each product purchased in the order into the Entity WholeSellerOrderProduct.
                var wholeSellerOrderProduct = new DatabaseModel.WholeSellerOrderProduct(
                    purchasedProduct.ProductId,
                    wholeSellerOrderId,
                    purchasedProduct.QuantityPurchased,
                    purchasedProduct.PurchasePrice
                    );
                db.WholeSellersOrderProducts.Add(wholeSellerOrderProduct);
            }
            // Saving the order.
            db.SaveChanges();
        }
    }
}
