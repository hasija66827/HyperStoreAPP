using SDKTemplate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mvvm;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using SDKTemplate.DTO;
using Models;
using Windows.UI.Xaml.Controls;

namespace SDKTemplate
{
    public class SupplierFormViewModel : ValidatableBindableBase, supplierInterface
    {
        public Guid? SupplierId { get; set; }

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

        private decimal _walletBalance;
        public virtual decimal WalletBalance { get { return this._walletBalance; } set { this._walletBalance = value; } }

        public SupplierFormViewModel()
        {
            this._address = null;
            this._mobileNo = null;
            this._name = null;
            this._walletBalance = 0;
        }

        public string WholeSeller_MobileNo_Address
        {
            get { return string.Format("{0}({1})", this.MobileNo, this.Address); }
        }

        public string WholeSeller_Name_MobileNo
        {
            get { return string.Format("{0} ({1})", this.MobileNo, this.Name); }
        }
    }
}
