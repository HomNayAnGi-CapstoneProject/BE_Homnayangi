using AutoMapper;
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
    public class CustomersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public CustomersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<PagedResponse<PagedList<Customer>>>> GetAllUsers([FromQuery] PagingUserRequest request)
        {
            var response = await _userService.GetAllCustomer(request);
            return Ok(new
            {
                result = response
            });
        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetCustomerById(Guid id)
        {
            var result = await _userService.GetCustomerById(id);
            return new JsonResult(new
            {
                result = result,
            });
        }
        //DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool? isBlocked = await _userService.BlockCustomerById(id);
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