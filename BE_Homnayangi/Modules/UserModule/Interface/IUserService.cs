using Library.Models;
using Library.Models.DTO.UserDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Interface
{
#nullable enable
    public interface IUserService
    {
        public Task<Customer> GetUserByEmail(string email);
        public Task<string> GenerateToken(LoginDTO login);
        public Task AddNewCustomer(Customer newCustomer);
        public Task<string> GenerateGoolgleToken(LoginGoogleDTO loginGoogle);
        public Customer GetCustomerByEmail(string? email);
        public Customer GetCustomerByUsername(string? username);
        public Task Register(RegisterDTO register);
    }
}
