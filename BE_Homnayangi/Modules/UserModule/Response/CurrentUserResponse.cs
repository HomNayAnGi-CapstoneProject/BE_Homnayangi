using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Response
{
    public class CurrentUserResponse
    {
        public Guid Id { get; set; }
        public string Displayname { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public int? Gender { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
    }
}
