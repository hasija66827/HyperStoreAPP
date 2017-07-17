using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate.View_Models;
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
            var remainingAmount = pageNavigationParameter.WholeSellerPurchaseCheckoutViewModel.RemainingAmount;
            var db = new DatabaseModel.RetailerContext();

            var transaction = new TransactionViewModel(-remainingAmount, DateTime.Now, wholeSellerViewModel);
            TransactionDataSource.CreateTransaction(transaction);
            var updatedWholeSellerWalletBalance = WholeSellerDataSource.UpdateWalletBalanceOfWholeSeller(db, wholeSellerViewModel,
            pageNavigationParameter.WholeSellerPurchaseCheckoutViewModel.RemainingAmount);

            ProductDataSource.UpdateProductStockByWholeSeller(db, productViewModelList);
            var wholeSellerOrderId = AddIntoWholeSellerOrder(db, pageNavigationParameter);
            WholeSellerOrderProductDataSource.AddIntoWholeSellerOrderProduct(db, productViewModelList, wholeSellerOrderId);
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

    
    }
}
