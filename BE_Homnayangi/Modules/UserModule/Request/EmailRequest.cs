using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class EmailRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
