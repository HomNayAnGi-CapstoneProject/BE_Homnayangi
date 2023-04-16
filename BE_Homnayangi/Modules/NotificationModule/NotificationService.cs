using BE_Homnayangi.Modules.NotificationModule.Interface;
using BE_Homnayangi.Modules.NotificationModule.Request;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.NotificationModule
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
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
    }
}
