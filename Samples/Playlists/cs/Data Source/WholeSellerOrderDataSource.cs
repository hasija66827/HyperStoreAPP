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

        #region Create
        private static Guid CreateNewWholeSellerOrder(DatabaseModel.RetailerContext db, WholeSellerCheckoutNavigationParameter navigationParameter)
        {
            var wholeSellerOrder = new DatabaseModel.WholeSellerOrder(navigationParameter);
            // Creating Entity Record in customerOrder.
            db.WholeSellersOrders.Add(wholeSellerOrder);
            return wholeSellerOrder.WholeSellerOrderId;
        }
        #endregion

        #region Read
        /// <summary>
        /// Retrieves all the wholeSaler orders.
        /// </summary>
        public static void RetrieveOrders()
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
                                (wholeseller, wholeSellerOrder) => new WholeSellerOrderViewModel(wholeSellerOrder))
                        .OrderByDescending(order => order.OrderDate);
            _Orders = query.ToList();
        }

        /// <summary>
        /// Return the orders filtered by filter criteria and the wholesellerId.
        /// </summary>
        /// <param name="filterWholeSalerOrderCriteria"></param>
        /// <param name="wholesellerId"></param>
        /// <returns></returns>
        public static List<WholeSellerOrderViewModel> GetFilteredOrders(FilterWholeSalerOrderCriteria filterWholeSalerOrderCriteria, Guid? wholesellerId)
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
        #endregion

        #region Create Update
        /// <summary>
        /// It update following entities
        /// a)Transaction: Create new Transaction.
        /// b)WholeSellerOrder: Create new WholeSellerOrder.
        /// c)WholeSellerOrderTransaction: Create new WholeSellerOrderTransaction on completion of a) & b).
        /// d)Wholeseller: Update the wallet balance.
        /// e)WholeSellerOrderProductDataSource: Create wop for all the products purchased from the wholeseller.
        /// f)ProductDataSource: Update the stock of the product.
        /// </summary>
        /// <param name="pageNavigationParameter"></param>
        /// <returns></returns>
        public static bool PlaceOrder(WholeSellerCheckoutNavigationParameter pageNavigationParameter)
        {
            var productList = pageNavigationParameter.productViewModelList;
            var wholeSeller = pageNavigationParameter.WholeSellerViewModel;
            var payingAmount = pageNavigationParameter.WholeSellerCheckoutViewModel.PaidAmount;
            var remainingAmount = pageNavigationParameter.WholeSellerCheckoutViewModel.RemainingAmount;
            var IsOrderSettleUp = remainingAmount == 0 ? true : false;
            var transaction = new TransactionViewModel(-remainingAmount, DateTime.Now, wholeSeller);
            var db = new DatabaseModel.RetailerContext();

            TransactionDataSource.CreateTransaction(transaction, db);
            var wholeSellerOrderId = CreateNewWholeSellerOrder(db, pageNavigationParameter);

            WholeSellerOrderTransactionDataSource.CreateNewWholeSellerOrderTransaction(transaction.TransactionId, wholeSellerOrderId, -remainingAmount, IsOrderSettleUp, db);
            var updatedWholeSellerWalletBalance = WholeSellerDataSource.UpdateWalletBalanceOfWholeSeller(db, wholeSeller,
                                                                                                            pageNavigationParameter.WholeSellerCheckoutViewModel.RemainingAmount);

            WholeSellerOrderProductDataSource.CreateWholeSellerOrderProduct(db, productList, wholeSellerOrderId);
            ProductDataSource.UpdateProductStockByWholeSeller(db, productList);
            return true;
        }

        /// <summary>
        /// Settle up the orders of the wholeseller with the credit amount.
        /// It starts with the oldest order and settle up the order till the credit amount is reduced to zero 
        /// or all the orders are completly paid.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="wholeSeller"></param>
        /// <returns>returns the credit amount remaining after completing the payements of order.</returns>
        public static float SettleUpOrders(TransactionViewModel transaction, WholeSellerViewModel wholeSeller, DatabaseModel.RetailerContext db = null)
        {
            if (db == null)
                db = new DatabaseModel.RetailerContext();
            var partiallyPaidOrders = db.WholeSellersOrders.Where(wo => wo.WholeSellerId == wholeSeller.WholeSellerId &&
                                                                        wo.BillAmount - wo.PaidAmount > 0)
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
                WholeSellerOrderTransactionDataSource.CreateNewWholeSellerOrderTransaction(transaction.TransactionId, partiallyPaidOrder.WholeSellerOrderId, payingAmountForOrder, IsOrderSettleUp, db);
                creditAmount -= payingAmountForOrder;
            }
            db.SaveChanges();
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
        #endregion
    }
}