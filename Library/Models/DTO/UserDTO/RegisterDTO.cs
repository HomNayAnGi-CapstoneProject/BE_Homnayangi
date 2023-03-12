using System.ComponentModel.DataAnnotations;

namespace Library.Models.DTO.UserDTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, ErrorMessage = "Username must be between 5 and 16 characters", MinimumLength = 5)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Password must be between 6 and 20 characters", MinimumLength = 6)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phonenumber is required")]
        [RegularExpression(@"^(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b", ErrorMessage = "Entered phone format is not valid.")]
        public string Phonenumber { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public int gender { get; set; }
    }
}
