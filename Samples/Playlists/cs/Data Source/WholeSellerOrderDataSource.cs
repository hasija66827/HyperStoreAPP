using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate.View_Models;
using SDKTemplate.Data_Source;

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

        public static WholeSellerOrderViewModel RetrieveWholeSellerOrder(Guid? wholeSellerOrderId, DatabaseModel.RetailerContext db)
        {
            if (db == null)
                db = new DatabaseModel.RetailerContext();
            var list=db.WholeSellersOrders.Where(wo => wo.WholeSellerOrderId == wholeSellerOrderId).ToList();
            if (list.Count != 1)
                throw new Exception(String.Format("{0} wholesellerOrderId found in wholesellerorder entity", list.Count));
            var wholeSellerOrderDB=list.ElementAt(0);
            var wholeSellerOrderViewModel=new WholeSellerOrderViewModel(wholeSellerOrderDB);
            return wholeSellerOrderViewModel;
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
            var payingAmount = pageNavigationParameter.WholeSellerPurchaseCheckoutViewModel.PaidAmount;
            var remainingAmount = pageNavigationParameter.WholeSellerPurchaseCheckoutViewModel.RemainingAmount;
            var IsOrderSettleUp = remainingAmount == 0 ? true : false;
            var transaction = new TransactionViewModel(-remainingAmount, DateTime.Now, wholeSellerViewModel);

            var db = new DatabaseModel.RetailerContext();
            // Six entities updated
            TransactionDataSource.CreateTransaction(transaction, db);
            var updatedWholeSellerWalletBalance = WholeSellerDataSource.UpdateWalletBalanceOfWholeSeller(db, wholeSellerViewModel,
                                                                                                            pageNavigationParameter.WholeSellerPurchaseCheckoutViewModel.RemainingAmount);
            var wholeSellerOrderId = CreateWholeSellerOrder(db, pageNavigationParameter);
            WholeSellerOrderTransactionDataSource.CreateWholeSellerOrderTransaction(transaction.TransactionId, wholeSellerOrderId, -remainingAmount, IsOrderSettleUp, db);
            WholeSellerOrderProductDataSource.CreateWholeSellerOrderProduct(db, productViewModelList, wholeSellerOrderId);
            ProductDataSource.UpdateProductStockByWholeSeller(db, productViewModelList);
            return true;
        }

        /// <summary>
        /// settle up the orders of the wholeseller with the credit amount.
        /// It starts with the oldest order and settle up the order till the credit amount is reduced to zero or all the orders are completly paid.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="wholeSeller"></param>
        /// <returns>returns the credit amount remaining after completing the payements of order.</returns>
        public static float SettleUpOrders(TransactionViewModel transaction, WholeSellerViewModel wholeSeller, DatabaseModel.RetailerContext db = null)
        {
            if (db == null)
                db = new DatabaseModel.RetailerContext();
            var partiallyPaidOrders = db.WholeSellersOrders.Where(wo => wo.WholeSellerId == wholeSeller.WholeSellerId
                                                                        && wo.BillAmount - wo.PaidAmount > 0)
                                                            .OrderBy(wo => wo.OrderDate);
            var creditAmount = transaction.CreditAmount;
            foreach (var partiallyPaidOrder in partiallyPaidOrders)
            {
                if (creditAmount <= 0)
                    break;
                var remainingAmount = partiallyPaidOrder.BillAmount - partiallyPaidOrder.PaidAmount;
                if (remainingAmount < 0)
                    throw new Exception(string.Format("remaining amount {0} cannot be less than zero", remainingAmount));
                float payingAmountForOrder = remainingAmount <= creditAmount ? remainingAmount : creditAmount;
                var IsOrderSettleUp = SettleUpOrder(transaction, partiallyPaidOrder, payingAmountForOrder, db);
                WholeSellerOrderTransactionDataSource.CreateWholeSellerOrderTransaction(transaction.TransactionId, partiallyPaidOrder.WholeSellerOrderId, payingAmountForOrder, IsOrderSettleUp, db);
                creditAmount -= payingAmountForOrder;
            }
            db.SaveChanges();
            return creditAmount;
        }

        /// <summary>
        /// Read only transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="wholeSeller"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static float RetrieveSettleUpOrders(TransactionViewModel transaction, WholeSellerViewModel wholeSeller, DatabaseModel.RetailerContext db = null)
        {
            if (db == null)
                db = new DatabaseModel.RetailerContext();
            var partiallyPaidOrders = db.WholeSellersOrders.Where(wo => wo.WholeSellerId == wholeSeller.WholeSellerId
                                                                        && wo.BillAmount - wo.PaidAmount > 0)
                                                            .OrderBy(wo => wo.OrderDate);
            var creditAmount = transaction.CreditAmount;
            foreach (var partiallyPaidOrder in partiallyPaidOrders)
            {
                if (creditAmount < 0)
                    break;
                var remainingAmount = partiallyPaidOrder.BillAmount - partiallyPaidOrder.PaidAmount;
                if (remainingAmount < 0)
                    throw new Exception(string.Format("remaining amount {0} cannot be less than zero", remainingAmount));
                float payingAmountForOrder = remainingAmount <= creditAmount ? remainingAmount : creditAmount;
                creditAmount -= payingAmountForOrder;
            }
            return creditAmount;
        }


        /// <summary>
        /// Used to settle up the order with required credit amount.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="wholeSellerOrder"></param>
        /// <param name="creditAmount"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static bool SettleUpOrder(TransactionViewModel transaction, DatabaseModel.WholeSellerOrder wholeSellerOrder, float creditAmount, DatabaseModel.RetailerContext db)
        {
            var remainingAmount = wholeSellerOrder.BillAmount - wholeSellerOrder.PaidAmount;
            if (creditAmount > remainingAmount)
                throw new Exception(String.Format("credit amount {0} for an order cannot be greater than remaining amount {1} of the order {2}", creditAmount, remainingAmount, wholeSellerOrder.WholeSellerOrderId));
            wholeSellerOrder.PaidAmount += creditAmount;
            db.WholeSellersOrders.Update(wholeSellerOrder);
            if (wholeSellerOrder.PaidAmount == wholeSellerOrder.BillAmount)
                return true;
            return false;
        }


        // Step 3:
        private static Guid CreateWholeSellerOrder(DatabaseModel.RetailerContext db, WholeSellerPurchaseNavigationParameter navigationParameter)
        {
            var wholeSellerOrder = new DatabaseModel.WholeSellerOrder(navigationParameter);
            // Creating Entity Record in customerOrder.
            db.WholeSellersOrders.Add(wholeSellerOrder);
            return wholeSellerOrder.WholeSellerOrderId;
        }

    }
}
