using Models;
using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public sealed class CustomerNewTransactionViewModel : ValidatableBindableBase
    {
        public TCustomer Customer { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [Range(0, float.MaxValue, ErrorMessage = "Try receiving amount in range(0, 10000000).")]
        public string ReceivingAmount { get; set; }

        public bool? IsCashBackTransaction { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(16, ErrorMessage = "Try description with atmost 16 charecters.")]
        public string Description { get; set; }
        public decimal? UpdatedWalletBalance
        {
            get
            {
                return this.Customer.WalletBalance + Utility.TryToConvertToDecimal(this.ReceivingAmount);
            }
        }
        public string ProceedToReceive { get { return "Proceed To Receive " + Utility.ConvertToRupee(ReceivingAmount); } }
        public CustomerNewTransactionViewModel() { }
    }
}
