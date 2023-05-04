using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface;
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
    public class CaloReferenceController : ControllerBase
    {
        private readonly ICaloReferenceService _caloRefService;
        private readonly IUserService _userService;

        public CaloReferenceController(ICaloReferenceService caloRefService, IUserService userService)
        {
            _caloRefService = caloRefService;
            _userService = userService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaloReference>>> GetCaloReferences()
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _caloRefService.GetCaloReferences());
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
        public async Task<ActionResult<CaloReference>> GetCaloRef([FromRoute] Guid? id)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                return Ok(await _caloRefService.GetCaloRef(id));
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
        public async Task<IActionResult> PutCaloRef([FromBody] UpdateCaloRefRequest updateCaloRefRequest)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                await _caloRefService.UpdateCaloReference(updateCaloRefRequest);
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
        [Authorize(Roles = "Staff")]
        public async Task<ActionResult<CaloReference>> PostCaloRef([FromBody] CreateNewCaloRefRequest reqCaloRef)
        {
            try
            {
                var result = await _caloRefService.CreateNewCaloRef(reqCaloRef);
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

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaloRef([FromRoute] Guid? id)
        {
            try
            {
                if (_userService.GetCurrentUser(Request.Headers["Authorization"]) == null)
                {
                    throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
                }

                await _caloRefService.DeleteCaloReference(id);
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
