using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.DTO.BlogDTO;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogModule
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IRecipeRepository _recipeRepository;

        public BlogService(IBlogRepository BlogRepository, IRecipeRepository recipeRepository)
        {
            _blogRepository = BlogRepository;
            _recipeRepository = recipeRepository;
        }

        public async Task<ICollection<Blog>> GetAll()
        {
            return await _blogRepository.GetAll();
        }

        public Task<ICollection<Blog>> GetBlogsBy(
            Expression<Func<Blog, bool>> filter = null,
            Func<IQueryable<Blog>, ICollection<Blog>> options = null,
            string includeProperties = null)
        {
            return _blogRepository.GetBlogsBy(filter);
        }

        public Task<ICollection<Blog>> GetRandomBlogsBy(
            Expression<Func<Blog, bool>> filter = null,
            Func<IQueryable<Blog>, ICollection<Blog>> options = null,
            string includeProperties = null,
            int numberItem = 0)
        {
            return _blogRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewBlog(Blog newBlog)
        {
            newBlog.BlogId = Guid.NewGuid();
            await _blogRepository.AddAsync(newBlog);
        }

        public async Task UpdateBlog(Blog blogUpdate)
        {
            await _blogRepository.UpdateAsync(blogUpdate);
        }

        public async Task DeleteBlog(Guid? id)
        {
            Blog blogDelete = _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId.Equals(id) && x.BlogStatus == 1).Result;
            if (blogDelete == null) return;
            blogDelete.BlogStatus = 0;
            await _blogRepository.UpdateAsync(blogDelete);
        }

        public Blog GetBlogByID(Guid? id)
        {
            return _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId.Equals(id)).Result;
        }

        // [6] giá nguyên liệu: 50k-100k
        public async Task<ICollection<BlogResponse>> GetBlogsSortByCookedPriceAsc()
        {
            List<BlogResponse> result = new List<BlogResponse>();

            var listRecipe = await _recipeRepository.GetNItemRandom(
                x => x.PackagePrice >= 50000 && x.PackagePrice <= 100000,
                numberItem: 6);

            var listResponse = listRecipe.Join(
                await _blogRepository.GetAll(includeProperties: "Author,Category"),
                x => x.RecipeId,
                y => y.BlogId,
                (x, y) => new BlogResponse
                {
                    BlogId = y.BlogId,
                    Title = y.Title,
                    Description = y.Description,
                    ImageUrl = y.ImageUrl,
                    Reaction = y.Reaction.Value,
                    View = y.View.Value,
                    AuthorName = y.Author.Name,
                    CategoryName = y.Category.Name,
                    PackagePrice = y.Recipe.PackagePrice
                }).ToList();
            return listResponse;
        }
    }
}
