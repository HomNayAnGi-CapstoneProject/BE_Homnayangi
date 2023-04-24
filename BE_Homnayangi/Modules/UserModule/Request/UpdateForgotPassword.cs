using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class UpdateForgotPassword
    {
        [Required(ErrorMessage = "Please enter your new password")]
        [StringLength(20, ErrorMessage = "Password must be between 6 and 20 characters", MinimumLength = 6)]
        public string newPassword { get; set; }
    }
}
