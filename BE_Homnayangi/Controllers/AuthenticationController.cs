using AutoMapper;
using BE_Homnayangi.Modules.UserModule.Interface;
using Google.Apis.Auth;
using Library.Commons;
using Library.DataAccess;
using Library.Models;
using Library.Models.DTO.UserDTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BE_Homnayangi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly HomnayangiContext _context;
        private readonly IUserService _userService;


        public AuthenticationController(HomnayangiContext context, IUserService userService, IMapper  mapper )
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }


        // POST api/<LoginController>
        [HttpPost("login")]
        public IActionResult Post([FromBody] LoginDTO login)
        {
            var token = _userService.GenerateToken(login);
            return new JsonResult(new
            {
                token = token
            });
        }
        [HttpPost("login-google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleUserCreateRequest request)
        {
            
            var customer = _mapper.Map<Customer>(request);
            if (customer.Email != request.Email)
            {
                customer.CustomerId = Guid.NewGuid();
                customer.IsGoogle = true;
                await _userService.AddNewCustomer(customer);
            }
           string accessToken = _userService.GenerateGoolgleToken(customer);
            return Ok(accessToken);
        }
    }
}
    


    

