using AutoMapper;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.UserModule.Request;
using BE_Homnayangi.Modules.UserModule.Response;
using BE_Homnayangi.Modules.Utils;
using Library.Commons;
using Library.Models;
using Library.Models.Constant;
using Library.Models.DTO.UserDTO;
using Library.Models.Enum;
using Library.PagedList;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly AppSetting _appSettings;
        private readonly AdministratorAccount _admin;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomAuthorization _customAuthorization;
        public UserService(IUserRepository userRepository, ICustomerRepository customerRepository, IOptionsMonitor<AppSetting> optionsMonitor, IOptionsMonitor<AdministratorAccount> admin, IMapper mapper, IHttpContextAccessor httpContextAccessor, ICustomAuthorization customAuthorization)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
            _appSettings = optionsMonitor.CurrentValue;
            _admin = admin.CurrentValue;
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

        public User GetUserByUsername(string? username)
        {
            return _userRepository.GetFirstOrDefaultAsync(x => x.Username == username).Result;
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
                    //Add user
                    newUser.Password = GenerateEncryptRandomPassword();
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
            try
            {
                User updateUser = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId == user.UserId);
                if (updateUser.Role != 2)
                    throw new Exception(ErrorMessage.StaffError.STAFF_NOT_EXIST);

                Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Username == user.Username);
                if (updateUser != null && updateUser.Role != 1)
                {
                    if (user.Username != null)
                        await CheckExistedUsername(user.UserId, user.Username);
                    if (user.Email != null)
                        await CheckExistedEmail(user.UserId, user.Email);
                    updateUser.Username = user.Username == null || user.Username == "" ? updateUser.Username : user.Username;
                    updateUser.Displayname = user.Displayname == null || user.Displayname == "" ? updateUser.Displayname : user.Displayname;
                    updateUser.Firstname = user.Firstname == null || user.Firstname == "" ? updateUser.Firstname : user.Firstname;
                    updateUser.Lastname = user.Lastname == null || user.Lastname == "" ? updateUser.Lastname : user.Lastname;
                    updateUser.Email = user.Email == null || user.Email == "" ? updateUser.Email : user.Email;
                    updateUser.Phonenumber = user.Phonenumber == null || user.Phonenumber == "" ? updateUser.Phonenumber : user.Phonenumber;
                    updateUser.Avatar = user.Avatar == null || user.Avatar == "" ? updateUser.Avatar : user.Avatar;
                    updateUser.Gender = user.Gender == null ? updateUser.Gender : user.Gender;
                    updateUser.UpdatedDate = DateTime.Now;
                    await _userRepository.UpdateAsync(updateUser);
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateStaff: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isUpdated;
        }

        public async Task<bool> UpdateUser(User user)
        {
            bool isUpdated = false;
            try
            {
                User updateUser = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId == user.UserId);
                Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Username == user.Username);
                if (updateUser != null)
                {
                    if (user.Username != null)
                        await CheckExistedUsername(user.UserId, user.Username);
                    if (user.Email != null)
                        await CheckExistedEmail(user.UserId, user.Email);

                    updateUser.Username = user.Username == null || user.Username == "" ? updateUser.Username : user.Username;
                    updateUser.Displayname = user.Displayname == null || user.Displayname == "" ? updateUser.Displayname : user.Displayname;
                    updateUser.Firstname = user.Firstname == null || user.Firstname == "" ? updateUser.Firstname : user.Firstname;
                    updateUser.Lastname = user.Lastname == null || user.Lastname == "" ? updateUser.Lastname : user.Lastname;
                    updateUser.Email = user.Email == null || user.Email == "" ? updateUser.Email : user.Email;
                    updateUser.Phonenumber = user.Phonenumber == null || user.Phonenumber == "" ? updateUser.Phonenumber : user.Phonenumber;
                    updateUser.Avatar = user.Avatar == null || user.Avatar == "" ? updateUser.Avatar = "NoLink" : user.Avatar;
                    updateUser.Gender = user.Gender == null ? updateUser.Gender : user.Gender;
                    updateUser.UpdatedDate = DateTime.Now;

                    await _userRepository.UpdateAsync(updateUser);
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateUser: " + ex.Message);
                throw new Exception(ex.Message);
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

        // Admin's actions

        public async Task<ICollection<CurrentUserResponse>> GetUserByRole(string role)
        {
            try
            {
                int roleNumber = ConvertRole(role);
                var users = await _userRepository.GetUsersBy(u => u.Role == roleNumber);
                var result = users.Select(m => new CurrentUserResponse()
                {
                    Id = m.UserId,
                    Displayname = m.Displayname,
                    Username = m.Username,
                    Firstname = m.Firstname,
                    Lastname = m.Lastname,
                    Email = m.Email,
                    Phonenumber = m.Phonenumber,
                    Gender = m.Gender,
                    Avatar = m.Avatar,
                    Role = CommonEnum.RoleEnum.MANAGER,
                    Status = m.IsBlocked.Value,
                }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetUserByRole: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ChangeStatusManagerByAdmin(UpdatedStatusManager request)
        {
            bool isUpdated = false;
            try
            {
                var manager = await _userRepository.GetFirstOrDefaultAsync(m => m.UserId == request.ManagerId && m.Role == 1);
                if (manager == null)
                    throw new Exception(ErrorMessage.ManagerError.MANAGER_NOT_FOUND);
                manager.IsBlocked = request.IsBlocked;
                await _userRepository.UpdateAsync(manager);
                isUpdated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UnblockManagerByAdmin: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isUpdated;
        }

        public async Task<bool> CreateANewManager(CreateManager request)
        {
            bool isInserted = false;
            try
            {

                var users = await _userRepository.GetAll();
                var customers = await _customerRepository.GetAll();

                string username = request.Username.Trim();
                string email = request.Email.Trim();
                string phone = request.Phonenumber.Trim();

                // Check duplicated data or not? (username, email, password)
                IsDuplicatedUsername(username, users, customers);
                IsDuplicatedEmail(email, users, customers);
                IsDuplicatedPhone(phone, users, customers);

                User manager = new User()
                {
                    UserId = Guid.NewGuid(),
                    Displayname = request.Firstname + " " + request.Lastname,
                    Username = username,
                    Firstname = request.Firstname.Trim(),
                    Lastname = request.Lastname.Trim(),
                    Email = email,
                    Password = GenerateEncryptRandomPassword(),
                    Phonenumber = phone,
                    Gender = request.Gender,
                    Avatar = null,
                    Role = 1,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsBlocked = false,
                    IsGoogle = true
                };
                await _userRepository.AddAsync(manager);
                isInserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateANewManager: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isInserted;
        }

        private void IsDuplicatedUsername(string username, ICollection<User> users, ICollection<Customer> customers)
        {
            try
            {
                var userNumber = users.Where(u => u.Username != null && u.Username.Equals(username)).Count();
                var customerNumber = customers.Where(u => u.Username != null && u.Username.Equals(username)).Count();
                if (userNumber > 0 || customerNumber > 0)
                    throw new Exception(ErrorMessage.UserError.USERNAME_EXISTED);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at IsDuplicatedUsername: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private void IsDuplicatedEmail(string email, ICollection<User> users, ICollection<Customer> customers)
        {
            try
            {
                var userNumber = users.Where(u => u.Email != null && u.Email.Equals(email)).Count();
                var customerNumber = customers.Where(u => u.Email != null && u.Email.Equals(email)).Count();
                if (userNumber > 0 || customerNumber > 0)
                    throw new Exception(ErrorMessage.UserError.EMAIL_EXISTED);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at IsDuplicatedEmail: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private void IsDuplicatedPhone(string phone, ICollection<User> users, ICollection<Customer> customers)
        {
            try
            {
                var userNumber = users.Where(u => u.Phonenumber != null && u.Phonenumber.Equals(phone)).Count();
                var customerNumber = customers.Where(u => u.Phonenumber != null && u.Phonenumber.Equals(phone)).Count();
                if (userNumber > 0 || customerNumber > 0)
                    throw new Exception(ErrorMessage.UserError.PHONE_EXISTED);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at IsDuplicatedPhone: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private string GenerateEncryptRandomPassword()
        {
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

            return EncryptPassword(sb.ToString());
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
            Customer customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.CustomerId == id, includeProperties: "Orders,Accomplishments");
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
            try
            {
                Customer updateCustomer = await _customerRepository.GetFirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId);
                User user = await _userRepository.GetFirstOrDefaultAsync(x => x.Username == customer.Username);
                if (updateCustomer != null)
                {
                    if (customer.Username != null)
                        await CheckExistedUsername(customer.CustomerId, customer.Username);

                    if (customer.Email != null)
                        await CheckExistedEmail(customer.CustomerId, customer.Email);

                    updateCustomer.Username = customer.Username == null || customer.Username == "" ? updateCustomer.Username : customer.Username;
                    updateCustomer.Displayname = customer.Displayname == null || customer.Displayname == "" ? updateCustomer.Displayname : customer.Displayname;
                    updateCustomer.Firstname = customer.Firstname == null || customer.Firstname == "" ? updateCustomer.Firstname : customer.Firstname;
                    updateCustomer.Lastname = customer.Lastname == null || customer.Lastname == "" ? updateCustomer.Lastname : customer.Lastname;
                    updateCustomer.Email = customer.Email == null || customer.Email == "" ? updateCustomer.Email : customer.Email;
                    updateCustomer.Phonenumber = customer.Phonenumber == null || customer.Phonenumber == "" ? updateCustomer.Phonenumber : customer.Phonenumber;
                    updateCustomer.Avatar = customer.Avatar == null || customer.Avatar == "" ? updateCustomer.Avatar : customer.Avatar;
                    updateCustomer.Gender = customer.Gender == null ? updateCustomer.Gender : customer.Gender;
                    updateCustomer.UpdatedDate = DateTime.Now;
                    await _customerRepository.UpdateAsync(updateCustomer);
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateCustomer: " + ex.Message);
                throw new Exception(ex.Message);
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
            var admin = _admin;
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
                    new Claim("PhoneNumber", customer.Phonenumber),
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
                else if (login.Username == admin.Username && EncryptPassword(login.Password) == admin.Password)
                {


                    if (admin != null)
                    {
                        var jwtTokenHandlerAdmin = new JwtSecurityTokenHandler();

                        var secretKeyBytesAdmin = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                        var tokenDescriptionAdmin = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new[] {
                                    new Claim("Id", admin.Id.ToString()),
                                    new Claim(ClaimTypes.Name, admin.Username),
                                    new Claim("PhoneNumber", ""),
                                    new Claim("Displayname", ""),
                                    new Claim("Firstname", ""),
                                    new Claim("Lastname",""),
                                    new Claim(ClaimTypes.Gender,""),
                                    new Claim(ClaimTypes.Email, admin.Email),
                                    new Claim("Avatar", ""),
                                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.ADMIN)
                        }),

                            Expires = DateTime.UtcNow.AddMinutes(60),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesAdmin), SecurityAlgorithms.HmacSha512Signature)
                        };

                        var principal = new ClaimsPrincipal(tokenDescriptionAdmin.Subject);
                        _httpContextAccessor.HttpContext.User = principal;
                        Console.WriteLine(_httpContextAccessor.HttpContext.User.Identity.Name);
                        var tokenAdmin = jwtTokenHandlerAdmin.CreateToken(tokenDescriptionAdmin);
                        return jwtTokenHandlerAdmin.WriteToken(tokenAdmin);
                    }
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
                    new Claim("PhoneNumber", user.Phonenumber),
                    new Claim("Displayname", user.Displayname == null ? "" : user.Displayname),
                    new Claim("Firstname", user.Firstname),
                    new Claim("Lastname",user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString()),
                    new Claim("Avatar", user.Avatar  == null ? "" : user.Avatar),
                    new Claim(ClaimTypes.Role, user.Role == 1 ? CommonEnum.RoleEnum.MANAGER : CommonEnum.RoleEnum.STAFF )
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
            var admin = _admin;
            if (user == null)
            {
                if (admin.Email == loginGoogle.Email)
                {
                    var jwtTokenHandlerAdmin = new JwtSecurityTokenHandler();

                    var secretKeyBytesAdmin = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

                    var tokenDescriptionAdmin = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", admin.Id.ToString()),
                    new Claim(ClaimTypes.Name, admin.Username),
                    new Claim("PhoneNumber", ""),
                    new Claim("Displayname", ""),
                    new Claim("Firstname", ""),
                    new Claim("Lastname",""),
                    new Claim(ClaimTypes.Gender,""),
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim("Avatar", ""),
                    new Claim(ClaimTypes.Role, CommonEnum.RoleEnum.ADMIN)

                }),

                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytesAdmin), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var principal = new ClaimsPrincipal(tokenDescriptionAdmin.Subject);
                    _httpContextAccessor.HttpContext.User = principal;
                    Console.WriteLine(_httpContextAccessor.HttpContext.User.Identity.Name);
                    var tokenAdmin = jwtTokenHandlerAdmin.CreateToken(tokenDescriptionAdmin);
                    return jwtTokenHandlerAdmin.WriteToken(tokenAdmin);
                }
                else
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
                    new Claim("PhoneNumber", customer.Phonenumber == null ? "": customer.Phonenumber),
                    new Claim("Displayname", customer.Displayname),
                    new Claim("Firstname", customer.Firstname == null ? "": customer.Firstname),
                    new Claim("Lastname",customer.Lastname == null ? "": customer.Lastname),
                    new Claim(ClaimTypes.Gender,customer.Gender.ToString() == null ? "": customer.Gender.ToString()),
                    new Claim("isGoogleAccount", customer.IsGoogle.ToString()  == null ? "" : customer.IsGoogle.ToString()),
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
                    new Claim("PhoneNumber", cus.Phonenumber == null ? "": cus.Phonenumber),
                    new Claim("Displayname", cus.Displayname),
                    new Claim("Firstname", cus.Firstname == null ? "": cus.Firstname),
                    new Claim("Lastname",cus.Lastname == null ? "": cus.Lastname),
                    new Claim(ClaimTypes.Gender,cus.Gender.ToString() == null ? "": cus.Gender.ToString()),
                    new Claim("isGoogleAccount", cus.IsGoogle.ToString()  == null ? "" : cus.IsGoogle.ToString()),
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
                    new Claim("PhoneNumber", user.Phonenumber == null ? "": user.Phonenumber),
                    new Claim("Displayname", user.Displayname),
                    new Claim("Firstname", user.Firstname == null ? "": user.Firstname),
                    new Claim("Lastname",user.Lastname == null ? "": user.Lastname),
                    new Claim(ClaimTypes.Gender,user.Gender.ToString() == null ? "": user.Gender.ToString()),
                    new Claim("isGoogleAccount", user.IsGoogle.ToString()  == null ? "" : user.IsGoogle.ToString()),
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
            var admin = _admin;
            var user = GetUserByUsername(register.Username);
            if (cus != null || admin.Username == register.Username || user != null)
                throw new Exception(ErrorMessage.UserError.USERNAME_EXISTED);

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
                if (authHeader != null)
                {
                    string token = authHeader.Split(" ")[1];
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    var id = new Guid(tokenS.Claims.First(claim => claim.Type == "Id")?.Value);
                    var Displayname = tokenS.Claims.First(x => x.Type == "Displayname")?.Value;
                    var Username = tokenS.Claims.First(x => x.Type == "unique_name")?.Value;
                    var Firstname = tokenS.Claims.First(x => x.Type == "Firstname")?.Value;
                    var Lastname = tokenS.Claims.First(x => x.Type == "Lastname")?.Value;
                    var Email = tokenS.Claims.First(x => x.Type == "email")?.Value;
                    var Avatar = tokenS.Claims.First(x => x.Type == "Avatar")?.Value;
                    var Phonenumber = tokenS.Claims.First(x => x.Type == "PhoneNumber")?.Value;
                    var Gender = Int32.Parse(tokenS.Claims.First(x => x.Type == "gender")?.Value == "" ? ((int)GenderEnum.Gender.MALE).ToString() : tokenS.Claims.First(x => x.Type == "gender")?.Value);
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
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetCurrentLoginUser: " + ex.Message);
                throw new Exception(ErrorMessage.UserError.USER_NOT_LOGIN);
            }
        }

        private async Task CheckExistedUsername(Guid id, string username)
        {
            try
            {
                var customer = await _customerRepository.GetFirstOrDefaultAsync(c => c.CustomerId != id && c.Username.Equals(username) && !c.IsBlocked.Value);
                var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserId != id && u.Username.Equals(username) && !u.IsBlocked.Value);
                if (customer != null)
                {
                    throw new Exception(ErrorMessage.CustomerError.USERNAME_EXISTED);
                }
                else if (user != null)
                {
                    throw new Exception(ErrorMessage.UserError.USERNAME_EXISTED);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Username: " + username);
                Console.WriteLine("Error at CheckExistedUsername: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private async Task CheckExistedEmail(Guid id, string email)
        {
            try
            {
                var customer = await _customerRepository.GetFirstOrDefaultAsync(c => c.CustomerId != id && c.Email.Equals(email) && !c.IsBlocked.Value);
                var user = await _userRepository.GetFirstOrDefaultAsync(u => u.UserId != id && u.Email.Equals(email) && !u.IsBlocked.Value);
                if (customer != null)
                {
                    throw new Exception(ErrorMessage.CustomerError.EMAIL_EXISTED);
                }
                else if (user != null)
                {
                    throw new Exception(ErrorMessage.UserError.EMAIL_EXISTED);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Email: " + email);
                Console.WriteLine("Error at CheckExistedEmail: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private int ConvertRole(string role)
        {
            int result = -1;
            switch (role)
            {
                case CommonEnum.RoleEnum.CUSTOMER:
                    {
                        result = 0;
                        break;
                    }
                case CommonEnum.RoleEnum.MANAGER:
                    {
                        result = 1;
                        break;
                    }
                case CommonEnum.RoleEnum.STAFF:
                    {
                        result = 2;
                        break;
                    }
                case CommonEnum.RoleEnum.ADMIN:
                    {
                        result = 3;
                        break;
                    }
            }
            return result;
        }
    }
}

