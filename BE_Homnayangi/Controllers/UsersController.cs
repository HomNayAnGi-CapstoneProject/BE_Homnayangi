﻿using AutoMapper;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.UserModule.Request;
using Library.DataAccess;
using Library.Models;
using Library.PagedList;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet]
        public async Task<ActionResult<PagedResponse<PagedList<User>>>> GetAllUsers([FromQuery] PagingUserRequest request)
        {
            var response = await _userService.GetAllUser(request);
            return Ok(new
            {
                result = response
            });
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
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
                    return BadRequest("User is existed");
                }
                else
                {
                    await _userService.AddNewUser(user);
                }

            }
            catch
            {
                return BadRequest();
            }
            return Ok("Create staff succcessfully");
        }


        //PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateUser updateUser)
        {
            if (id != updateUser.UserId)
            {
                return BadRequest();
            }
            try
            {

                var user = _mapper.Map<User>(updateUser);
                bool isUpdated = await _userService.UpdateStaff(user);
                if (isUpdated == true)
                {
                    return Ok("Update Staff Successfully");
                }
                return BadRequest("Update Staff Fail");
            }
            catch
            {
                return BadRequest();
            }

        }
        //DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool? isBlocked = await _userService.BlockUserById(id);
            if (isBlocked == true)
            {
                return new JsonResult(new
                {
                    status = "staff is blocked"
                });
            }
            else if (isBlocked == false)
            {
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
    }
}
