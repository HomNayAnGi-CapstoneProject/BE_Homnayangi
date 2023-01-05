using BE_Homnayangi.Modules.BlogModule.Response;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogModule.Interface
{
    public interface IBlogService
    {
        public Task AddNewBlog(Blog newBlog);

        public Task UpdateBlog(Blog BlogUpdate);

        public Task DeleteBlog(Guid? ID);

        public Task<ICollection<Blog>> GetAll();

        public Task<ICollection<Blog>> GetBlogsBy(
            Expression<Func<Blog, bool>> filter = null,
            Func<IQueryable<Blog>, ICollection<Blog>> options = null,
            string includeProperties = null);

        public Task<ICollection<Blog>> GetRandomBlogsBy(Expression<Func<Blog, bool>> filter = null,
            Func<IQueryable<Blog>, ICollection<Blog>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public Blog GetBlogByID(Guid? id);

        public Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsSortByPackagePriceAsc();

        public Task<ICollection<BlogResponse>> GetBlogsByCategory(Guid categoryId, int numberItems);

        public Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsByCategoryForHomePage(Guid? categoryId);
        public Task<ICollection<BlogResponse>> GetBlogAndRecipeByName(String name);
    }
}
