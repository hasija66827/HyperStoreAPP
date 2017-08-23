using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization.DateTimeFormatting;

namespace SDKTemplate.View_Models
{
    public class TransactionViewModel
    {
        private Guid _transactionId;
        public Guid TransactionId { get { return this._transactionId; } }

        private string _transactionNo;
        public string TransactionNo { get { return this._transactionNo; } }

        //Amount credited into the account of the wholeseller.
        private decimal _creditAmount;
        public decimal CreditAmount { get { return this._creditAmount; } }

        private DateTime _transactionDate;
        public DateTime TransactionDate { get { return this._transactionDate; } }
        public string FormattedTransactionDate
        {
            get
            {
                var formatter = new DateTimeFormatter("day month");
                return formatter.Format(this._transactionDate);
            }
        }

        private decimal _walletSnapShot;
        public decimal WalletSnapshot { get { return this._walletSnapShot; } }

        public decimal UpdatedWalletSnapshot { get { return this._walletSnapShot - this._creditAmount; } }

        private Guid? _wholeSellerId;
        public Guid? WholeSellerId { get { return this._wholeSellerId; } }

        public TransactionViewModel(decimal creditAmount, DateTime transactionDate,
                   WholeSellerViewModel wholeSellerViewModel)
        { 
            this._transactionId = Guid.NewGuid();
            this._transactionNo = Utility.GenerateWholeSellerTransactionNo();
            this._creditAmount = creditAmount;
            this._transactionDate = transactionDate;
            this._walletSnapShot = wholeSellerViewModel.WalletBalance;
            this._wholeSellerId = wholeSellerViewModel.SupplierId;
        }

        public TransactionViewModel(DatabaseModel.Transaction transaction)
        {
            this._transactionId = transaction.TransactionId;
            this._transactionNo = transaction.TransactionNo;
            this._transactionDate = transaction.TransactionDate;
            this._walletSnapShot = transaction.WalletSnapshot;
            this._creditAmount = transaction.CreditAmount;
            this._wholeSellerId = transaction.WholeSellerId;
        }
    }

    public class TransactionHistoryOfWholeSellerCollection {
        public List<TransactionViewModel> Transactions { get; set; }
        public TransactionHistoryOfWholeSellerCollection() { }
    }

    public class TransactionHistoryOfWholeSellerOrderCollection {

        public List<TransactionViewModel> Transactions { get; set; }
        public TransactionHistoryOfWholeSellerOrderCollection() { }
    }
}
