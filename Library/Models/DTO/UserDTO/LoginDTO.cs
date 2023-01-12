using GSF.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.DTO.UserDTO
{
   public class LoginDTO
    {
      
   
        [Required(ErrorMessage = "Please enter your username.")] 
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        
        public string Password { get; set; }
    }
}
