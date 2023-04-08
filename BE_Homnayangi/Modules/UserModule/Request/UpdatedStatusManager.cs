using System;

namespace BE_Homnayangi.Modules.UserModule.Request
{
    public class UpdatedStatusManager
    {
        public Guid ManagerId { get; set; }
        public bool IsBlocked { get; set; }
    }
}
