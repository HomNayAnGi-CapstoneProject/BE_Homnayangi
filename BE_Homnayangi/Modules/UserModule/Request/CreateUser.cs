using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class CreateUser
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, ErrorMessage = "Username must be between 5 and 16 characters", MinimumLength = 5)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Displayname is required")]
        public string Displayname { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phonenumber is required")]
        [RegularExpression(@"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b", ErrorMessage = "Entered phone format is not valid.")]
        public string Phonenumber { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public bool? Gender { get; set; }
        [Required(ErrorMessage = "Avatar is required")]
        public string Avatar { get; set; }
    }
}
