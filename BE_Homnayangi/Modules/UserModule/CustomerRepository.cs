using BE_Homnayangi.Modules.UserModule.Interface;
using Library.DataAccess;
using Library.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {

        private readonly HomnayangiContext _db;

        public CustomerRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
    }
}
