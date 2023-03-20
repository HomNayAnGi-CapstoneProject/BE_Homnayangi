﻿using AutoMapper;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.UserModule.Request;
using BE_Homnayangi.Modules.UserModule.Response;
using BE_Homnayangi.Modules.Utils;
using Library.Models;
using Library.Models.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/personal-customer")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class PersonalCustomerController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IUserService _userService;
        private readonly ICustomAuthorization _customAuthorization;
        public PersonalCustomerController(IUserService userService, IMapper mapper, ICustomAuthorization customAuthorization)
        {

            _userService = userService;
            _mapper = mapper;
            _customAuthorization = customAuthorization;
        }

        // GET api/<ValuesController>/5
        [HttpGet]
        public async Task<ActionResult<CurrentUserResponse>> GetUserById()
        {
            var currentUserId = _userService.GetCurrentUser(Request.Headers["Authorization"]).Id;
            var currentUser = await _userService.GetCustomerById(currentUserId);
            return new JsonResult(new
            {
                result = currentUser
            });
        }

        //PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCustomer customerUpdate)
        {
            var currentUser = _userService.GetCurrentUser(Request.Headers["Authorization"]);
            if (currentUser.Id != customerUpdate.CustomerId)
            {
                return BadRequest(ErrorMessage.CustomerError.NOT_OWNER);
            }
            try
            {
                var user = _mapper.Map<Customer>(customerUpdate);
                bool isUpdated = await _userService.UpdateCustomer(user);
                if (isUpdated == true)
                {
                    return Ok("Update Successfully");
                }
                else
                {
                    return BadRequest("Update Fail");
                }

            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    message = ex.Message
                });
            }
        }
        //PUT api/<ValuesController>/5
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {

            try
            {
                await _userService.UpdateCustomerPassword(_userService.GetCurrentUser(Request.Headers["Authorization"]).Id, request.oldPassword, request.newPassword);
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = "failed",
                    message = ex.Message
                });
            }
            return Ok("Update Successfully");

        }
    }
}
