using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Request;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers.AdminControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BadgeConditionController : ControllerBase
    {
        private readonly IBadgeConditionService _badgeConditionService;
        private readonly IUserService _userService;

        public BadgeConditionController(IBadgeConditionService badgeConditionService, IUserService userService)
        {
            _badgeConditionService = badgeConditionService;
            _userService = userService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BadgeCondition>>> GetBadgeConditions()
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _badgeConditionService.GetBadgeConditions());
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
        /*   [Authorize(Roles = "Customer")]*/
        [HttpGet("customers")]

        public async Task<ActionResult<IEnumerable<BadgeCondition>>> GetBadgeConditionsByCustomers()
        {
            try
            {
                /*    if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                    {
                        throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                    }
    */
                return Ok(await _badgeConditionService.GetBadgeConditionsByCustomer());
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


        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BadgeCondition>> GetBadgeCondition([FromRoute] Guid? id)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _badgeConditionService.GetBadgeCondition(id));
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

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutBadgeCondition([FromBody] UpdateBadgeConditionRequest updateBadgeConditionRequest)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                await _badgeConditionService.UpdateBadgeCondition(updateBadgeConditionRequest);
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BadgeCondition>> PostBadgeCondition([FromBody] CreateNewBadgeConditionRequest reqBadgeCondition)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _badgeConditionService.CreateNewBadgeCondition(reqBadgeCondition));
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

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBadgeCondition([FromRoute] Guid? id)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                await _badgeConditionService.DeleteBadgeCondition(id);
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
