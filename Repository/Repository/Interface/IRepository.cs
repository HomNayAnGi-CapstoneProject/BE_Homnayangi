using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid key);

        Task AddAsync(T entity);

        void Add(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);
        void AddRange(IEnumerable<T> entities);
        void UpdateRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);

        Task<ICollection<T>> GetAll(
                Func<IQueryable<T>,
                ICollection<T>> options = null,
                string includeProperties = null
        );

        // options: o => o.OrderBy(p => p.Like - p.Dislike + p.View / 10).Take(quantity).ToList()
        Task<ICollection<T>> GetNItemRandom(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, ICollection<T>> options = null,
            string includeProperties = null,
            int numberItem = 0
        );


        Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );

        Task UpdateAsync(T entity);

        Task UpdateRangeAsync(IEnumerable<T> entities);

        Task RemoveAsync(string key);

        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}