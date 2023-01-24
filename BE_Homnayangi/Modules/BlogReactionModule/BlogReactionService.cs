using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using BE_Homnayangi.Modules.BlogReactionModule.Response;
using Library.Models;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogReactionModule
{
    public class BlogReactionService : IBlogReactionService
    {
        private readonly IBlogReactionRepository _blogReactionRepository;

        public BlogReactionService(IBlogReactionRepository blogReactionRepository)
        {
            _blogReactionRepository = blogReactionRepository;
        }

        public async Task<BlogReactionResponse> GetBlogReactionByBlogAndCustomerId(Guid blogId, Guid customerId)
        {
            BlogReaction result = null;
            try
            {
                result = await _blogReactionRepository.GetFirstOrDefaultAsync(br => br.BlogId == blogId && br.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogReactionByBlogAndCustomerId:" + ex.Message);
                throw;
            }
            return result != null ? ConvertDTO(result) : null;
        }

        private BlogReactionResponse ConvertDTO(BlogReaction br)
        {
            return new BlogReactionResponse()
            {
                BlogId = br.BlogId,
                CustomerId = br.CustomerId,
                Status = br.Status != null && br.Status.Value
            };
        }

        public async Task<BlogReactionResponse> GiveReactionForBlog(Guid blogId, Guid customerId)
        {
            BlogReactionResponse result = null;
            try
            {
                var reaction = await _blogReactionRepository.GetFirstOrDefaultAsync(br => br.BlogId == blogId &&  br.CustomerId == customerId);
                if (reaction == null) //  insert a new record into db
                {
                    reaction = new BlogReaction()
                    {
                        BlogId = blogId,
                        CustomerId = customerId,
                        Status = true,
                        CreatedDate = DateTime.Now,
                    };
                    await _blogReactionRepository.AddAsync(reaction);
                    result = ConvertDTO(reaction);
                }
                else                // update reaction status in db
                {
                    reaction.Status = true;
                    await _blogReactionRepository.UpdateAsync(reaction);
                    result = ConvertDTO(reaction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GiveReactionForBlog: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<BlogReactionResponse> RemoveReactionForBlog(Guid blogId, Guid customerId)
        {
            BlogReactionResponse result = null;
            try
            {
                var reaction = await _blogReactionRepository.GetFirstOrDefaultAsync(br => br.BlogId == blogId && br.CustomerId == customerId);
                if (reaction != null)
                {
                    reaction.Status = false;
                    await _blogReactionRepository.UpdateAsync(reaction);
                    result = ConvertDTO(reaction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at RemoveReactionForBlog: " + ex.Message);
                throw;
            }
            return result;
        }
    }
}
