using BE_Homnayangi.Modules.NotificationModule.Interface;
using BE_Homnayangi.Modules.NotificationModule.Request;
using Library.Models.Constant;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Notification = Library.Models.Notification;

namespace BE_Homnayangi.Modules.NotificationModule
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public Task<ICollection<Notification>> GetNotificationsBy(
                Expression<Func<Notification,
                bool>> filter = null,
                Func<IQueryable<Notification>,
                ICollection<Notification>> options = null,
                string includeProperties = null)
        {
            return _notificationRepository.GetNotificationsBy(filter, options, includeProperties);
        }

        public async Task<ICollection<Notification>> GetAllNofications() // DEACTIVE, UNREAD, READ
        {
            try
            {
                var result = await _notificationRepository.GetAll();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllNofications:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Notification> GetNoficationById(Guid id) // DEACTIVE, UNREAD, READ
        {
            try
            {
                var result = await _notificationRepository.GetFirstOrDefaultAsync(n => n.NotificationId == id);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetNoficationById:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Notification>> GetNoficationsByReceiverId(Guid receiverId)
        {
            try
            {
                var result = await _notificationRepository.GetNotificationsBy(n => n.ReceiverId == receiverId);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetNoficationByReceiverId:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guid> CreateNotification(CreatedNotification request)
        {
            try
            {
                Notification noti = new Notification()
                {
                    NotificationId = Guid.NewGuid(),
                    Description = request.Description,
                    CreatedDate = DateTime.Now,
                    Status = false, // UNREAD
                    ReceiverId = request.ReceiverId
                };
                await _notificationRepository.AddAsync(noti);
                return noti.NotificationId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateNotification:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateNotification(UpdatedNotification request)
        {
            try
            {
                var noti = await _notificationRepository.GetByIdAsync(request.NotificationId);
                if (noti == null)
                    throw new Exception(ErrorMessage.NotificationError.NOTIFICATION_NOT_FOUND);
                noti.Description = request.Description;
                await _notificationRepository.UpdateAsync(noti);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateNotification:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteNotification(Guid id)
        {
            try
            {
                var noti = await _notificationRepository.GetByIdAsync(id);
                if (noti == null)
                    throw new Exception(ErrorMessage.NotificationError.NOTIFICATION_NOT_FOUND);
                await _notificationRepository.RemoveAsync(noti);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteNotification:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateNotificationStatus(Guid id, bool status)
        {
            try
            {
                var noti = await _notificationRepository.GetByIdAsync(id);
                if (noti == null)
                    throw new Exception(ErrorMessage.NotificationError.NOTIFICATION_NOT_FOUND);
                noti.Status = status;
                await _notificationRepository.UpdateAsync(noti);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateNotificationStatus:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Notification>> GetNoficationsForStaff()
        {
            try
            {
                var result = await _notificationRepository.GetNotificationsBy(n => n.ReceiverId == null);
                return result.OrderByDescending(r => r.CreatedDate).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetNoficationsForStaff:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
