using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class UpdateUser
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid UserId { get; set; }
        [StringLength(16, ErrorMessage = "Username must be between 5 and 16 characters", MinimumLength = 5)]
        public string Username { get; set; }
        public string Displayname { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Phonenumber is required")]
        [RegularExpression(@"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b", ErrorMessage = "Entered phone format is not valid.")]
        public string Phonenumber { get; set; }
        public bool? Gender { get; set; }
        public string Avatar { get; set; }

    }
}
