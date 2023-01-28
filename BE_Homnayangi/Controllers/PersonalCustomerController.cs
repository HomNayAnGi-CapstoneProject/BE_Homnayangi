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
    [Route("api/v1/personal-customer")]
    [ApiController]
    public class PersonalCustomerController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IUserService _userService;


        public PersonalCustomerController(IUserService userService, IMapper mapper)
        {

            _userService = userService;
            _mapper = mapper;
        }

        // GET api/<ValuesController>/5
        [HttpGet]
        public async Task<ActionResult<Customer>> GetUserById()
        {
            Guid id = _userService.GetCurrentLoginUserId(Request.Headers["Authorization"]);
            var result = await _userService.GetCustomerById(id);
            return new JsonResult(new
            {
                result = result,
            });
        }

        //PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCustomer customerUpdate)
        {
            Guid id = _userService.GetCurrentLoginUserId(Request.Headers["Authorization"]);
            if (id != customerUpdate.CustomerId)
            {
                return BadRequest();
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
                return BadRequest(ex.Message);
            }
        }
        //PUT api/<ValuesController>/5
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            Guid id = _userService.GetCurrentLoginUserId(Request.Headers["Authorization"]);
            try
            {


                await _userService.UpdateCustomerPassword(id, request.oldPassword, request.newPassword);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Update Successfully");

        }
    }
}
