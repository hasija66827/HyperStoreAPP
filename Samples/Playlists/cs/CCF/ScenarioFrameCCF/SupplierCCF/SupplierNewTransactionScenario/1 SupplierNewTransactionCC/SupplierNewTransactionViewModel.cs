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
    public class SupplierNewTransactionViewModel: ValidatableBindableBase
    {
        public TSupplier Supplier { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [Range(0, float.MaxValue, ErrorMessage = "Try paying amount in range(0, 10000000).")]
        public string PayingAmount { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(16, ErrorMessage = "Try description with atmost 16 charecters.")]
        public string Description { get; set; }

        public decimal? UpdatedWalletBalance
        {
            get
            {
                return this.Supplier.WalletBalance - Utility.TryToConvertToDecimal(this.PayingAmount);
            }
        }
        public string ProceedToPay { get { return "Proceed To Pay " + Utility.ConvertToRupee(PayingAmount); } }
        public SupplierNewTransactionViewModel() { }
    }
}
