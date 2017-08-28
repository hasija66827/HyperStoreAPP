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
    public sealed class SupplierTransactionViewModel : TTransaction
    {
        public string FormattedTransactionDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this.TransactionDate);
            }
        }
        public decimal UpdatedWalletSnapshot
        {
            get
            {
                if (IsCredit == false)
                    return this.WalletSnapshot - this.TransactionAmount;
                else return this.WalletSnapshot + this.TransactionAmount;
            }
        }
        public decimal SignedTransactionAmount {
            get {
                if (IsCredit == false)
                    return -TransactionAmount;
                return TransactionAmount;
            } }

        public SupplierTransactionViewModel(TTransaction parent)
        {
            foreach (PropertyInfo prop in typeof(TTransaction).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public sealed class SupplierTransactionCollection
    {
        public List<SupplierTransactionViewModel> Transactions { get; set; }
        public SupplierTransactionCollection() { }
    }
}
