using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.PackageModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace BE_Homnayangi.Modules.RecipeModule
{
    public class PackageRepository : Repository<Package>, IPackageRepository
	{
        private readonly HomnayangiContext _db;

        public PackageRepository(HomnayangiContext db) : base(db)
		{
            _db = db;
		}

        public async Task<ICollection<Package>> GetPackagesBy(Expression<Func<Package, bool>> filter = null, Func<IQueryable<Package>, ICollection<Package>> options = null, string includeProperties = null)

        {
            IQueryable<Package> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : await query.ToListAsync();
        }
    }
}
