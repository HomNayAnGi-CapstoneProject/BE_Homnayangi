using BE_Homnayangi.Modules.PackageDetailModule.Interface;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.PackageDetailModule
{
    public class PackageDetailService : IPackageDetailService
    {
        private readonly IPackageDetailRepository _packageDetailRepository;

        public PackageDetailService(IPackageDetailRepository packageDetailRepository)
        {
            _packageDetailRepository = packageDetailRepository;
        }

        public Task<ICollection<PackageDetail>> GetPackageDetailsBy(Expression<Func<PackageDetail, bool>> filter = null,
            Func<IQueryable<PackageDetail>, ICollection<PackageDetail>> options = null,
            string includeProperties = null)
        {
            return _packageDetailRepository.GetPackageDetailsBy(filter, options, includeProperties);
        }
    }
}

