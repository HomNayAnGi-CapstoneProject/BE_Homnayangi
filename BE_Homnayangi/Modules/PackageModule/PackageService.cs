using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.PackageDetailModule.Interface;
using BE_Homnayangi.Modules.PackageModule.Interface;
using Library.Models;
using Library.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RecipeModule
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IPackageDetailRepository _packageDetailRepository;
        private readonly IBlogRepository _blogRepository;

        public PackageService(IPackageRepository packageRepository, IPackageDetailRepository packageDetailRepository,
            IBlogRepository blogRepository)
        {
            _packageRepository = packageRepository;
            _packageDetailRepository = packageDetailRepository;
            _blogRepository = blogRepository;
        }

        public async Task<ICollection<Package>> GetAll()
        {
            return await _packageRepository.GetAll();
        }

        public Task<ICollection<Package>> GetPackagesBy(
                Expression<Func<Package,
                bool>> filter = null,
                Func<IQueryable<Package>,
                ICollection<Package>> options = null,
                string includeProperties = null)
        {
            return _packageRepository.GetPackagesBy(filter);
        }

        public Task<ICollection<Package>> GetRandomPackagesBy(
                Expression<Func<Package, bool>> filter = null,
                Func<IQueryable<Package>, ICollection<Package>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _packageRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewPackage(Package newPackage)
        {
            newPackage.PackageId = Guid.NewGuid();
            await _packageRepository.AddAsync(newPackage);
        }

        public async Task UpdatePackage(Package packageUpdate)
        {
            await _packageRepository.UpdateAsync(packageUpdate);
        }

        public Package GetPackageByID(Guid? id)
        {
            return _packageRepository.GetFirstOrDefaultAsync(x => x.PackageId.Equals(id)).Result;
        }
    }
}
