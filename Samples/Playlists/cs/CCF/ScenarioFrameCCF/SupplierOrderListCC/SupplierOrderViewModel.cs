using Models;
using SDKTemplate.Data_Source;
using SDKTemplate.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;

namespace SDKTemplate
{
    /// <summary>
    /// This class represents the Master Detail View Model
    /// Detail view show detail of the order and the trnasactions which completed the payment of the order.
    /// </summary>
    public class SupplierOrderViewModel : TSupplierOrder
    {
        //TODO: Remove
        public List<SettledOrderOfTransactionViewModel> _transactions;
        public List<SettledOrderOfTransactionViewModel> Transactions
        {
            get
            {
                if (this._transactions.Count == 0)
                    this._transactions = WholeSellerOrderTransactionDataSource.RetrieveWholeSellerOrderTransactions(null, this.SupplierOrderId);
                return this._transactions;
            }
        }

        public string FormattedOrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this.OrderDate);
            }
        }

        public string FormattedDueDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this.DueDate);
            }
        }

        public string FormattedPaidBillAmount
        {
            get { return Utility.ConvertToRupee(this.PaidAmount) + "/" + Utility.ConvertToRupee(this.BillAmount); }
        }

        public List<SupplierOrderProductViewModel> OrderDetails { get; set; }

        public SupplierOrderViewModel(TSupplierOrder parent)
        {
            //Remove
            this._transactions = new List<SettledOrderOfTransactionViewModel>();
            this.OrderDetails = new List<SupplierOrderProductViewModel>();
            foreach (PropertyInfo prop in typeof(TSupplierOrder).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public sealed class SupplierOrderProductViewModel : TSupplierOrderProduct
    {
        public decimal NetValue { get { return this.PurchasePrice * this.QuantityPurchased; } }
        public SupplierOrderProductViewModel(TSupplierOrderProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
