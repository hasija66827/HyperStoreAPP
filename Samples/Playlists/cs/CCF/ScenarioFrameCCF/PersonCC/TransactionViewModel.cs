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
    public sealed class TransactionViewModel : Transaction
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

        public TransactionViewModel(Transaction parent)
        {
            foreach (PropertyInfo prop in typeof(Transaction).GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
        }
    }

    public sealed class TransactionCollection
    {
        public List<TransactionViewModel> Transactions { get; set; }
        public string MoneyToFromPerson { get { return "Money to / from " + this.PersonName; } }
        public string PersonName { get; set; }        
        public TransactionCollection() { }
    }
}
