using Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.SignUp
{
    public class BusinessInformationViewModel : ValidatableBindableBase
    {
        private string _businessName;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(25, ErrorMessage = "Try Business name with atmost 25 charecters.")]
        [RegularExpression(@"[a-zA-Z]{1,20}", ErrorMessage = "Business name is Invalid")]
        public string BusinessName
        {
            get { return this._businessName; }
            set { SetProperty(ref _businessName, value); }
        }

        private string _addressLine;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(25, ErrorMessage = "Try address with atmost 25 charecters.")]
        public string AddressLine
        {
            get { return this._addressLine; }
            set { SetProperty(ref _addressLine, value); }
        }

        private string _city;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(15, ErrorMessage = "Try city with atmost 15 charecters.")]
        public string City
        {
            get { return this._city; }
            set { SetProperty(ref _city, value); }
        }

        private string _pincode;
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(6, ErrorMessage = "Try pincode with 6 digits.")]
        [RegularExpression(@"[1-9]\d{5}", ErrorMessage = "Try pincode with 6 digits.")]
        public string PinCode
        {
            get { return this._pincode; }
            set { SetProperty(ref _pincode, value); }
        }

        public string SelectedCategory { get; set; }
        public int SelectedStateIndex { get; set; }

        public List<string> Category { get; }

        public List<string> State { get; }

        public string FullAddress
        {
            get
            {
                var state = SelectedStateIndex > -1 ? State[SelectedStateIndex] : "";
                return (_addressLine + " " + _city + ", " + state + " " + PinCode);
            }
        }

        public BusinessInformationViewModel()
        {
            var x = new List<string>();
            x.Add("Apparels and Shoes Store");
            x.Add("Medical Store");
            x.Add("Grocerry Store");
            x.Add("Electronic Store");
            Category = x;

            var s = new List<string>();
            s.Add("Delhi");
            s.Add("Gujrat");
            s.Add("Madhya Pradesh");
            s.Add("Maharashtra");
            s.Add("Rajhasthan");
            s.Add("Telengana");
            State = s;
        }
    }
}
