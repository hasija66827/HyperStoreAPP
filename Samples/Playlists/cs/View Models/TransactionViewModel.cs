using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.View_Models
{
    public class TransactionViewModel
    {
        private Guid _transactionId;
        public Guid TransactionId { get { return this._transactionId; } }

        private float _amount;
        public float Amount { get { return this._amount; } }

        private DateTime _transactionDate;
        public DateTime TransactionDate { get { return this._transactionDate; } }

        private float _walletSnapShot;
        public float WalletSnapshot { get { return this._walletSnapShot; } }

        private Guid? _wholeSellerId;
        public Guid? WholeSellerId { get { return this._wholeSellerId; } }

        public TransactionViewModel(float amount, DateTime transactionDate,
                   WholeSellerViewModel wholeSellerViewModel)
        { 
            this._transactionId = Guid.NewGuid();
            this._amount = amount;
            this._transactionDate = transactionDate;
            this._walletSnapShot = wholeSellerViewModel.WalletBalance;
            this._wholeSellerId = wholeSellerViewModel.WholeSellerId;
        }
    }
}
