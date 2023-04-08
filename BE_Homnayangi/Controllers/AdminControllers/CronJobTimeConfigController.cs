using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Request;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers.AdminControllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class CronJobTimeConfigController : Controller
    {

        private readonly ICronJobTimeConfigService _cronJobTimeConfigService;
        private readonly IUserService _userService;

        public CronJobTimeConfigController(ICronJobTimeConfigService cronJobTimeConfigService, IUserService userService)
        {
            _cronJobTimeConfigService = cronJobTimeConfigService;
            _userService = userService;
        }
        // GET: CronJobTimeConfigController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CronJobTimeConfig>>> GetAllCronJobTimeConfigs()
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _cronJobTimeConfigService.GetAllCronJobTimeConfigs());
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
        // GET: CronJobTimeConfigController/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CronJobTimeConfig>> GetCronJobTimeConfig([FromRoute] Guid? id)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _cronJobTimeConfigService.GetCronJobTimeConfig(id));
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
        // GET: CronJobTimeConfigController/Create
        [HttpPost]
        public async Task<ActionResult<CronJobTimeConfig>> PostCronJobTimeConfig([FromBody] CreateNewCronJobTimeConfig newCronJobTimeConfig)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _cronJobTimeConfigService.CreateNewCronJobTimeConfig(newCronJobTimeConfig));
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

        [HttpPut]
        public async Task<IActionResult> PutCronJobTimeConfig([FromBody] UpdateCronJobTimeConfig updateCronJobTimeConfigRequest)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                await _cronJobTimeConfigService.UpdateCronJobTimeConfig(updateCronJobTimeConfigRequest);
                return Ok("Update success");
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCronJobTimeConfig([FromRoute] Guid? id)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                await _cronJobTimeConfigService.DeleteCronJobTimeConfig(id);
                return Ok("Delete success");
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



