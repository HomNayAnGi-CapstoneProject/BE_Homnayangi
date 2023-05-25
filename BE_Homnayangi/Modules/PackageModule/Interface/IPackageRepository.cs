using System;
using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.PackageModule.Interface
{
    public interface IPackageRepository : IRepository<Package>
    {
        public Task<ICollection<Package>> GetPackagesBy(
               Expression<Func<Package, bool>> filter = null,
               Func<IQueryable<Package>, ICollection<Package>> options = null,
               string includeProperties = null
           );
    }
}
