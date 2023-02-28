using BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Interface;
using BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Request;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers.AdminControllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SeasonReferenceController : ControllerBase
    {
        private readonly ISeasonReferenceService _seasonRefService;
        private readonly IUserService _userService;

        public SeasonReferenceController(ISeasonReferenceService seasonRefService, IUserService userService)
        {
            _seasonRefService = seasonRefService;
            _userService = userService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeasonReference>>> GetSeasonReferences()
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _seasonRefService.GetSeasonReferences());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeasonReference>> GetSeasonRef([FromRoute] Guid? id)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _seasonRefService.GetSeasonRef(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutSeasonRef([FromBody] UpdateSeasonRefRequest updateSeasonRefRequest)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                await _seasonRefService.UpdateSeasonReference(updateSeasonRefRequest);
                return Ok("Update success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SeasonReference>> PostSeasonRef([FromBody] CreateNewSeasonRefRequest reqSeasonRef)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _seasonRefService.CreateNewSeasonRef(reqSeasonRef));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeasonRef([FromRoute] Guid? id)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                await _seasonRefService.DeleteSeasonReference(id);
                return Ok("Delete success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
