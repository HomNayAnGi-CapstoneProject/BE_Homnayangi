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

        public async Task<string> GenerateToken(LoginDTO login)
        {
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Username == login.Username && x.Password == login.Password);
            if (user == null)
            {
                var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Username == login.Username && x.Password == login.Password);

                if (customer != null && customer.IsBlocked == false)
                {
                    var jwtTokenHandlerCustomer = new JwtSecurityTokenHandler();

                    var secretKeyBytesCustomer = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                    var tokenDescriptionCustomer = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
                       new Claim(ClaimTypes.Name, customer.Username),
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim(ClaimTypes.MobilePhone, customer.Phonenumber),
                    new Claim("Displayname", customer.Displayname),
                    new Claim("Firstname", customer.Firstname),
                    new Claim("Latname",customer.Lastname),
                    new Claim(ClaimTypes.Gender,customer.Gender.ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.CUSTOMER)

                }),
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesCustomer), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var tokenCustomer = jwtTokenHandlerCustomer.CreateToken(tokenDescriptionCustomer);

                    return jwtTokenHandlerCustomer.WriteToken(tokenCustomer);
                }

                return null;

            }
            else if (user.IsBlocked == false)
            {

                var jwtTokenHandler = new JwtSecurityTokenHandler();

                var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                if (user.Role == 1)
                {
                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
                     new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.Phonenumber),
                    new Claim("Displayname", user.Displayname),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Latname",user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.MANAGER)
                }),
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

                    };

                    var token = jwtTokenHandler.CreateToken(tokenDescription);

                    return jwtTokenHandler.WriteToken(token);
                }
                else if (user.Role == 2)
                {
                    var tokenDescription = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {

                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.Phonenumber),
                    new Claim("Displayname", user.Displayname),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Latname",user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.STAFF)


                }),
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

                    };

                    var token = jwtTokenHandler.CreateToken(tokenDescription);

                    return jwtTokenHandler.WriteToken(token);
                }
                return null;
            }
                    return null;
                
            
        }
        public async Task AddNewCustomer(Customer newCustomer)
        {
        
            await _customerRepository.AddAsync(newCustomer);
        }
        public Customer GetCustomerByEmail (string? email)
        {
            return _customerRepository.GetFirstOrDefaultAsync(x => x.Email == email).Result;
        }
        public Customer GetCustomerByUsername(string? username)
        {
            return _customerRepository.GetFirstOrDefaultAsync(x => x.Username == username).Result;
        }



        public async Task<string> GenerateGoolgleToken(LoginGoogleDTO loginGoogle)
        {
            
                var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == loginGoogle.Email); ;
                if (user == null)
                {
                    var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Email == loginGoogle.Email);
                    if (customer != null && customer.IsBlocked == false)
                    {
                        var jwtTokenHandlerCustomer = new JwtSecurityTokenHandler();

                        var secretKeyBytesCustomer = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                        var tokenDescriptionCustomer = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[] {
                 
                    new Claim(ClaimTypes.Name, customer.Username),
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim(ClaimTypes.MobilePhone, customer.Phonenumber),
                    new Claim("Displayname", customer.Displayname),
                    new Claim("Firstname", customer.Firstname),
                    new Claim("Latname",customer.Lastname),
                    new Claim(ClaimTypes.Gender,customer.Gender.ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.CUSTOMER)

                }),
                            Expires = DateTime.UtcNow.AddMinutes(60),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesCustomer), SecurityAlgorithms.HmacSha512Signature)
                        };

                        var tokenCustomer = jwtTokenHandlerCustomer.CreateToken(tokenDescriptionCustomer);

                        return jwtTokenHandlerCustomer.WriteToken(tokenCustomer);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (user.IsBlocked == false)
                {

                    var jwtTokenHandler = new JwtSecurityTokenHandler();

                    var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                    if (user.Role == 1)
                    {
                        var tokenDescription = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[] {

                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.Phonenumber),
                    new Claim("Displayname", user.Displayname),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Latname",user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.MANAGER)
                }),
                            Expires = DateTime.UtcNow.AddMinutes(60),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

                        };

                        var token = jwtTokenHandler.CreateToken(tokenDescription);

                        return jwtTokenHandler.WriteToken(token);
                    }
                    else if (user.Role == 2)
                    {
                        var tokenDescription = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[] {

                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.MobilePhone, user.Phonenumber),
                    new Claim("Displayname", user.Displayname),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Latname",user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString()),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.STAFF)

                }),
                            Expires = DateTime.UtcNow.AddMinutes(60),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

                        };

                        var token = jwtTokenHandler.CreateToken(tokenDescription);

                        return jwtTokenHandler.WriteToken(token);
                    }
                    return null;
                }

            return null;
        }
        public async Task Register(RegisterDTO register)
        {
            var customer = _mapper.Map<Customer>(register);
            await _customerRepository.AddAsync(customer);

        }
        
    }
}

