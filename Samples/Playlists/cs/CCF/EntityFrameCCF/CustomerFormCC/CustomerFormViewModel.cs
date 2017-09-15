﻿using Models;
using Mvvm;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace SDKTemplate
{
    public class CustomerFormViewModel : ValidatableBindableBase, ICustomer
    {
        public Guid? CustomerId
        {
            get; set;
        }
        private string _address;
        public virtual string Address { get { return this._address; } set { this._address = value; } }

        private string _gstin;
        [RegularExpression(@"\d{2}[a-zA-Z]{5}\d{4}[a-zA-Z]{1}\d{1}[zZ][a-zA-Z0-9]", ErrorMessage = "{0} is Invalid.")]
        public string GSTIN { get { return this._gstin; } set { SetProperty(ref _gstin, value); } }

        private string _mobileNo;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [RegularExpression(@"[987]\d{9}", ErrorMessage = "{0} is Invalid.")]
        public virtual string MobileNo { get { return this._mobileNo; } set { SetProperty(ref _mobileNo, value); } }

        private string _name;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(16, ErrorMessage ="Try name with atmost 16 charecters.")]
        [RegularExpression(@"[a-zA-Z ]{1,16}", ErrorMessage = "Name is Invalid")]
        public virtual string Name
        {
            get { return this._name; }
            set { SetProperty(ref _name, value); }
        }
        public CustomerFormViewModel()
        {
            this.CustomerId = null;
            this._address = "";
            this._mobileNo = "";
            this._name = "";
            this._gstin = "";
        }
    }
}
