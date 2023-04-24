using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AccomplishmentReactionModule.Interface;
using BE_Homnayangi.Modules.NotificationModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Enum;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Operations;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AccomplishmentReactionModule
{
    public class AccomplishmentReactionService : IAccomplishmentReactionService
    {

        private readonly IAccomplishmentReactionRepository _accomplishmentReactionRepository;
        private readonly IAccomplishmentRepository _accomplishmentRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public AccomplishmentReactionService(IAccomplishmentReactionRepository accomplishmentReactionRepository,
            INotificationRepository notificationRepository, IUserRepository userRepository, IAccomplishmentRepository accomplishmentRepository,
            IHubContext<SignalRServer> hubContext)
        {
            _accomplishmentReactionRepository = accomplishmentReactionRepository;
            _accomplishmentRepository = accomplishmentRepository;
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        public async Task<bool> GetReactionByCustomerId(Guid customerId, Guid accomplishmentId)
        {
            try
            {
                var item = await _accomplishmentReactionRepository.GetFirstOrDefaultAsync(x => x.AccomplishmentId == accomplishmentId
                                                                                         && x.CustomerId == customerId
                                                                                         && x.Status);
                return item != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetReactionByCustomerId:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<AccomplishmentReaction> InteractWithAccomplishment(Guid customerId, Guid accomplishmentId)
        {
            try
            {
                var item = await _accomplishmentReactionRepository.GetFirstOrDefaultAsync(x => x.CustomerId == customerId
                                                                                        && x.AccomplishmentId == accomplishmentId);
                if (item == null) // insert new record
                {
                    item = new AccomplishmentReaction()
                    {
                        AccomplishmentId = accomplishmentId,
                        CustomerId = customerId,
                        CreatedDate = DateTime.Now,
                        Status = true,
                    };
                    await _accomplishmentReactionRepository.AddAsync(item);
                }
                else
                {
                    item.Status = !item.Status;
                    await _accomplishmentReactionRepository.UpdateAsync(item);
                }

                #region Create notification
                var notiId = Guid.NewGuid();
                var accom = await _accomplishmentRepository.GetByIdAsync(accomplishmentId);
                var customer = await _userRepository.GetByIdAsync(customerId);
                if(accom != null && customer != null)
                {
                    var noti = new Library.Models.Notification
                    {
                        NotificationId = notiId,
                        Description = $"{customer.Displayname ?? customer.Username} đã tương tác với thành quả của bạn",
                        CreatedDate = DateTime.Now,
                        Status = false,
                        ReceiverId = accom.AuthorId
                    };
                    await _notificationRepository.AddAsync(noti);
                    await _hubContext.Clients.All.SendAsync($"{accom.AuthorId}_InteractAccomplishment", JsonConvert.SerializeObject(noti));
                }
                #endregion
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at InteractWithAccomplishment:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
