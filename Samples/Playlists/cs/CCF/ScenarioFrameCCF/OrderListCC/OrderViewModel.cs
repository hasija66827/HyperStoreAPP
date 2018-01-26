using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class OrderViewModel : Order
    {
        public decimal PayedAmountByTx { get { return this.SettledPayedAmount - this.PayedAmount; } }
        public decimal RemainingAmount { get { return this.BillAmount - this.SettledPayedAmount; } }
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
            get { return Math.Round(this.SettledPayedAmount / this.BillAmount * 100) + "%"; }
        }

        public string FormattedRemainingAmount
        {
            get { return Utility.ConvertToRupee(this.RemainingAmount); }
        }

        public List<KeyValuePair<string, decimal>> Paid_BillAmount
        {
            get
            {
                var x = new List<KeyValuePair<string, decimal>>();
                x.Add(new KeyValuePair<string, decimal>("Settled Amunt", SettledPayedAmount));
                x.Add(new KeyValuePair<string, decimal>("Remaining Amount", BillAmount - SettledPayedAmount));
                return x;
            }
        }

        private PersonViewModel _personViewModel;
        public PersonViewModel PersonViewModel {
            get { return this._personViewModel; }
        }

        public string Items_Quantity { get { return this.TotalItems + "/" + this.TotalQuantity; } }

        public List<OrderProductViewModel> OrderDetails { get; set; }


        public OrderViewModel(Order parent)
        {
            this.OrderDetails = new List<OrderProductViewModel>();
            this._personViewModel = new PersonViewModel(parent.Person);
            foreach (PropertyInfo prop in typeof(Order).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public sealed class OrderProductViewModel : OrderProduct
    {
        public decimal NetValue { get { return this.PurchasePrice * this.QuantityPurchased; } }
        public OrderProductViewModel(OrderProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
