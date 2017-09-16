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
        [RegularExpression(@"[a-zA-Z\s]{1,25}", ErrorMessage = "Business name is Invalid")]
        public string BusinessName
        {
            get { return this._businessName; }
            set { SetProperty(ref _businessName, value); }
        }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [RegularExpression(@"\d{2}[a-zA-Z]{5}\d{4}[a-zA-Z]{1}\d{1}[zZ][a-zA-Z0-9]", ErrorMessage = "{0} is Invalid.")]
        public string GSTIN { get; set; }

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

        public string SelectedBusinessType { get { if (SelectedBusinessTypeIndex > -1) return BusinessTypes[SelectedBusinessTypeIndex]; return ""; } }
        public int SelectedBusinessTypeIndex { get; set; }
        public List<string> BusinessTypes { get; }

        public string SelectedState { get { if (SelectedStateIndex > -1) return States[SelectedStateIndex]; return ""; } }
        public int SelectedStateIndex { get; set; }
        public List<string> States { get; }

        public string FullAddress
        {
            get
            {
                if (SelectedStateIndex > -1)
                    return _addressLine + " " + _city + ",  " + SelectedState + "  " + PinCode;
                else
                    return "";
            }
        }

        public BusinessInformationViewModel()
        {
            var x = new List<string>();
            x.Add("Apparels Store");
            x.Add("Electronic Store");
            x.Add("Grocerry Store");
            x.Add("Medical Store");
            x.Add("Shoes Store");
            SelectedBusinessTypeIndex = -1;
            BusinessTypes = x;

            var s = new List<string>();
            s.Add("Delhi");
            s.Add("Gujrat");
            s.Add("Karnataka");
            s.Add("Madhya Pradesh");
            s.Add("Maharashtra");
            s.Add("Rajasthan");
            s.Add("Telengana");
            States = s;
            SelectedStateIndex = -1;
        }
    }
}
