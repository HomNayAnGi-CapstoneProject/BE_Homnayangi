using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using Library.Models;
using Library.PagedList;
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

        public Task<ICollection<SearchBlogsResponse>> GetBlogAndRecipeByName(String name);

        public Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsBySubCateForHomePage(Guid? tagId, int numberOfItems = 0);

        public Task<ICollection<GetBlogsForHomePageResponse>> GetSoupAndNormalBlogs(Guid? categoryId, Guid? subCateId);

        public Task<BlogDetailResponse> GetBlogDetails(Guid blogId);

        public Task<PagedResponse<PagedList<Blog>>> GetAllPaged(PagedRequest request);

    }
}
