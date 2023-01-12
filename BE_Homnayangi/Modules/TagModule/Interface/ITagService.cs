using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.TagModule.Response;

namespace BE_Homnayangi.Modules.TagModule.Interface
{
	public interface ITagService 
	{
        public Task AddNewTag(Tag newTag);

        public Task UpdateTag(Tag TagUpdate);

        public Task<ICollection<Tag>> GetAll();

        public Task<ICollection<Tag>> GetTagsBy(
            Expression<Func<Tag, bool>> filter = null,
            Func<IQueryable<Tag>, ICollection<Tag>> options = null,
            string includeProperties = null);

        public Task<ICollection<Tag>> GetRandomTagsBy(Expression<Func<Tag, bool>> filter = null,
            Func<IQueryable<Tag>, ICollection<Tag>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public Tag GetTagByID(Guid? id);

        public Task<ICollection<TagResponse>> GetTagsByCategoryId(Guid id);

    }
}

