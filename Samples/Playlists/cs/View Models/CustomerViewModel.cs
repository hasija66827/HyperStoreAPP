using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModel;
using Windows.Globalization.DateTimeFormatting;
using SDKTemp.Data;
using Mvvm;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using SDKTemplate.DTO;

namespace SDKTemplate
{
    public class CustomerViewModel : ValidatableBindableBase
    {
        private DelegateCommand _validateCommand;
        private Guid _customerId;
        public Guid CustomerId { get { return this._customerId; } }

        private string _address;
        public virtual string Address { get { return this._address; } set { this._address = value; } }

        private string _gstin;
        [RegularExpression(@"\d{2}[a-zA-Z]{5}\d{4}[a-zA-Z]{1}\d{1}[zZ][a-zA-Z0-9]", ErrorMessage = "{0} is Invalid.")]
        public string GSTIN { get { return this._gstin; } set { SetProperty(ref _gstin, value); } }

        private string _mobileNo;
        [RegularExpression(@"[987]\d{9}", ErrorMessage = "{0} is Invalid.")]
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        public virtual string MobileNo { get { return this._mobileNo; } set { SetProperty(ref _mobileNo, value); } }

        private string _name;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        public virtual string Name
        {
            get { return this._name; }
            set { SetProperty(ref _name, value); }
        }

        private decimal _walletBalance;
        public virtual decimal WalletBalance { get { return this._walletBalance; } set { this._walletBalance = value; } }

        private string _walletBalanceString;

        [Numeric(ErrorMessage ="Wallet Amount must be numeric.")]
        public virtual string WalletBalanceString {
            get { return this._walletBalanceString; }
            set { SetProperty(ref _walletBalanceString, value);
                this._walletBalance = Utility.TryToConvertToFloat(_walletBalanceString);
            }
        }

        public CustomerViewModel()
        {
            _validateCommand = new DelegateCommand(ValidateAndSave_Executed);
            this._customerId = Guid.NewGuid();
            this._address = "";
            this._mobileNo = "";
            this._name = "";
            this._gstin = "";
            this._walletBalance = 0;
            this._walletBalanceString = "";
        }

        public CustomerViewModel(DatabaseModel.Customer customer)
        {
            _validateCommand = new DelegateCommand(ValidateAndSave_Executed);
            this._customerId = customer.CustomerId;
            this._address = customer.Address;
            this._gstin = customer.GSTIN;
            this._mobileNo = customer.MobileNo;
            this._name = customer.Name;
            this._walletBalance =(decimal) customer.WalletBalance;
            this._walletBalanceString = "";
        }

        /// <summary>
        /// This poperty is used by CustomerASBCC for its display member property of SearchBox.
        /// </summary>
        public string Customer_MobileNo_Address
        {
            get
            { return string.Format("{0}({1})", this.MobileNo, this.Address); }
        }

        public ICommand ValidateCommand
        {
            get { return _validateCommand; }
        }

        private void ValidateAndSave_Executed()
        {
            var IsValid = ValidateProperties();
            if (IsValid && Utility.CheckIfUniqueMobileNumber(this._mobileNo, Person.Customer))
            {
                CustomerDTO customerDTO = new CustomerDTO()
                {
                    Address = this.Address,
                    GSTIN = this.GSTIN,
                    MobileNo = this.MobileNo,
                    Name = this.Name,
                    WalletBalance = this.WalletBalance
                };
                CustomerDataSource.CreateNewCustomer(customerDTO);
                MainPage.Current.NotifyUser("New customer was added succesfully ", NotifyType.StatusMessage);
            }
        }
    }
}
