using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LoginSignUpService.DTO
{
    public class CreateUserDTO
    {
        [Required]
        public PersonalInformationDTO PI { get; set; }
        [Required]
        public BusinessInformationDTO BI { get; set; }

        [Required(ErrorMessage = "You can't leave this empty.", AllowEmptyStrings = false)]
        public string DeviceId { get; set; }
    }
}