using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AccomplishmentModule.Request;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccomplishmentsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAccomplishmentService _accomplishmentService;

        public AccomplishmentsController(IAccomplishmentService accomplishmentService, IUserService userService)
        {
            _accomplishmentService = accomplishmentService;
            _userService = userService;
        }

        // Create a new Accomplishment by Customer
        [Authorize(Roles = "Customer")]
        [HttpPost] // chỗ này FE KHÔNG CẦN truyền AuthorId để check chính chủ Accomplishment
        public async Task<ActionResult> PostAccomplishment([FromBody] CreatedAccomplishment request)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                var result = await _accomplishmentService.CreateANewAccomplishment(currentUser.Id, request);
                if (result)
                {
                    return new JsonResult(new
                    {
                        status = "success"
                    });
                }
                else
                {
                    return new JsonResult(new
                    {
                        status = "failed",
                        msg = "Request data is not enough"
                    });
                }
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

        #region  Update Accomplishment and Approve - Reject
        [Authorize(Roles = "Staff,Manager")]
        [HttpPut("approve-reject")]
        public async Task<IActionResult> ApproveRejectAccomplishment([FromBody] VerifiedAccomplishment request)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                var result = await _accomplishmentService.ApproveRejectAccomplishment(request, currentUser.Id);
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

        [Authorize(Roles = "Customer")]
        [HttpPut] // chỗ này FE phải truyền AuthorId để check chính chủ Accomplishment
        public async Task<ActionResult> UpdateAccomplishmentDetail([FromBody] UpdatedAccomplishment request)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                var result = await _accomplishmentService.UpdateAccomplishmentDetail(currentUser.Id, request);
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
        #endregion

        #region Get Accomplishment
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAccomplishmentById([FromRoute] Guid id)
        {
            try
            {
                var result = await _accomplishmentService.GetAccomplishmentById(id);
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

        [Authorize(Roles = "Staff,Manager")]
        [HttpGet]
        public async Task<ActionResult> GetAccomplishmentByStatus([FromQuery] string status)
        {
            try
            {
                var result = await _accomplishmentService.GetAccomplishmentByStatus(status);
                return new JsonResult(new
                {
                    total = result.Count,
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

        [Authorize(Roles = "Staff,Manager")]
        [HttpGet("customers/{customerId}")]
        public async Task<ActionResult> GetAccomplishmentByCustomerId([FromRoute] Guid customerId)
        {
            try
            {
                var result = await _accomplishmentService.GetAccomplishmentsByCustomerId(customerId);
                return new JsonResult(new
                {
                    total = result.Count,
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

        [HttpGet("blogs/{blogId}")]
        public async Task<ActionResult> GetAccomplishmentsByBlogId([FromRoute] Guid blogId)
        {
            try
            {
                var result = await _accomplishmentService.GetAccomplishmentsByBlogId(blogId);
                return new JsonResult(new
                {
                    total = result.Count,
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
        #endregion

        #region

        //[Authorize(Roles = "Staff,Manager")]
        [HttpPut("reject/{accomplishmentId}")] // delete soft: ACTIVE > DEACTIVE
        public async Task<ActionResult> RejectAccomplishment([FromRoute] Guid accomplishmentId)
        {
            try
            {
                var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
                var result = await _accomplishmentService.RejectAccomplishment(currentUser.Id, accomplishmentId);
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

        [Authorize(Roles = "Staff,Manager")]
        [HttpDelete("{accomplishmentId}")] // delete hard: DEACTIVE | DRAFTED | PENDING > REMOVE RECORD
        public async Task<ActionResult> DeleteAccomplishment([FromRoute] Guid accomplishmentId)
        {
            try
            {
                var result = await _accomplishmentService.DeleteHardAccomplishment(accomplishmentId);
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

        #endregion
    }
}
