using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.SignUp
{
   public  class ProfileCompletionViewModel : ValidatableBindableBase
    {
        private string _firstName;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(9, ErrorMessage = "Try first name with atmost 9 charecters.")]
        [RegularExpression(@"[a-zA-Z]{1,9}[\s]{0,1}", ErrorMessage = "First name is Invalid")]
        public string FirstName
        {
            get { return this._firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(9, ErrorMessage = "Try last name with atmost 9 charecters.")]
        [RegularExpression(@"[a-zA-Z]{1,9}", ErrorMessage = "Last name is Invalid")]
        public string LastName
        {
            get { return this._lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        public string Name
        {
            get { return this._firstName + this._lastName; }
        }

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

        private string _emailId;
        [EmailAddress(ErrorMessage = "Email Id is Invalid")]
        public string EmailId
        {
            get { return this._emailId; }
            set { SetProperty(ref _emailId, value); }
        }

        public DateTime DateOfBirth { get; set; }    
    }
}
