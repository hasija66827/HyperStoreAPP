using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
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
            get { return Utility.ConvertToRupee(this.PayingAmount) + "/" + Utility.ConvertToRupee(this.DiscountedAmount); }
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

        public CustomerOrderViewModel(TCustomerOrder parent)
        {
            this.OrderDetails = new List<CustomerOrderProductViewModel>();
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public sealed class CustomerOrderProductViewModel : TCustomerOrderProduct
    {
        public CustomerOrderProductViewModel(TCustomerOrderProduct parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }
}
