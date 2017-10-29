using Models;
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
    public class OrderViewModel : TSupplierOrder
    {
        public decimal PayedAmountByTx { get { return this.SettledPayedAmount - this.PayedAmount; } }
        public decimal RemainingAmount{ get { return this.BillAmount - this.SettledPayedAmount; } }
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
            get { return Utility.ConvertToRupee(this.SettledPayedAmount) + "/" + Utility.ConvertToRupee(this.BillAmount); }
        }

        public string Items_Quantity { get { return this.TotalItems + "/" + this.TotalQuantity; } }

        public List<OrderProductViewModel> OrderDetails { get; set; }


        public OrderViewModel(TSupplierOrder parent)
        {
            this.OrderDetails = new List<OrderProductViewModel>();
            foreach (PropertyInfo prop in typeof(TSupplierOrder).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public sealed class OrderProductViewModel : TSupplierOrderProduct
    {
        public decimal NetValue { get { return this.PurchasePrice * this.QuantityPurchased; } }
        public OrderProductViewModel(TSupplierOrderProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
