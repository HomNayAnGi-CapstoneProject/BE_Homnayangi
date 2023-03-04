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
        #region CUD Blog

        public Task UpdateBlog(BlogUpdateRequest request, Guid currentUserId);
        public Task<Guid> CreateEmptyBlog(Guid authorId);
        public Task RestoreBlog(Guid id);
        public Task DeleteBlog(Guid ID);
        public Task RemoveBlogDraft(Guid? Id);
        #endregion

        #region GET
        public Task<ICollection<OverviewBlog>> GetBlogsByUser();
        #endregion

        #region Blog detail
        public Task<BlogDetailResponse> GetBlogDetail(Guid blogId);
        public Task<BlogDetailResponse> GetBlogDetailPreview(Guid blogId);
        #endregion

        public Task<ICollection<OverviewBlogResponse>> GetBlogsSortByPackagePriceAsc();
        public Task<ICollection<SearchBlogsResponse>> GetBlogAndRecipeByName(String name);
        public Task<ICollection<OverviewBlogResponse>> GetBlogsBySubCateForHomePage(Guid? tagId, int numberOfItems = 0);
        public Task<PagedResponse<PagedList<BlogsByCatesResponse>>> GetBlogsBySubCates(BlogsBySubCatesRequest request);
        public Task<ICollection<OverviewBlogResponse>> GetSuggestBlogByCalo(SuggestBlogByCaloRequest request);
    }
}
