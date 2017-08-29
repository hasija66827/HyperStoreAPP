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
    public sealed class CustomerTransactionViewModel : TCustomerTransaction
    {
        public string FormattedTransactionDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this.TransactionDate);
            }
        }
        public CustomerTransactionViewModel(TCustomerTransaction parent)
        {
            foreach (PropertyInfo prop in typeof(TCustomerTransaction).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public sealed class CustomerTransactionCollection
    {
        public List<CustomerTransactionViewModel> Transactions { get; set; }
        public CustomerTransactionCollection() { }
    }
}
