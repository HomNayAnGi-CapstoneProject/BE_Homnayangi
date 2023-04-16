using BE_Homnayangi.Modules.NotificationModule.Request;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.NotificationModule.Interface
{
    public interface INotificationService
    {
        public Task<ICollection<Notification>> GetAllNofications();

        public Task<Notification> GetNoficationById(Guid id);

        public Task<ICollection<Notification>> GetNoficationsByReceiverId(Guid receiverId);

        public Task<Guid> CreateNotification(CreatedNotification request);

        public Task UpdateNotification(UpdatedNotification request);

        public Task DeleteNotification(Guid id);
    }
}
