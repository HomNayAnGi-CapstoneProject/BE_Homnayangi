using System;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.IngredientModule
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private readonly HomnayangiContext _db;

        public IngredientRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<Ingredient>> GetIngredientsBy(Expression<Func<Ingredient, bool>> filter = null, Func<IQueryable<Ingredient>, ICollection<Ingredient>> options = null, string includeProperties = null)
        {
            IQueryable<Ingredient> query = DbSet;

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

