using BE_Homnayangi.Modules.BlogReactionModule.Response;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogReactionModule.Interface
{
    public interface IBlogReactionService
    {

        public Task<BlogReactionResponse> GetBlogReactionByBlogAndCustomerId(Guid blogId, Guid customerId);

        public Task<BlogReactionResponse> GiveReactionForBlog(Guid blogId, Guid customerId);

        public Task<BlogReactionResponse> RemoveReactionForBlog(Guid blogId, Guid customerId);
    }
}
