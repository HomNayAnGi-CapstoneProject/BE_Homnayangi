using BE_Homnayangi.Modules.BlogReactionModule.Response;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogReactionModule.Interface
{
    public interface IBlogReactionService
    {

        public Task<BlogReactionResponse> GetBlogReactionByBlogAndCustomerId(Guid blogId, Guid customerId);

        public Task<BlogReactionResponse> InteractWithBlog(Guid blogId, Guid customerId);

    }
}
