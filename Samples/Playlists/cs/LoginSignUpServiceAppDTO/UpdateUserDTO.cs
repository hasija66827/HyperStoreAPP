using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LoginSignUpService.DTO
{
    public class UpdateUserDTO
    {
        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [RegularExpression(@"\d{6}", ErrorMessage = "Try passcode with 6 digits.")]
        public string Passcode { get; set; }
    }
}