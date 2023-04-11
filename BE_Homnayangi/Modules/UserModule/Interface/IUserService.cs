using BE_Homnayangi.Modules.UserModule.Request;
using BE_Homnayangi.Modules.UserModule.Response;
using Library.Models;
using Library.Models.DTO.UserDTO;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Interface
{
#nullable enable
    public interface IUserService
    {
        #region CRUD staff
        public Task<User> GetUserByEmail(string email);
        public Task<CurrentUserResponse> GetUserById(Guid id);
        public Task AddNewUser(User newUser);
        public Task<bool?> BlockUserById(Guid id);
        public Task<bool> UpdateStaff(User user);
        public Task<bool> UpdateUser(User user);
        public Task UpdateUserPassword(Guid userId, string oldPass, string newPass);
        #endregion

        #region CRUD Customer

        public Task<ICollection<CurrentUserResponse>> GetAllCustomer();
        public Customer GetCustomerByUsername(string? username);
        public Task<CurrentUserResponse> GetCustomerById(Guid id);
        public Customer GetCustomerByEmail(string? email);
        public Task<bool> UpdateCustomer(Customer customer);
        public Task<bool?> BlockCustomerById(Guid id);
        public Task UpdateCustomerPassword(Guid userId, string oldPass, string newPass);
        #endregion

        #region Authentication
        public Task<string> GenerateToken(LoginDTO login);
        public Task<string> GenerateGoolgleToken(LoginGoogleDTO loginGoogle);
        public Task Register(RegisterDTO register);
        #endregion

        public CurrentUserResponse GetCurrentUser(string authHeader);

        #region CRUD Manager (Admin's actions)

        public Task<ICollection<CurrentUserResponse>> GetUserByRole(string role);

        public Task<bool> ChangeStatusManagerByAdmin(UpdatedStatusManager request);

        public Task<bool> CreateANewManager(CreateManager request);


        #endregion
    }
}
