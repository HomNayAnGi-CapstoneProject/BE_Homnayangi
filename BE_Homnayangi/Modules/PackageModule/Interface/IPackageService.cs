using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.PackageModule.Interface
{
    public interface IPackageService
    {
        public Task AddNewPackage(Package newPackage);

        public Task UpdatePackage(Package packageUpdate);

        public Task<ICollection<Package>> GetAll();

        public Task<ICollection<Package>> GetPackagesBy(
            Expression<Func<Package, bool>> filter = null,
            Func<IQueryable<Package>, ICollection<Package>> options = null,
            string includeProperties = null);

        public Task<ICollection<Package>> GetRandomPackagesBy(Expression<Func<Package, bool>> filter = null,
            Func<IQueryable<Package>, ICollection<Package>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public Package GetPackageByID(Guid? id);
    }
}
