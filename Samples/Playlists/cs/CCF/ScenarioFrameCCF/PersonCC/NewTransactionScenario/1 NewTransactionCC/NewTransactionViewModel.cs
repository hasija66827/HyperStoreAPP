﻿using Models;
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
        public PersonViewModelBase Person { get; set; }

        public string Title
        {
            get
            {
                if (this.Person.EntityType == EntityType.Customer)
                    return "Receive Money";
                else
                    return "Pay Money";
            }
        }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [Range(0, float.MaxValue, ErrorMessage = "Try amount in range(0, 10000000).")]
        [LessThanProperty(nameof(AbsWalletBalance))]
        public string Amount { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(16, ErrorMessage = "Try description with atmost 16 charecters.")]
        public string Description { get; set; }

        public decimal AbsWalletBalance { get { return Math.Abs(this.Person.WalletBalance); } }

        public decimal? UpdatedWalletBalance
        {
            get
            {
                if (this.Person.EntityType == EntityType.Supplier)
                    return this.Person.WalletBalance - Utility.TryToConvertToDecimal(this.Amount);
                else
                    return this.Person.WalletBalance + Utility.TryToConvertToDecimal(this.Amount);
            }
        }

        public string ProceedToPay
        {
            get
            {
                if (this.Person.EntityType == EntityType.Customer)
                    return "Proceed to Receive " + Utility.ConvertToRupee(Amount);
                else
                    return "Proceed to Pay " + Utility.ConvertToRupee(Amount);
            }
        }

        public NewTransactionViewModel() { }
    }
}
