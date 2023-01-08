using Library.Models;
using Library.Models.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Interface
{
    public interface IUserService
    {
        public Task<Customer> GetUserByEmail(string email);
        public string GenerateToken(LoginDTO login);
        public Task AddNewCustomer(Customer newCustomer);
        public string GenerateGoolgleToken(Customer customer);
        //public Task<Customer> CreateUserByGoogleLogin(GoogleUserCreateRequest request);
        //public string GenerateTokenByGoolgle(Customer customer);
    }
}
