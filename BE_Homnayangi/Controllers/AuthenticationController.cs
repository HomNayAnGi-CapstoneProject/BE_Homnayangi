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


        public AuthenticationController(HomnayangiContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }


        // POST api/<LoginController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var token = await _userService.GenerateToken(login);
                if (token != null && token != "" && token is string)
                {
                    return new JsonResult(new
                    {
                        result = token
                    });
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpPost("login-google")]
        public async Task<IActionResult> GoogleLogin([FromQuery] string token)
        {
            try
            {

                if (token != null && token != "" && token is string)
                {

                    var stream = token;
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(stream);

                    var tokenS = jsonToken as JwtSecurityToken;

                    var displayName = tokenS.Claims.First(claim => claim.Type == "name").Value;
                    var email = tokenS.Claims.First(claim => claim.Type == "email").Value;

                    var loginGoogle = new LoginGoogleDTO
                    {

                        Email = email,
                        Displayname = displayName,

                    };


                    var accessToken = await _userService.GenerateGoolgleToken(loginGoogle);
                    return new JsonResult(new
                    {

                        result = accessToken,
                    });
                }
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {

            try
            {
                var cus = _userService.GetCustomerByUsername(register.Username);
                if (cus == null)
                {
                    await _userService.Register(register);
                    return Ok("Register successful");
                }
                else if (cus.Username == register.Username)
                {
                    return Ok("Account is already existed");
                }
                return Unauthorized();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}





