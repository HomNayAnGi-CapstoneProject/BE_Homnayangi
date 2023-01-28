using AutoMapper;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.UserModule.Request;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/personal-user")]
    [ApiController]
    public class PersonalUsersController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IUserService _userService;


        public PersonalUsersController(IUserService userService, IMapper mapper)
        {

            _userService = userService;
            _mapper = mapper;
        }

        // GET api/<ValuesController>/5
        [HttpGet]
        public async Task<ActionResult<User>> GetUserById()
        {
            Guid id = _userService.GetCurrentLoginUserId(Request.Headers["Authorization"]);
            var result = await _userService.GetUserById(id);
            return new JsonResult(new
            {
                result = result,
            });
        }





        //PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUser updateUser)
        {
            Guid id = _userService.GetCurrentLoginUserId(Request.Headers["Authorization"]);
            if (id != updateUser.UserId)
            {
                return BadRequest();
            }
            try
            {

                var user = _mapper.Map<User>(updateUser);
                bool isUpdated = await _userService.UpdateUser(user);
                if (isUpdated == true)
                {
                    return Ok("Update Successfully");
                }
                else
                {
                    return BadRequest("Update Fail");
                }

            }
            catch
            {
                return BadRequest();
            }


        }
        //PUT api/<ValuesController>/5
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            Guid id = _userService.GetCurrentLoginUserId(Request.Headers["Authorization"]);
            try
            {


                await _userService.UpdateUserPassword(id, request.oldPassword, request.newPassword);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Update Successfully");

        }
    }
}
