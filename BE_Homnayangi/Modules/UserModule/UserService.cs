using Library.Commons;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.DTO.UserDTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BE_Homnayangi.Modules.UserModule
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly AppSetting _appSettings;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository, ICustomerRepository customerRepository, IOptionsMonitor<AppSetting> optionsMonitor, IMapper mapper)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _appSettings = optionsMonitor.CurrentValue;
            _mapper = mapper;
        }
        public async Task<Customer> GetUserByEmail(string email)
        {
            Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Email == email); ;
            return customer;
        }

        public string GenerateToken(LoginDTO login)
        {
            {
                var user = _userRepository.GetFirstOrDefaultAsync(x => x.Username == login.Username && x.Password == login.Password);
                if (user.Result == null)
                {
                    var customer = _customerRepository.GetFirstOrDefaultAsync(x => x.Username == login.Username && x.Password == login.Password);

                    var jwtTokenHandlerCustomer = new JwtSecurityTokenHandler();

                    var secretKeyBytesCustomer = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                    var tokenDescriptionCustomer = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, customer.Result.Username),
                    new Claim(ClaimTypes.Email, customer.Result.Email),
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.CUSTOMER)

                }),
                        Expires = DateTime.UtcNow.AddMinutes(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesCustomer), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var tokenCustomer = jwtTokenHandlerCustomer.CreateToken(tokenDescriptionCustomer);

                    return jwtTokenHandlerCustomer.WriteToken(tokenCustomer);
                }
                else
                {

                    var jwtTokenHandler = new JwtSecurityTokenHandler();

                    var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                    if (user.Result.Role == 1)
                    {
                        var tokenDescription = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Result.Username),
                    new Claim(ClaimTypes.Email, user.Result.Email),
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.MANAGER)
                }),
                            Expires = DateTime.UtcNow.AddMinutes(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

                        };

                        var token = jwtTokenHandler.CreateToken(tokenDescription);

                        return jwtTokenHandler.WriteToken(token);
                    }
                    else if (user.Result.Role == 2)
                    {
                        var tokenDescription = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Result.Username),
                    new Claim(ClaimTypes.Email, user.Result.Email),
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.STAFF)

                }),
                            Expires = DateTime.UtcNow.AddMinutes(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

                        };

                        var token = jwtTokenHandler.CreateToken(tokenDescription);

                        return jwtTokenHandler.WriteToken(token);
                    }
                    return null;
                }
            }
        }
        public async Task AddNewCustomer(Customer newCustomer)
        {
        
            await _customerRepository.AddAsync(newCustomer);
        }

        public string GenerateGoolgleToken(Customer customer)
        {

            var jwtTokenHandlerCustomer = new JwtSecurityTokenHandler();

            var secretKeyBytesCustomer = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescriptionCustomer = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim("TokenId", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.CUSTOMER)

                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesCustomer), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenCustomer = jwtTokenHandlerCustomer.CreateToken(tokenDescriptionCustomer);

            return jwtTokenHandlerCustomer.WriteToken(tokenCustomer);

        }
    }
}

