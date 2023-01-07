using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.TagModule.Interface;
using Library.Models;

namespace BE_Homnayangi.Modules.TagModule
{
	public class TagService : ITagService
	{
        private readonly ITagRepository _tagRepository;

		public TagService(ITagRepository tagRepository)
		{
            _tagRepository = tagRepository;
		}

        public async Task<ICollection<Tag>> GetAll()
        {
            return await _tagRepository.GetAll();
        }

        public Task<ICollection<Tag>> GetTagsBy(
                Expression<Func<Tag,
                bool>> filter = null,
                Func<IQueryable<Tag>,
                ICollection<Tag>> options = null,
                string includeProperties = null)
        {
            return _tagRepository.GetTagsBy(filter);
        }

        public Task<ICollection<Tag>> GetRandomTagsBy(
                Expression<Func<Tag, bool>> filter = null,
                Func<IQueryable<Tag>, ICollection<Tag>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _tagRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewTag(Tag newTag)
        {
            newTag.TagId = Guid.NewGuid();
            await _tagRepository.AddAsync(newTag);
        }

        public async Task UpdateTag(Tag TagUpdate)
        {
            await _tagRepository.UpdateAsync(TagUpdate);
        }

        public Tag GetTagByID(Guid? id)
        {
            return _tagRepository.GetFirstOrDefaultAsync(x => x.TagId.Equals(id)).Result;
        }
    }
}

