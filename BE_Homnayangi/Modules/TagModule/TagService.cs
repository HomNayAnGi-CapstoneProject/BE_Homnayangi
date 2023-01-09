using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.TagModule.Interface;
using BE_Homnayangi.Modules.TagModule.Response;
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
            return _tagRepository.GetFirstOrDefaultAsync(x => x.TagId.Equals(id) && x.Status.Value).Result;
        }

        public async Task<ICollection<TagResponse>> GetTagsByCategoryId(Guid id)
        {
            try
            {
                List<TagResponse> result = new List<TagResponse>();
                ICollection<Tag> list = await _tagRepository.GetTagsBy(x => x.CategoryId.Equals(id) && x.Status.Value);
                foreach (var tag in list)
                {
                    result.Add(new TagResponse()
                    {
                        TagId = tag.TagId,
                        Name = tag.Name
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetTagsByCategoryId: " + ex.Message);
                throw;
            }
        }
    }
}

