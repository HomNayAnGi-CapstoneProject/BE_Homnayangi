using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IBlogService _eventService;

        public EventsController(IUserService userService, IBlogService eventService)
        {
            _userService = userService;
            _eventService = eventService;
        }

        [HttpGet] // manage
        public async Task<ActionResult> GetAllEvent([FromQuery] bool? isExpired)
        {
            try
            {
                var result = await _eventService.GetAllEvent(isExpired);
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
    }
}
