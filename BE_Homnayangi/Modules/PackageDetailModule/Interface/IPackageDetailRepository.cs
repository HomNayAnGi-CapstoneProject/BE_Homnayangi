using System;
using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.PackageDetailModule.Interface
{
    public interface IPackageDetailRepository : IRepository<PackageDetail>
    {
        public Task<ICollection<PackageDetail>> GetPackageDetailsBy(
               Expression<Func<PackageDetail, bool>> filter = null,
               Func<IQueryable<PackageDetail>, ICollection<PackageDetail>> options = null,
               string includeProperties = null
           );
    }
}

