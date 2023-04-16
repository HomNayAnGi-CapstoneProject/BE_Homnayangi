using BE_Homnayangi.Modules.NotificationModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.NotificationModule
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        private readonly HomnayangiContext _db;

        public NotificationRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Notification>> GetNotificationsBy(
            Expression<Func<Notification, bool>> filter = null,
            Func<IQueryable<Notification>, ICollection<Notification>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Notification> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : await query.ToListAsync();
        }
    }
}
