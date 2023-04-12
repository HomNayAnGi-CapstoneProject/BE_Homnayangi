using System;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class UpdatedUserRole
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}
