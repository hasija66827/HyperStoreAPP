using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LoginSignUpService.DTO
{
    public class AuthenticateUserDTO
    {
        [RegularExpression(@"[987]\d{9}", ErrorMessage = "{0} is Invalid.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        public string DeviceId { get; set; }

    }
}