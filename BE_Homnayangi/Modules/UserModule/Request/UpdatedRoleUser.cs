using System;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class UpdatedRoleUser
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
