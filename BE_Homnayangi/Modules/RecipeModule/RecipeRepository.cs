using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RecipeModule
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        private readonly HomnayangiContext _db;

        public RecipeRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Recipe>> GetRecipesBy(
            Expression<Func<Recipe, bool>> filter = null,
            Func<IQueryable<Recipe>, ICollection<Recipe>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Recipe> query = DbSet;

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
