using BE_Homnayangi.Modules.AccomplishmentReactionModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccomplishmentReactionsController : ControllerBase
    {
        private readonly IAccomplishmentReactionService _accomplishmentReactionService;
        private readonly IUserService _userService;

        public AccomplishmentReactionsController(IAccomplishmentReactionService accomplishmentReactionService, IUserService userService)
        {
            _accomplishmentReactionService = accomplishmentReactionService;
            _userService = userService;
        }

        // GET: api/AccomplishmentReactions/5
        [HttpGet("accomplishments/{accomplishmentId}")]
        public async Task<ActionResult<AccomplishmentReaction>> GetReactionByCustomerId([FromRoute] Guid accomplishmentId)
        {
            try
            {
                var currentAccount = _userService.GetCurrentUser(Request.Headers["Authorization"]);

                var result = await _accomplishmentReactionService.GetReactionByCustomerId(currentAccount.Id, accomplishmentId);
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

        // PUT: api/AccomplishmentReactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{accomplishmentId}")]
        public async Task<IActionResult> InteractWithAccomplishment([FromRoute]Guid accomplishmentId)
        {
            try
            {
                var currentAccount = _userService.GetCurrentUser(Request.Headers["Authorization"]);

                var result = await _accomplishmentReactionService.InteractWithAccomplishment(currentAccount.Id, accomplishmentId);
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
