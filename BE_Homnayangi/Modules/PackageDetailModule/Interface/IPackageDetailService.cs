using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.PackageDetailModule.Interface
{
    public interface IPackageDetailService
    {
        public Task<ICollection<PackageDetail>> GetPackageDetailsBy(Expression<Func<PackageDetail, bool>> filter = null,
            Func<IQueryable<PackageDetail>, ICollection<PackageDetail>> options = null,
            string includeProperties = null);
    }
}

