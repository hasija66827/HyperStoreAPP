using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Globalization.DateTimeFormatting;
using SDKTemp.Data;
using Models;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SDKTemplate
{
    public class CustomerOrderViewModel : TCustomerOrder
    {
        public string FormattedPaidBillAmount
        {
            get { return Utility.ConvertToRupee(-this.UsingWalletAmount); }
        }

        public string FormattedOrderDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this.OrderDate);
            }
        }

        public List<CustomerOrderProductViewModel> OrderDetails { get; set; }

        private CustomerBillingSummaryViewModelBase _billingSummary;
        public CustomerBillingSummaryViewModelBase BillingSummary
        {
            get
            {
                if (this._billingSummary == null)
                    this._billingSummary = new CustomerBillingSummaryViewModelBase()
                    {
                        CartAmount = this.CartAmount,
                        DiscountAmount = this.DiscountAmount,
                        Tax = this.Tax,
                        PayAmount = this.PayAmount,
                        TotalItems = this.TotalItems,
                        TotalQuantity = this.TotalQuantity,
                    };
                return this._billingSummary;
            }
        }

        public CustomerOrderViewModel(TCustomerOrder parent)
        {
            this.OrderDetails = new List<CustomerOrderProductViewModel>();
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public sealed class CustomerOrderProductViewModel : TCustomerOrderProduct
    {
        public decimal DiscountAmountSnapShot { get { return this.MRPSnapShot * this.DiscountPerSnapShot / 100; } }
        public decimal TGSTPerSnapShot { get { return this.CGSTPerSnapShot + this.SGSTPerSnapshot; } }
        public decimal TotalGSTAmountSnapShot
        {
            get
            {
                return this.ValueIncTaxSnapShot * TGSTPerSnapShot / (100 + TGSTPerSnapShot);
            }
        }
        public CustomerOrderProductViewModel(TCustomerOrderProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
