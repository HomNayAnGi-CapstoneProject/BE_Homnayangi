using System;

namespace BE_Homnayangi.Modules.NotificationModule.Request
{
    public class UpdatedNotification
    {
        public  Guid NotificationId { get; set; }
        public string Description { get; set; }
    }
}
