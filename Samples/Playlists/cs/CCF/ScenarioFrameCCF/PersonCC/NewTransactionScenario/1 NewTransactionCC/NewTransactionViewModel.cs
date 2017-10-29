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
    public class NewTransactionViewModel : ValidatableBindableBase
    {
        public Person Supplier { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [Range(0, float.MaxValue, ErrorMessage = "Try amount in range(0, 10000000).")]
        [LessThanProperty(nameof(AbsWalletBalance))]
        public string Amount { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(16, ErrorMessage = "Try description with atmost 16 charecters.")]
        public string Description { get; set; }

        public decimal AbsWalletBalance{ get { return Math.Abs(this.Supplier.WalletBalance); } }

        public decimal? UpdatedWalletBalance
        {
            get
            {
                if (this.Supplier.EntityType == DTO.EntityType.Supplier)
                    return this.Supplier.WalletBalance - Utility.TryToConvertToDecimal(this.Amount);
                else
                    return this.Supplier.WalletBalance + Utility.TryToConvertToDecimal(this.Amount);
            }
        }
        public string ProceedToPay { get { return "Proceed" + Utility.ConvertToRupee(Amount); } }
        public NewTransactionViewModel() { }
    }
}
