using System;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RecipeDetailModule
{
    public class RecipeDetailRepository : Repository<RecipeDetail>, IRecipeDetailRepository
    {
        private readonly HomnayangiContext _db;

        public RecipeDetailRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<RecipeDetail>> GetRecipeDetailsBy(Expression<Func<RecipeDetail, bool>> filter = null, Func<IQueryable<RecipeDetail>, ICollection<RecipeDetail>> options = null, string includeProperties = null)
        {
            IQueryable<RecipeDetail> query = DbSet;

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

