﻿using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogModule.Interface
{
    public interface IBlogService
    {
        #region CUD Blog

        public Task UpdateBlog(BlogUpdateRequest request, Guid currentUserId);
        public Task<Guid> CreateEmptyBlog(Guid authorId);
        public Task RestoreBlog(Guid id, bool isEvent);
        public Task DeleteBlog(Guid ID, bool isEvent);
        public Task RemoveBlogDraft(Guid? Id);
        #endregion

        #region GET
        public Task<ICollection<OverviewBlog>> GetBlogsByUser(string role, bool? isPending, bool isEvent);
        public Task<ICollection<OverviewBlogResponse>> GetBlogsSortByPackagePriceAsc();
        public Task<ICollection<SearchBlogsResponse>> GetBlogAndRecipeByName(String name);
        public Task<ICollection<OverviewBlogResponse>> GetBlogsBySubCateForHomePage(Guid? tagId, int numberOfItems = 0);
        public Task<PagedResponse<PagedList<BlogsByCatesResponse>>> GetBlogsBySubCates(BlogsBySubCatesRequest request);
        public Task<ICollection<OverviewBlogResponse>> GetSuggestBlogByCalo(SuggestBlogByCaloRequest request);
        public Task<ICollection<BlogsByCatesResponse>> GetBlogsByIngredientId(Guid ingredientId, bool isEvent);

        // Event
        public Task<ICollection<OverviewBlog>> GetAllEvent(bool? isExpired);

        #endregion

        #region Blog detail
        public Task<BlogDetailResponse> GetBlogDetail(bool isEvent, Guid blogId);
        public Task<BlogDetailResponse> GetBlogDetailPreview(bool isEvent, Guid blogId);
        #endregion

        #region
        public Task<bool> ApproveRejectBlog(string type, Guid blogId, bool isEvent);
        #endregion
    }
}
