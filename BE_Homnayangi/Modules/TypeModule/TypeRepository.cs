using BE_Homnayangi.Modules.TypeModule.Interface;
using Library.DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Type = Library.Models.Type;

namespace BE_Homnayangi.Modules.TypeModule
{
    public class TypeRepository : Repository<Type>, ITypeRepository
	{
        private readonly HomnayangiContext _db;

        public TypeRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Type>> GetTypesBy(
            Expression<Func<Type, bool>> filter = null,
            Func<IQueryable<Type>, ICollection<Type>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Type> query = DbSet;

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

