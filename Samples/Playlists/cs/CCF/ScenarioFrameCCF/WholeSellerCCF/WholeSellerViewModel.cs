using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mvvm;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace SDKTemplate
{
    public class WholeSellerViewModel : ValidatableBindableBase
    {
        private DelegateCommand validateCommand;
        private Guid _wholeSellerId;
        public virtual Guid SupplierId { get { return this._wholeSellerId; } set { this._wholeSellerId = value; } }

        private string _address;
        public virtual string Address
        {
            get { return this._address; }
            set { SetProperty(ref _address, value); }
        }

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

        private float _walletBalance;
        public virtual float WalletBalance { get { return this._walletBalance; } set { this._walletBalance = value; } }

        public WholeSellerViewModel()
        {
            validateCommand = new DelegateCommand(ValidateAndSave_Executed);
            this._wholeSellerId = Guid.NewGuid();
            this._address = "";
            this._mobileNo = "";
            this._name = "";
            this._walletBalance = 0;
        }

        public WholeSellerViewModel(DatabaseModel.WholeSeller wholeSeller)
        {
            validateCommand = new DelegateCommand(ValidateAndSave_Executed);
            this._wholeSellerId = wholeSeller.WholeSellerId;
            this._address = wholeSeller.Address;
            this._gstin = wholeSeller.GSTIN;
            this._mobileNo = wholeSeller.MobileNo;
            this._name = wholeSeller.Name;
            this._walletBalance = wholeSeller.WalletBalance;
        }

        public string WholeSeller_MobileNo_Address
        {
            get { return string.Format("{0}({1})", this.MobileNo, this.Address); }
        }

        public string WholeSeller_Name_MobileNo
        {
            get { return string.Format("{0}\n{1}", this.Name, this.MobileNo); }
        }

        public ICommand ValidateCommand
        {
            get { return validateCommand; }
        }

        private void ValidateAndSave_Executed()
        {
            var IsValid = ValidateProperties();
            if (IsValid && Utility.CheckIfUniqueMobileNumber(this.MobileNo, Person.WholeSeller))
            {
                WholeSellerDataSource.CreateWholeSeller(this);
                MainPage.Current.NotifyUser("New wholesller was added succesfully ", NotifyType.StatusMessage);
            }
        }
    }
}
