using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class UpdatePasswordRequest
    {
        [Required(ErrorMessage = "Please enter your password")]
        public string oldPassword { get; set; }
        [Required(ErrorMessage = "Please enter your new password")]
        public string newPassword { get; set; }
    }
}
