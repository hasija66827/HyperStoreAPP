using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.Login
{
    class LoginViewModel: ValidatableBindableBase
    {
        private string _mobileNo;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [RegularExpression(@"[987]\d{9}", ErrorMessage = "{0} is Invalid.")]
        public string MobileNo { get { return this._mobileNo; } set { SetProperty(ref _mobileNo, value); } }

        private string _password;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get { return this._password; } set { SetProperty(ref _password, value); } }

    }
}
