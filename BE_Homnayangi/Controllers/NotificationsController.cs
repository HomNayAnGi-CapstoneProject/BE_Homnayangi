using BE_Homnayangi.Modules.NotificationModule.Interface;
using BE_Homnayangi.Modules.NotificationModule.Request;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult> GetNotifications()
        {
            try
            {
                var result = await _notificationService.GetAllNofications();
                return new JsonResult(new
                {
                    status = "success",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        [HttpGet("staff")]
        public async Task<ActionResult> GetNotificationsForStaff()
        {
            try
            {
                var result = await _notificationService.GetNoficationsForStaff();
                return new JsonResult(new
                {
                    status = "success",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        [HttpGet("manager")]
        public async Task<ActionResult> GetNotificationsForManager()
        {
            try
            {
                var result = await _notificationService.GetNoficationsForManager();
                return new JsonResult(new
                {
                    status = "success",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetNotification([FromRoute] Guid id)
        {
            try
            {
                var result = await _notificationService.GetNoficationById(id);
                return new JsonResult(new
                {
                    status = "success",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        // GET: api/Notifications/receivers/5
        [HttpGet("receivers/{receiverId}")]
        public async Task<ActionResult> GetNotificationsByReceiverId([FromRoute] Guid receiverId)
        {
            try
            {
                var result = await _notificationService.GetNoficationsByReceiverId(receiverId);
                return new JsonResult(new
                {
                    status = "success",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }


        // PUT: api/Notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutNotification([FromBody] UpdatedNotification request)
        {
            try
            {
                await _notificationService.UpdateNotification(request);
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateNotificationStatus([FromBody] UpdatedNotificationStatus request)
        {
            try
            {
                await _notificationService.UpdateNotificationStatus(request.NotificationId, request.Status);
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification([FromBody] CreatedNotification request)
        {
            try
            {
                var result = await _notificationService.CreateNotification(request);
                return new JsonResult(new
                {
                    status = "success",
                    notificationId = result
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification([FromRoute] Guid id)
        {
            try
            {
                await _notificationService.DeleteNotification(id);
                return new JsonResult(new
                {
                    status = "success"
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    msg = ex.Message
                });
            }
        }
    }
}
