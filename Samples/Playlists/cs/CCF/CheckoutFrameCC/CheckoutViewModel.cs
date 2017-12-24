﻿using Mvvm;
using SDKTemplate;
using SDKTemplate.Models;
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
    public class CheckoutViewModel : ValidatableBindableBase
    {
        public decimal? AmountToBePaid { get; set; }
        public decimal? AmountToBePaidLater { get { return this.AmountToBePaid - this._PayingAmountDec; } }
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [Range(0d, 100, ErrorMessage = "Try Interest Rate in Range(0, 100)")]
        public string IntrestRate { get; set; }

        private decimal? _PayingAmountDec { get { return Utility.TryToConvertToDecimal(_payingAmount); } }
        private string _payingAmount;

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [Range(0, float.MaxValue, ErrorMessage = "Try value in Range(0, 100,00,000).")]
        [LessThanProperty(nameof(AmountToBePaid))]
        public string PayingAmount
        {
            get { return this._payingAmount; }
            set
            {
                this._payingAmount = value;
                this.OnPropertyChanged(nameof(PayingAmount));
                this.OnPropertyChanged(nameof(AmountToBePaidLater));
            }
        }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        public PaymentOption PaymentOption { get; set; }

        public CheckoutViewModel() { }
    }
}
