using Models;
using Mvvm;
using SDKTemplate.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace SDKTemplate
{
    public class CustomerFormViewModel : ValidatableBindableBase, customerInterface
    {
        private DelegateCommand _validateCommand;

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

        private decimal? _walletBalance;
        public virtual decimal? WalletBalance { get { return this._walletBalance; } set { this._walletBalance = value; } }

        private string _walletBalanceString;

        [Numeric(ErrorMessage = "Wallet Amount must be numeric.")]
        public virtual string WalletBalanceString
        {
            get { return this._walletBalanceString; }
            set
            {
                SetProperty(ref _walletBalanceString, value);
                this._walletBalance = Utility.TryToConvertToFloat(_walletBalanceString);
            }
        }

        public CustomerFormViewModel()
        {
            _validateCommand = new DelegateCommand(ValidateAndSave_Executed);
            this._address = "";
            this._mobileNo = "";
            this._name = "";
            this._gstin = "";
            this._walletBalance = 0;
            this._walletBalanceString = "";
        }

        public ICommand ValidateCommand
        {
            get { return _validateCommand; }
        }

        private async void ValidateAndSave_Executed()
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
                await CustomerDataSource.CreateNewCustomerAsync(customerDTO);
                MainPage.Current.NotifyUser("New customer was added succesfully ", NotifyType.StatusMessage);
            }
        }
    }
}
