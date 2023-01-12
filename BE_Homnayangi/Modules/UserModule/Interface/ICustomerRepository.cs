using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Interface
{
    public interface ICustomerRepository : IRepository<Customer>
    {
    }
}
