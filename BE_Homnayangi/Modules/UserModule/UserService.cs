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
using Library.PagedList;
using BE_Homnayangi.Modules.UserModule.Request;
using System.Linq.Expressions;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using Library.Models.Constant;
using Microsoft.AspNetCore.Http;
using BE_Homnayangi.Modules.UserModule.Response;
using BE_Homnayangi.Utils;
using BE_Homnayangi.Modules.Utils;

namespace BE_Homnayangi.Modules.UserModule
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly AppSetting _appSettings;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthorization _customAuthorization;
        public UserService(IUserRepository userRepository, ICustomerRepository customerRepository, IOptionsMonitor<AppSetting> optionsMonitor, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICustomAuthorization customAuthorization)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _appSettings = optionsMonitor.CurrentValue;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _customAuthorization = customAuthorization;
        }

        public Task<ICollection<User>> GetUsersBy(
            Expression<Func<User, bool>> filter = null,
            Func<IQueryable<User>, ICollection<User>> options = null,
            string includeProperties = null)
        {
            return _userRepository.GetUsersBy(filter);
        }

        #region CRUD User
        public async Task<PagedResponse<PagedList<User>>> GetAllUser(PagingUserRequest request)
        {

            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;
            var user = await _userRepository.GetUsersBy(x => x.Role == 1, includeProperties: "Blogs");
            var response = PagedList<User>.ToPagedList(source: user, pageNumber: pageNumber, pageSize: pageSize);
            return response.ToPagedResponse();





        }

        public async Task<User> GetUserByEmail(string email)
        {
            User user = await _userRepository.GetFirstOrDefaultAsync(x => x.Email == email); ;
            return user;
        }
        public async Task<User> GetUserById(Guid id)
        {

            User user = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId == id, includeProperties: "Blogs");
            return user;
        }

        public async Task AddNewUser(User newUser)
        {
            User user = await _userRepository.GetFirstOrDefaultAsync(x => x.Username == newUser.Username);
            Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Username == newUser.Username);
            if (user.Username == null && user.Email == null)
            {
                if (customer.Username == null && customer.Email == null)
                {
                    //generate random password
                    string RandomString(int size, bool lowerCase = false)
                    {
                        var builder = new StringBuilder(size);

                        Random _random = new Random();

                        char offset = lowerCase ? 'a' : 'A';
                        const int lettersOffset = 26; // A...Z or a..z: length=26  

                        for (var i = 0; i < size; i++)
                        {
                            var @char = (char)_random.Next(offset, offset + lettersOffset);
                            builder.Append(@char);
                        }
                        return lowerCase ? builder.ToString().ToLower() : builder.ToString();
                    }
                    var sb = new StringBuilder();
                    sb.Append(RandomString(8, true));
                    string pw = sb.ToString();

                    string encryptPassword = EncryptPassword(pw);

                    //Add user
                    newUser.Password = encryptPassword;
                    newUser.CreatedDate = DateTime.Now;
                    newUser.Role = 2;
                    newUser.IsBlocked = false;
                    newUser.IsGoogle = newUser.Email == null ? false : true;



                    await _userRepository.AddAsync(newUser);
                }
            }
        }
        public async Task<bool?> BlockUserById(Guid id)
        {
            bool isBlock = false;
            User user = await _userRepository.GetByIdAsync(id);
            if (user.Role == 2 && user != null)
            {
                if (user.IsBlocked == false)
                {
                    user.IsBlocked = true;
                    await _userRepository.UpdateAsync(user);
                    isBlock = true;

                }
                else if (user.IsBlocked == true)
                {
                    user.IsBlocked = false;
                    await _userRepository.UpdateAsync(user);
                    isBlock = false;
                }
                return isBlock;
            }
            return null;
        }

        public async Task<bool> UpdateStaff(User user)
        {
            bool isUpdated = false;
            User updateUser = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId == user.UserId);
            Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Username == user.Username);
            if (updateUser != null && updateUser.Role != 1)
            {
                if (updateUser.Username != user.Username || updateUser.Email != user.Email)
                {
                    if (customer.Username != user.Username || customer.Email != user.Email)
                    {
                        updateUser.Username = user.Username == "" ? updateUser.Username = null : user.Username;
                        updateUser.Displayname = user.Displayname == "" ? updateUser.Displayname = null : user.Displayname;
                        updateUser.Firstname = user.Firstname == "" ? updateUser.Firstname = null : user.Firstname;
                        updateUser.Lastname = user.Lastname == "" ? updateUser.Lastname = null : user.Lastname;
                        updateUser.Email = user.Email == "" ? updateUser.Email = null : user.Email;
                        updateUser.Phonenumber = user.Phonenumber == "" ? updateUser.Phonenumber = null : user.Phonenumber;
                        updateUser.Avatar = user.Avatar == "" ? updateUser.Avatar = "NoLink" : user.Avatar;
                        updateUser.Gender = user.Gender;
                        updateUser.UpdatedDate = DateTime.Now;
                        await _userRepository.UpdateAsync(updateUser);
                        isUpdated = true;
                    }
                }

            }
            return isUpdated;
        }
        public async Task<bool> UpdateUser(User user)
        {
            bool isUpdated = false;
            User updateUser = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId == user.UserId);
            Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Username == user.Username);
            if (updateUser != null)
            {
                if (updateUser.Username != user.Username || updateUser.Email != user.Email)
                {
                    if (customer.Username != user.Username || customer.Email != user.Email)
                    {
                        updateUser.Username = user.Username == "" ? updateUser.Username = null : user.Username;
                        updateUser.Displayname = user.Displayname == "" ? updateUser.Displayname = null : user.Displayname;
                        updateUser.Firstname = user.Firstname == "" ? updateUser.Firstname = null : user.Firstname;
                        updateUser.Lastname = user.Lastname == "" ? updateUser.Lastname = null : user.Lastname;
                        updateUser.Email = user.Email == "" ? updateUser.Email = null : user.Email;
                        updateUser.Phonenumber = user.Phonenumber == "" ? updateUser.Phonenumber = null : user.Phonenumber;
                        updateUser.Avatar = user.Avatar == "" ? updateUser.Avatar = "NoLink" : user.Avatar;
                        updateUser.Gender = user.Gender;
                        updateUser.UpdatedDate = DateTime.Now;
                        await _userRepository.UpdateAsync(updateUser);
                        isUpdated = true;
                    }
                }
            }
            return isUpdated;
        }
        public async Task UpdateUserPassword(Guid userId, string oldPass, string newPass)
        {
            User user = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId == userId);

            string decryptOldPass = DecryptPassword(user.Password);

            if (oldPass != decryptOldPass)
            {
                throw new Exception("Old password wrong!");
            }

            string encryptNewPass = EncryptPassword(newPass);

            user.Password = encryptNewPass;
            user.UpdatedDate = DateTime.Now;

            await _userRepository.UpdateAsync(user);
        }



        #endregion

        #region CRUD Customer
        public async Task<PagedResponse<PagedList<Customer>>> GetAllCustomer(PagingUserRequest request)
        {
            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;
            var customer = await _customerRepository.GetAll();
            var response = PagedList<Customer>.ToPagedList(source: customer, pageNumber: pageNumber, pageSize: pageSize);
            return response.ToPagedResponse();
        }
        public Customer GetCustomerByEmail(string? email)
        {
            return _customerRepository.GetFirstOrDefaultAsync(x => x.Email == email).Result;
        }
        public Customer GetCustomerByUsername(string? username)
        {
            return _customerRepository.GetFirstOrDefaultAsync(x => x.Username == username).Result;
        }
        public async Task<Customer> GetCustomerById(Guid id)
        {
            Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.CustomerId == id);
            return customer;
        }
        public async Task<Customer> GetCustomerByIdAndusername(Guid? id, string userName)
        {

            Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.CustomerId == id && x.Username == userName);
            return customer;
        }

        public async Task<bool?> BlockCustomerById(Guid id)
        {
            bool isBlock = false;
            Customer customer = await _customerRepository.GetByIdAsync(id);

            if (customer.IsBlocked == false)
            {
                customer.IsBlocked = true;
                await _customerRepository.UpdateAsync(customer);
                isBlock = true;

            }
            else if (customer.IsBlocked == true)
            {
                customer.IsBlocked = false;
                await _customerRepository.UpdateAsync(customer);
                isBlock = false;
            }
            return isBlock;


        }
        public async Task<bool> UpdateCustomer(Customer customer)
        {
            bool isUpdated = false;
            Customer updateCustomer = await _customerRepository.GetFirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId);
            User user = await _userRepository.GetFirstOrDefaultAsync(x => x.Username == customer.Username);
            if (updateCustomer != null)
            {
                if (updateCustomer.Username != customer.Username || updateCustomer.Email != customer.Email)
                {
                    if (customer.Username != user.Username || customer.Email != user.Email)
                    {
                        updateCustomer.Username = customer.Username == "" ? updateCustomer.Username = null : customer.Username;
                        updateCustomer.Displayname = customer.Displayname == "" ? updateCustomer.Displayname = null : customer.Displayname;
                        updateCustomer.Firstname = customer.Firstname == "" ? updateCustomer.Firstname = null : customer.Firstname;
                        updateCustomer.Lastname = customer.Lastname == "" ? updateCustomer.Lastname = null : customer.Lastname;
                        updateCustomer.Email = customer.Email == "" ? updateCustomer.Email = null : customer.Email;
                        updateCustomer.Phonenumber = customer.Phonenumber == "" ? updateCustomer.Phonenumber = null : customer.Phonenumber;
                        updateCustomer.Avatar = customer.Avatar == "" ? updateCustomer.Avatar = "NoLink" : customer.Avatar;
                        updateCustomer.Gender = customer.Gender;
                        updateCustomer.UpdatedDate = DateTime.Now;
                        await _customerRepository.UpdateAsync(updateCustomer);
                        isUpdated = true;
                    }
                }

            }
            return isUpdated;
        }
        public async Task UpdateCustomerPassword(Guid userId, string oldPass, string newPass)
        {
            Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.CustomerId == userId);

            string decryptOldPass = DecryptPassword(customer.Password);

            if (oldPass != decryptOldPass)
            {
                throw new Exception("Old password wrong!");
            }

            string encryptNewPass = EncryptPassword(newPass);

            customer.Password = encryptNewPass;
            customer.UpdatedDate = DateTime.Now;

            await _customerRepository.UpdateAsync(customer);
        }


        #endregion

        #region Authentication

        public async Task<string> GenerateToken(LoginDTO login)
        {
            var user = await _userRepository.GetFirstOrDefaultAsync(x => x.Username == login.Username && x.Password == EncryptPassword(login.Password));
            Console.WriteLine(EncryptPassword(login.Password));
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
                    new Claim("Id", customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.Name, customer.Username),
                    new Claim(ClaimTypes.Email, customer.Email == null ? "" : customer.Email),
                    new Claim("Phone Number", customer.Phonenumber),
                    new Claim("Displayname", customer.Displayname == null ? "" : customer.Displayname),
                    new Claim("Firstname", customer.Firstname),
                    new Claim("Lastname",customer.Lastname),
                    new Claim(ClaimTypes.Gender,customer.Gender.ToString()),
                    new Claim("Avatar", customer.Avatar  == null ? "" : customer.Avatar),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.CUSTOMER)

                }),
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesCustomer), SecurityAlgorithms.HmacSha512Signature)
                    };
                    var principal = new ClaimsPrincipal(tokenDescriptionCustomer.Subject);
                    _httpContextAccessor.HttpContext.User = principal;
                    Console.WriteLine(_httpContextAccessor.HttpContext.User.Identity.Name);
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


                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email == null ? "" : user.Email),
                    new Claim("Phone Number", user.Phonenumber),
                    new Claim("Displayname", user.Displayname == null ? "" : user.Displayname),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Lastname",user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString()),
                    new Claim("Avatar", user.Avatar  == null ? "" : user.Avatar),
                    new Claim(ClaimTypes.Role, user.Role == 1 ? CommonEnum.RoleEnum.MANAGER :  CommonEnum.RoleEnum.STAFF)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

                };
                var principal = new ClaimsPrincipal(tokenDescription.Subject);
                _httpContextAccessor.HttpContext.User = principal;
                var token = jwtTokenHandler.CreateToken(tokenDescription);
                return jwtTokenHandler.WriteToken(token);


            }
            return null;


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

                    new Claim("Id", customer.CustomerId.ToString()),
                    new Claim(ClaimTypes.Name, customer.Username == null ? "": customer.Username),
                    new Claim(ClaimTypes.Email, customer.Email),
                    new Claim("Phone Number", customer.Phonenumber == null ? "": customer.Phonenumber),
                    new Claim("Displayname", customer.Displayname),
                    new Claim("Firstname", customer.Firstname == null ? "": customer.Firstname),
                    new Claim("Lastname",customer.Lastname == null ? "": customer.Lastname),
                    new Claim(ClaimTypes.Gender,customer.Gender.ToString() == null ? "": customer.Gender.ToString()),
                    new Claim("Avatar", customer.Avatar  == null ? "" : customer.Avatar),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.CUSTOMER)

                }),
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesCustomer), SecurityAlgorithms.HmacSha512Signature)
                    };
                    var principal = new ClaimsPrincipal(tokenDescriptionCustomer.Subject);
                    _httpContextAccessor.HttpContext.User = principal;
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


                    new Claim("Id", cus.CustomerId.ToString()),
                    new Claim(ClaimTypes.Name, cus.Username == null ? "": cus.Username),
                    new Claim(ClaimTypes.Email, cus.Email),
                    new Claim("Phone Number", cus.Phonenumber == null ? "": cus.Phonenumber),
                    new Claim("Displayname", cus.Displayname),
                    new Claim("Firstname", cus.Firstname == null ? "": cus.Firstname),
                    new Claim("Lastname",cus.Lastname == null ? "": cus.Lastname),
                    new Claim(ClaimTypes.Gender,cus.Gender.ToString() == null ? "": cus.Gender.ToString()),
                    new Claim("Avatar", cus.Avatar  == null ? "" : cus.Avatar),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.CUSTOMER)

                }),
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesCustomer), SecurityAlgorithms.HmacSha512Signature)
                    };
                    var principal = new ClaimsPrincipal(tokenDescriptionCustomer.Subject);
                    _httpContextAccessor.HttpContext.User = principal;
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

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {

                    new Claim("Id", user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Username == null ? "": user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Phone Number", user.Phonenumber == null ? "": user.Phonenumber),
                    new Claim("Displayname", user.Displayname),
                    new Claim("Firstname", user.Firstname == null ? "": user.Firstname),
                    new Claim("Lastname",user.Lastname == null ? "": user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString() == null ? "": user.Gender.ToString()),
                    new Claim("Avatar", user.Avatar  == null ? "" : user.Avatar),
                    new Claim(ClaimTypes.Role,user.Role == 1 ? CommonEnum.RoleEnum.MANAGER : CommonEnum.RoleEnum.STAFF)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)
                };


                var principal = new ClaimsPrincipal(tokenDescription.Subject);
                _httpContextAccessor.HttpContext.User = principal;

                var token = jwtTokenHandler.CreateToken(tokenDescription);
                return jwtTokenHandler.WriteToken(token);


            }
            return null;
        }
        public async Task Register(RegisterDTO register)
        {
            var cus = GetCustomerByUsername(register.Username);
            if (cus != null) throw new Exception(ErrorMessage.UserError.USER_EXISTED);

            register.Password = EncryptPassword(register.Password);
            var customer = _mapper.Map<Customer>(register);
            customer.CustomerId = Guid.NewGuid();
            customer.IsBlocked = false;
            customer.CreatedDate = DateTime.Now;
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



        #endregion

        public CurrentUserResponse GetCurrentUser(string authHeader)
        {
            try
            {
                string token = authHeader.Split(" ")[1];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                var id = new Guid(tokenS.Claims.First(claim => claim.Type == "Id").Value);
                var Displayname = tokenS.Claims.First(x => x.Type == "Displayname")?.Value;
                var Username = tokenS.Claims.First(x => x.Type == "unique_name")?.Value;
                var Firstname = tokenS.Claims.First(x => x.Type == "Firstname")?.Value;
                var Lastname = tokenS.Claims.First(x => x.Type == "Lastname")?.Value;
                var Email = tokenS.Claims.First(x => x.Type == "email")?.Value;
                var Avatar = tokenS.Claims.First(x => x.Type == "Avatar")?.Value;
                var Phonenumber = tokenS.Claims.First(x => x.Type == "Phone Number")?.Value;
                var Gender = Int32.Parse(tokenS.Claims.First(x => x.Type == "gender")?.Value);
                var Role = tokenS.Claims.First(x => x.Type == "role")?.Value;
                CurrentUserResponse currentUser = new CurrentUserResponse()
                {
                    Id = id,
                    Displayname = Displayname,
                    Username = Username,
                    Firstname = Firstname,
                    Lastname = Lastname,
                    Email = Email,
                    Avatar = Avatar,
                    Phonenumber = Phonenumber,
                    Gender = Gender,
                    Role = Role
                };

                return currentUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetCurrentLoginUser: " + ex.Message);
                throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
            }
        }
    }
}

