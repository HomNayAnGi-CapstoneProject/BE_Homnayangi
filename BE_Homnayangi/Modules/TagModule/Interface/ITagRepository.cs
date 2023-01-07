using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Repository.Repository.Interface;

namespace BE_Homnayangi.Modules.TagModule.Interface
{
	public interface ITagRepository : IRepository<Tag>
	{
        public Task<ICollection<Tag>> GetTagsBy(
            Expression<Func<Tag, bool>> filter = null,
            Func<IQueryable<Tag>, ICollection<Tag>> options = null,
            string includeProperties = null
        );
    }
}

