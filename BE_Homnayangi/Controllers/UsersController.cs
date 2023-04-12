using AutoMapper;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.UserModule.Request;
using Library.Models;
using Library.PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IUserService _userService;


        public UsersController(IUserService userService, IMapper mapper)
        {

            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/<ValuesController>
        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            try
            {
                var result = await _userService.GetUserByRole("Staff");
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


        // GET api/<ValuesController>/5
        [Authorize(Roles = "Manager")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById([FromRoute] Guid id)
        {
            var result = await _userService.GetUserById(id);
            return new JsonResult(new
            {
                result = result,
            });
        }

        private async Task<bool> UserExists(Guid id)
        {
            return await _userService.GetUserById(id) != null;
        }

        // POST api/<ValuesController>
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUser createUser)
        {
            var user = _mapper.Map<User>(createUser);
            user.UserId = Guid.NewGuid();

            /*    user.Password = Membership.GeneratePassword(12, 1);*/
            try
            {
                if (await UserExists(user.UserId))
                {
                    return new JsonResult(new
                    {
                        status = "failed",
                        msg = "User is existed"
                    });
                }
                else
                {
                    await _userService.AddNewUser(user);
                    return Ok("Create staff succcessfully");
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

        //PUT api/<ValuesController>/5
        [Authorize(Roles = "Manager")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUser updateUser)
        {
            try
            {
                var user = _mapper.Map<User>(updateUser);
                bool isUpdated = await _userService.UpdateStaff(user);
                if (isUpdated == true)
                {
                    return Ok("Update Staff Successfully");
                }
                return new JsonResult(new
                {
                    status = "failed",
                    msg = "Update Staff Fail"
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

        //DELETE api/<ValuesController>/5
        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user.IsBlocked == false)
            {
                user.IsBlocked = (bool)await _userService.BlockUserById(id);
                return new JsonResult(new
                {
                    status = "staff is blocked"
                });
            }
            return new JsonResult(new
            {
                status = "failed"
            });
            ;
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UnBlockStaff([FromRoute] Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user.IsBlocked == true)
            {
                user.IsBlocked = (bool)await _userService.BlockUserById(id);
                return new JsonResult(new
                {
                    status = "staff is unblocked"
                });
            }
            return new JsonResult(new
            {
                status = "failed"
            });
            ;
        }

        #region Admin's actions

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-manage/users")] // Get all Staff || Manager ACTIVE
        public async Task<IActionResult> GetAllUsersByAdmin()
        {
            try
            {
                var result = await _userService.GetAllUsers();
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

        [Authorize(Roles = "Admin")]
        [HttpPut("admin-manage/update-role")]
        public async Task<IActionResult> UpdateRole(UpdatedUserRole request)
        {
            try
            {
                var result = await _userService.UpdateRoleUser(request);
                return new JsonResult(new
                {
                    status = result ? "success" : "failed"
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

        //[Authorize(Roles = "Admin")]
        //[HttpPut("admin-manage/status")]
        //public async Task<IActionResult> UnBlockManagerByAdmin([FromBody] UpdatedStatusManager request)
        //{
        //    try
        //    {
        //        var result = await _userService.ChangeStatusManagerByAdmin(request);
        //        if (result)
        //        {
        //            return new JsonResult(new
        //            {
        //                status = "success"
        //            });
        //        }
        //        else
        //        {
        //            return new JsonResult(new
        //            {
        //                status = "failed"
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new
        //        {
        //            status = "failed",
        //            msg = ex.Message
        //        });
        //    }
        //}

        //[Authorize(Roles = "Admin")]
        //[HttpPost("admin-manage")]
        //public async Task<IActionResult> CreateANewManager([FromBody] CreateManager request)
        //{
        //    try
        //    {
        //        var result = await _userService.CreateANewManager(request);
        //        if (result)
        //        {
        //            return new JsonResult(new
        //            {
        //                status = "success"
        //            });
        //        }
        //        else
        //        {
        //            return new JsonResult(new
        //            {
        //                status = "failed"
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new
        //        {
        //            status = "failed",
        //            msg = ex.Message
        //        });
        //    }
        //}

        #endregion
    }
}
