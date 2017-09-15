using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.SignUp
{
    class HyperStoreAccountViewModel : ValidatableBindableBase
    {
        private string _mobileNo;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [RegularExpression(@"[987]\d{9}", ErrorMessage = "{0} is Invalid.")]
        public string MobileNo { get { return this._mobileNo; } set { SetProperty(ref _mobileNo, value); } }

        private string _password;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Short passwords are easy to guess. Try one with at least 6 characters.")]
        [RegularExpression(@"[a-zA-Z]{6,16}", ErrorMessage = "Need to change the regx dude.")]
        public string Password { get { return this._password; } set { SetProperty(ref _password, value); } }

        private string _confirmPassword;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "These passwords don't match. Try again?")]
        public string ConfirmPassword { get { return this._confirmPassword; } set { SetProperty(ref _confirmPassword, value); } }

    }
}
