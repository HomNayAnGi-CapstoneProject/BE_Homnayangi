using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using BE_Homnayangi.Modules.BlogReactionModule.Response;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using Library.Models;
using Library.Models.Constant;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogReactionModule
{
    public class BlogReactionService : IBlogReactionService
    {
        private readonly IBlogReactionRepository _blogReactionRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly ICustomerRepository _customerRepository;

        public BlogReactionService(IBlogReactionRepository blogReactionRepository,
            IBlogRepository blogRepository, ICustomerRepository customerRepository)
        {
            _blogReactionRepository = blogReactionRepository;
            _blogRepository = blogRepository;
            _customerRepository = customerRepository;
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

        public async Task<BlogReactionResponse> InteractWithBlog(Guid blogId, Guid customerId)
        {
            BlogReactionResponse result = null;
            try
            {
                #region Check this blog & customer is existed in out system
                var blog = await _blogRepository.GetFirstOrDefaultAsync(b => b.BlogId == blogId && b.BlogStatus.Value == 1);
                if (blog == null) throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);

                var customer = await _customerRepository.GetFirstOrDefaultAsync(c => c.CustomerId == customerId && !c.IsBlocked.Value);
                if (customer == null) throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_FOUND);
                #endregion
                var reaction = await _blogReactionRepository.GetFirstOrDefaultAsync(br => br.BlogId == blogId
                                                                                    && br.CustomerId == customerId);
                if (reaction == null)
                { //  insert a new record into db
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
                else
                { // update reaction status in db
                    reaction.Status = !reaction.Status;
                    await _blogReactionRepository.UpdateAsync(reaction);
                    result = ConvertDTO(reaction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at InteractWithBlog: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
