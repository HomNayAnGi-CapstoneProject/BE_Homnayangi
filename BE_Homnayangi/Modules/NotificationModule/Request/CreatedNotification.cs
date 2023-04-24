using System;

namespace BE_Homnayangi.Modules.NotificationModule.Request
{
    public class CreatedNotification
    {
        public string Description { get; set; }
        public Guid? ReceiverId { get; set; }
    }
}
