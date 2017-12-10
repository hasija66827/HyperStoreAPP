using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LoginSignUpService.DTO
{
    public class OTPVerificationDTO
    {
        [Required]
        public Guid? UserID { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        [RegularExpression(@"[987]\d{9}", ErrorMessage = "{0} is Invalid.")]
        public string ReceiverMobileNo { get; set; }

        [Required]
        [StringLength(140)]
        public string SMSContent { get; set; }
    }
}