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
using System.Security.Cryptography;
using System.IO;

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
            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Username == login.Username && x.Password == EncryptPassword(login.Password));
            if (user == null)
            {
                var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Username == login.Username && x.Password == EncryptPassword(login.Password));

                if (customer != null && customer.IsBlocked == false)
                {
                    var jwtTokenHandlerCustomer = new JwtSecurityTokenHandler();

                    var secretKeyBytesCustomer = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                    var tokenDescriptionCustomer = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, customer.Username),
                    new Claim(ClaimTypes.Email, customer.Email == null ? "" : customer.Email),
                    new Claim("Phone Number", customer.Phonenumber),
                    new Claim("Displayname", customer.Displayname == null ? "" : user.Displayname),
                    new Claim("Firstname", customer.Firstname),
                    new Claim("Latname",customer.Lastname),
                    new Claim(ClaimTypes.Gender,customer.Gender.ToString()),
                    new Claim("Avatar", customer.Avatar  == null ? "" : customer.Avatar),
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
                    new Claim(ClaimTypes.Email, user.Email == null ? "" : user.Email),
                    new Claim("Phone Number", user.Phonenumber),
                    new Claim("Displayname", user.Displayname == null ? "" : user.Displayname),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Latname",user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString()),
                    new Claim("Avatar", user.Avatar  == null ? "" : user.Avatar),
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
                    new Claim(ClaimTypes.Email, user.Email == null ? "" : user.Email),
                    new Claim("Phone Number", user.Phonenumber),
                    new Claim("Displayname", user.Displayname == null ? "" : user.Displayname),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Latname",user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString()),
                    new Claim("Avatar", user.Avatar  == null ? "" : user.Avatar),
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
        public Customer GetCustomerByEmail(string? email)
        {
            return _customerRepository.GetFirstOrDefaultAsync(x => x.Email == email).Result;
        }
        public Customer GetCustomerByUsername(string? username)
        {
            return _customerRepository.GetFirstOrDefaultAsync(x => x.Username == username).Result;
        }



        public async Task<string> GenerateGoolgleToken(LoginGoogleDTO loginGoogle)
        {

            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == loginGoogle.Email);
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


                    new Claim(ClaimTypes.Name, customer.Username == null ? "": customer.Username),
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim("Phone Number", customer.Phonenumber == null ? "": customer.Phonenumber),
                    new Claim("Displayname", customer.Displayname),
                    new Claim("Firstname", customer.Firstname == null ? "": customer.Firstname),
                    new Claim("Latname",customer.Lastname == null ? "": customer.Lastname),
                    new Claim(ClaimTypes.Gender,customer.Gender.ToString() == null ? "": customer.Gender.ToString()),
                    new Claim("Avatar", customer.Avatar  == null ? "" : customer.Avatar),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.CUSTOMER)

                }),
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesCustomer), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var tokenCustomer = jwtTokenHandlerCustomer.CreateToken(tokenDescriptionCustomer);

                    return jwtTokenHandlerCustomer.WriteToken(tokenCustomer);
                }
                else if (customer == null)
                {
                    var cus = _mapper.Map<Customer>(loginGoogle);
                    cus.CustomerId = Guid.NewGuid();
                    cus.IsBlocked = false;
                    cus.IsGoogle = true;
                    await _customerRepository.AddAsync(cus);
                    var jwtTokenHandlerCustomer = new JwtSecurityTokenHandler();

                    var secretKeyBytesCustomer = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                    var tokenDescriptionCustomer = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {



                    new Claim(ClaimTypes.Name, cus.Username == null ? "": cus.Username),
                    new Claim(ClaimTypes.Email, cus.Email),
                    new Claim("Phone Number", cus.Phonenumber == null ? "": cus.Phonenumber),
                    new Claim("Displayname", cus.Displayname),
                    new Claim("Firstname", cus.Firstname == null ? "": cus.Firstname),
                    new Claim("Latname",cus.Lastname == null ? "": cus.Lastname),
                    new Claim(ClaimTypes.Gender,cus.Gender.ToString() == null ? "": cus.Gender.ToString()),
                    new Claim("Avatar", cus.Avatar  == null ? "" : cus.Avatar),
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


                    new Claim(ClaimTypes.Name, user.Username == null ? "": user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Phone Number", user.Phonenumber == null ? "": user.Phonenumber),
                    new Claim("Displayname", user.Displayname),
                    new Claim("Firstname", user.Firstname == null ? "": user.Firstname),
                    new Claim("Latname",user.Lastname == null ? "": user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString() == null ? "": user.Gender.ToString()),
                    new Claim("Avatar", user.Avatar  == null ? "" : user.Avatar),
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
                    new Claim("Avatar", user.Avatar  == null ? "" : user.Avatar),
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
            register.Password = EncryptPassword(register.Password);
            var customer = _mapper.Map<Customer>(register);
            customer.CustomerId = Guid.NewGuid();
            customer.IsBlocked = false;
            customer.CreatedDate = DateTime.UtcNow;
            await _customerRepository.AddAsync(customer);

        }

        public string EncryptPassword(string plainText)
        {
            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string DecryptPassword(string plainText)
        {
            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(plainText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}

