using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.Settings
{
    class SettingsViewModel : ValidatableBindableBase
    {
        private string _passcode;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [RegularExpression(@"\d{6}", ErrorMessage = "Try passcode with 6 digits.")]
        public string Passcode { get { return this._passcode; } set { SetProperty(ref _passcode, value); } }

        private string _confirmPasscode;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Compare("Passcode", ErrorMessage = "These Passcodes don't match. Try again?")]
        public string ConfirmPasscode { get { return this._confirmPasscode; } set { SetProperty(ref _confirmPasscode, value); } }
    }
}
