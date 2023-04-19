using System;

namespace BE_Homnayangi.Modules.NotificationModule.Request
{
    public class UpdatedNotificationStatus
    {
        public  Guid NotificationId { get; set; }
        public bool Status { get; set; }
    }
}
