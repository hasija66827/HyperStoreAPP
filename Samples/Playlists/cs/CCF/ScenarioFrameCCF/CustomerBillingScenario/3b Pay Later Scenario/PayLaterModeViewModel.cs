using Models;
using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate
{
    public class PayLaterModeViewModel : ValidatableBindableBase
    {
        public decimal? AmountToBePaid { get; set; }
        public decimal? AmountToBePaidLater { get { return this.AmountToBePaid - this._PayingAmountDec; } }

        private decimal? _PayingAmountDec { get { return Utility.TryToConvertToDecimal(_payingAmount); } }
        private string _payingAmount;

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [Range(0, 1000000, ErrorMessage = "Try value in Range(0, 10000000).")]
        [LessThanProperty(nameof(AmountToBePaid))]
        public string PayingAmount
        {
            get { return this._payingAmount; }
            set
            {
                this._payingAmount = value;
                this.OnPropertyChanged(nameof(AmountToBePaid));
                this.OnPropertyChanged(nameof(AmountToBePaidLater));
            }
        }
    }
}
