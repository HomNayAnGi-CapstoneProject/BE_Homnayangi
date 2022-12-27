using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository.CategoryRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.CategoryRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly HomnayangiContext _db;

        public CategoryRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Category>> GetCategoriesBy(
            Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, ICollection<Category>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Category> query = DbSet;

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
            var test = new Random();
            var result = new List<Category>();
            for (int i = 0; i < 4; i++)
            {
                var test2 = test.Next(0, query.ToList().Count());
                result.Add(query.ToList().ElementAt(test2));
            }
            return options != null ? options(query).ToList() : result;
        }
    }
}
