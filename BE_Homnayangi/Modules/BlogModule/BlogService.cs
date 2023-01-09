using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.BlogTagModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.TagModule.Interface;
using Library.Models;
using Library.PagedList;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogModule
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IBlogTagRepository _blogTagRepository;
        private readonly ITagRepository _tagRepository;
        
        public BlogService(IBlogRepository blogRepository, IRecipeRepository recipeRepository, IBlogTagRepository blogTagRepository, ITagRepository tagRepository)
        {
            _blogRepository = blogRepository;
            _recipeRepository = recipeRepository;
            _blogTagRepository = blogTagRepository;
            _tagRepository = tagRepository;
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
        public async Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsSortByPackagePriceAsc()
        {
            List<GetBlogsForHomePageResponse> result = new List<GetBlogsForHomePageResponse>();

            var listBlog = await _blogRepository.GetAll(includeProperties: "Category");
            var listBlogTag = await _blogTagRepository.GetAll(includeProperties: "Tag");

            var listTagName = GetListTagName(listBlog, listBlogTag);

            var listResponse = listBlog.Join(listTagName, b => b.BlogId, y => y.Key, (b, y) => new
            {
                b,
                CategoryName = b.Category.Name,
                ListTagName = y.Value
            }).Join(await _recipeRepository.GetNItemRandom(x => x.PackagePrice.Value >= ((decimal)Price.PriceEnum.MIN) 
            && x.PackagePrice <= ((decimal)Price.PriceEnum.MAX), numberItem: (int)NumberItem.NumberItemRandomEnum.CHEAP_PRICE),
                x => x.b.BlogId, y => y.RecipeId, (x, y) => new
                {
                    BlogId = x.b.BlogId,
                    Title = x.b.Title,
                    Description = x.b.Description,
                    ImageUrl = x.b.ImageUrl,
                    View = x.b.View,
                    Reaction = x.b.Reaction,
                    CategoryName = x.CategoryName,
                    ListTagName = x.ListTagName,
                    PackagePrice = y.PackagePrice
                }).OrderByDescending(x => x.View).Take((int) NumberItem.NumberItemShowEnum.CHEAP_PRICE).Select(x => new GetBlogsForHomePageResponse
                {
                    BlogId = x.BlogId,
                    Title = x.Title,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    CategoryName = x.CategoryName,
                    ListTagName = x.ListTagName,
                    PackagePrice = x.PackagePrice,
                    View = x.View,
                    Reaction = x.Reaction
                }).ToList();

            return listResponse;
        }

        public async Task<ICollection<BlogResponse>> GetBlogsByCategory(Guid categoryId, int numberItems)
        {
            var listBlogs = await _blogRepository.GetNItemRandom(b => b.CategoryId.Equals(categoryId), includeProperties: "Author,Category", numberItem: numberItems);

            var listResponse = listBlogs.Join(
                await _recipeRepository.GetAll(),
                b => b.BlogId,
                r => r.RecipeId,
                (b, r) => new BlogResponse
                {
                    BlogId = b.BlogId,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    Reaction = b.Reaction.Value,
                    View = b.View.Value,
                    AuthorName = b.Author.Name,
                    CategoryName = b.Category.Name,
                    PackagePrice = b.Recipe.PackagePrice
                }).ToList();
            return listResponse;
        }

        public async Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsByTagForHomePage(Guid? tagId, int numberOfItems = 0)
        {
            var listBlogTag = await _blogTagRepository.GetBlogTagsBy(x => x.TagId.Equals(tagId), includeProperties: "Tag");

            var listBlogs = await _blogRepository.GetAll(includeProperties: "Category");

            listBlogs = numberOfItems > 0
                ? listBlogs.Join(listBlogTag, x => x.BlogId, y => y.BlogId, (x, y) => x).OrderByDescending(x => x.CreatedDate).Take(numberOfItems).ToList()
                : listBlogs.Join(listBlogTag, x => x.BlogId, y => y.BlogId, (x, y) => x).OrderByDescending(x => x.CreatedDate).ToList();

            var listTagName = GetListTagName(listBlogs, listBlogTag);

            var listResponse = listBlogs
                .Join(listTagName, b => b.BlogId, y => y.Key, (b, y) => new GetBlogsForHomePageResponse
                {
                    BlogId = b.BlogId,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    CategoryName = b.Category.Name,
                    ListTagName = y.Value,
                    Reaction = b.Reaction.HasValue ? b.Reaction.Value : 0,
                    View = b.View.HasValue ? b.View.Value : 0
                }).ToList();

            return listResponse;
        }

        public async Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsByCategoryForHomePage(Guid? categoryId, int numberOfItems = 0)
        {

            var listBlogs = await _blogRepository
                .GetBlogsBy(b => b.CategoryId.Equals(categoryId),
                    options: numberOfItems > 0
                        ? (list) => { return list.OrderByDescending(b => b.CreatedDate).Take(numberOfItems).ToList(); }
            : (list) => { return list.OrderByDescending(b => b.CreatedDate).ToList(); },
                    includeProperties: "Category");

            var listBlogTag = await _blogTagRepository.GetAll(includeProperties: "Tag");

            var listTagName = GetListTagName(listBlogs, listBlogTag);

            var listResponse = listBlogs
                .Join(listTagName, b => b.BlogId, y => y.Key, (b, y) => new GetBlogsForHomePageResponse
                {
                    BlogId = b.BlogId,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    CategoryName = b.Category.Name,
                    ListTagName = y.Value,
                    CreatedDate = b.CreatedDate.HasValue ? b.CreatedDate.Value : new DateTime(),
                    Reaction = b.Reaction.HasValue ? b.Reaction.Value : 0,
                    View = b.View.HasValue ? b.View.Value : 0,
                }).ToList();

            return listResponse;
        }

            public async Task<ICollection<SearchBlogsResponse>> GetBlogAndRecipeByName(String name)
        {

            var Blogs = await _blogRepository.GetBlogsBy(x => x.Title.Contains(name));
            var blogResponse = Blogs.Join(
                await _recipeRepository.GetAll(),
                b => b.BlogId,
                r => r.RecipeId,
                (b, r) => new SearchBlogsResponse
                {
                    BlogId = b.BlogId,
                    Title = b.Title,
                }).ToList();

            return blogResponse;
        }


        internal IDictionary<Guid, List<string>> GetListTagName(ICollection<Blog> blogs, ICollection<BlogTag> blogTags)
        {

            var listTagName = new Dictionary<Guid, List<string>>();

            foreach (var blog in blogs)
            {
                listTagName.Add(blog.BlogId, blogTags.Where(x => x.BlogId.Equals(blog.BlogId)).Select(x => x.Tag.Name).ToList());
            }
            return listTagName;
        }

        public async Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsByCategoryAndTag(BlogFilterByCateAndTagRequest blogFilter)
        {
            var categoryId = blogFilter.CategoryId;
            var tagId = blogFilter.TagId;
            var pageSize = blogFilter.PageSize;
            var pageNumber = blogFilter.PageNumber;
            var sort = blogFilter.sort;
            ICollection<GetBlogsForHomePageResponse> response;


            if (categoryId != null)
            {
                response = await GetBlogsByCategoryForHomePage(categoryId);

                if (tagId != null)
                {
                    var blogTags = await _blogTagRepository.GetBlogTagsBy(bt => bt.TagId.Equals(tagId));

                    response = blogTags.Join(response, bt => bt.BlogId, r => r.BlogId, (bt, r) => new GetBlogsForHomePageResponse
                    {
                        BlogId = r.BlogId,
                        RecipeName = r.RecipeName,
                        Title = r.Title,
                        Description = r.Description,
                        ImageUrl = r.ImageUrl,
                        CategoryName = r.CategoryName,
                        ListTagName = r.ListTagName,
                        PackagePrice = r.PackagePrice,
                        CreatedDate = r.CreatedDate,
                        Reaction = r.Reaction,
                        View = r.View
                    }).ToList();
                }
            }
            else
            {
                response = await GetBlogsByCategoryForHomePage(categoryId);
            }

            switch (sort)
            {
                case 1:
                    response = response.OrderByDescending(r => r.CreatedDate).ToList();
                    break;
                case 2:
                    response = response.OrderByDescending(r => r.Reaction).ToList();
                    break;
                case 3:
                    response = response.OrderByDescending(r => r.View).ToList();
                    break;
                default:
                    response = response.OrderByDescending(r => r.CreatedDate).ToList();
                    break;
            }

            response = PagedList<GetBlogsForHomePageResponse>.ToPagedList(source: response, pageNumber: pageNumber, pageSize: pageSize);
            return response;
        }

        public async Task<ICollection<GetBlogsForHomePageResponse>> GetSoupAndNormalBlogs(Guid categoryId)
        {
            string tagName = GetTagNameByCurrentTime();
            if (tagName.Equals(""))
            {
                return null;
            }

            var listResponse = new List<GetBlogsForHomePageResponse>();
            try
            {
                var result = await _tagRepository.GetFirstOrDefaultAsync(x => x.Name == tagName);
                var listTagMenu = await _tagRepository.GetTagsBy(x => x.CategoryId == categoryId);
                var listBlogTag = await _blogTagRepository.GetAll(includeProperties: "Tag");

                var listBlogTagMenu = listBlogTag.Join(listTagMenu, x => x.TagId, y => y.TagId, (x, y) => x).ToList();

                var listBlogTagWithSession = listBlogTag.Where(x => x.TagId == result.TagId).ToList();

                var listMenuBlogTag = listBlogTagMenu.Join(listBlogTagWithSession, x => x.BlogId, y => y.BlogId, (x,y) => x).ToList();

                if(listMenuBlogTag.Count() == 0)
                {
                    return null;
                }

                var listMenuBlog = listBlogTag.Where(x => x.TagId == listMenuBlogTag.First().TagId).ToList();

                var listBlogs = _blogRepository.GetAll(includeProperties: "Category").Result.Join(listMenuBlog, x => x.BlogId, y => y.BlogId, (x, y) => x).ToList();

                var listTagName = GetListTagName(listBlogs, listBlogTag);

                listResponse = listBlogs
               .Join(listTagName, b => b.BlogId, y => y.Key, (b, y) => new GetBlogsForHomePageResponse
               {
                   BlogId = b.BlogId,
                   Title = b.Title,
                   Description = b.Description,
                   ImageUrl = b.ImageUrl,
                   CategoryName = b.Category.Name,
                   ListTagName = y.Value,
                   Reaction = b.Reaction.HasValue ? b.Reaction.Value : 0,
                   View = b.View.HasValue ? b.View.Value : 0
               }).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetSoupAndNormalBlogs: " + ex.Message);
            }
            return listResponse;
        }

        private string GetTagNameByCurrentTime()
        {
            int hour = Int32.Parse(DateTime.Now.ToString("HH"));
            string tagString = "";
            Console.WriteLine("Now: " + hour);

            if (hour >= 6 && hour < 10)
            {
                Console.WriteLine("Bữa sáng");
                tagString = "Bữa sáng";
            }
            else if (hour >= 10 && hour < 15)
            {
                Console.WriteLine("Bữa trưa");
                tagString = "Bữa trưa";
            }
            else if (hour >= 15 && hour < 20)
            {
                Console.WriteLine("Bữa tối");
                tagString = "Bữa tối";
            }
            else
            {
                Console.WriteLine("Error at GetSoupAndNormalBlogs: Service does not provided at this time!");
            }

            return tagString;

        }
    }
}
