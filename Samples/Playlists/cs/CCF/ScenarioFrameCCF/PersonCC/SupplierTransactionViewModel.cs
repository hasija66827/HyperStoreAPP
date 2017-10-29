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
    public sealed class SupplierTransactionViewModel : TSupplierTransaction
    {
        public string FormattedTransactionDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this.TransactionDate);
            }
        }
        public decimal SignedTransactionAmount
        {
            get
            {
                if (IsCredit == false)
                    return -TransactionAmount;
                return TransactionAmount;
            }
        }

        public SupplierTransactionViewModel(TSupplierTransaction parent)
        {
            foreach (PropertyInfo prop in typeof(TSupplierTransaction).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public sealed class SupplierTransactionCollection
    {
        public List<SupplierTransactionViewModel> Transactions { get; set; }
        public string PayMoneyToSupplier { get { return "Money to " + this.SupplierName; } }
        public string SupplierName { get; set; }        
        public SupplierTransactionCollection() { }
    }
}
