using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.NotificationModule.Interface
{
    public interface INotificationRepository : IRepository<Notification>
    {
        public Task<ICollection<Notification>> GetNotificationsBy(
           Expression<Func<Notification, bool>> filter = null,
           Func<IQueryable<Notification>, ICollection<Notification>> options = null,
           string includeProperties = null
       );
    }
}
