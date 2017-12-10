using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LoginSignUpService.DTO
{
    public class PersonalInformationDTO
    {
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [MaxLength(9, ErrorMessage = "Try first name with atmost 9 charecters.")]
        [RegularExpression(@"[a-zA-Z]{1,9}[\s]{0,1}", ErrorMessage = "First name is Invalid")]
        public string FirstName { get; set; }

        [MaxLength(9, ErrorMessage = "Try last name with atmost 9 charecters.")]
        [RegularExpression(@"[a-zA-Z]{1,9}", ErrorMessage = "Last name is Invalid")]
        public string LastName { get; set; }

        public string EmailId { get; set; }

        public DateTime DateOfBirth { get; set; }

        [RegularExpression(@"[987]\d{9}", ErrorMessage = "{0} is Invalid.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Short passwords are easy to guess. Try one with at least 6 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [RegularExpression(@"\d{6}", ErrorMessage = "Try passcode with 6 digits.")]
        public string Passcode { get; set; }
    }
}