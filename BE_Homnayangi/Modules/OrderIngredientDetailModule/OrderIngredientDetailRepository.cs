    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.OrderIngredientDetailModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace BE_Homnayangi.Modules.OrderIngredientDetailModule
{
    public class OrderIngredientDetailRepository : Repository<OrderIngredientDetail>, IOrderIngredientDetailRepository
	{
        private readonly HomnayangiContext _db;

        public OrderIngredientDetailRepository(HomnayangiContext db) : base(db)
		{
            _db = db;
		}

        public async Task<ICollection<OrderIngredientDetail>> GetOrderIngredientDetailsBy(Expression<Func<OrderIngredientDetail, bool>> filter = null, Func<IQueryable<OrderIngredientDetail>, ICollection<OrderIngredientDetail>> options = null, string includeProperties = null)

        {
            IQueryable<OrderIngredientDetail> query = DbSet;

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
