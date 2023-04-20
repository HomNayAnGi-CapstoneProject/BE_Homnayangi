using BE_Homnayangi.Modules.NotificationModule.Request;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.NotificationModule.Interface
{
    public interface INotificationService
    {

        public Task<ICollection<Notification>> GetNotificationsBy(
            Expression<Func<Notification, bool>> filter = null,
            Func<IQueryable<Notification>, ICollection<Notification>> options = null,
            string includeProperties = null);

        public Task<ICollection<Notification>> GetAllNofications();

        public Task<Notification> GetNoficationById(Guid id);

        public Task<ICollection<Notification>> GetNoficationsByReceiverId(Guid receiverId);

        public Task<ICollection<Notification>> GetNoficationsForStaff();

        public Task<Guid> CreateNotification(CreatedNotification request);

        public Task UpdateNotificationStatus(Guid id, bool status);

        public Task UpdateNotification(UpdatedNotification request);

        public Task DeleteNotification(Guid id);
    }
}
