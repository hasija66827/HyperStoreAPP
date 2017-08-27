using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKTemplate.View_Models;
using SDKTemplate.Data_Source;
using Models;
using SDKTemplate.DTO;

namespace SDKTemplate
{
    public class SupplierOrderDataSource
    {
        private static List<WholeSellerOrderViewModel> _Orders;
        public static List<WholeSellerOrderViewModel> Orders { get { return _Orders; } }


        #region Create
        public static async Task<decimal> CreateSupplierOrderAsync(SupplierOrderDTO supplierOrderDTO)
        {
            string actionURI = "supplierorders";
            return await Utility.CreateAsync<decimal>(actionURI, supplierOrderDTO);
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
                                (wholeseller, wholeSellerOrder) => new WholeSellerOrderViewModel(wholeSellerOrder, wholeseller))
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
                result = SupplierOrderDataSource.Orders;
            else
                result = SupplierOrderDataSource.Orders.Where(o => o.WholeSeller.SupplierId == wholesellerId).ToList();
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
    }
}