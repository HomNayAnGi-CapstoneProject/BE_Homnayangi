using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.BlogTagModule.Interface;
using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.TagModule.Interface;
using BE_Homnayangi.Modules.TypeModule.Interface;
using Library.Models;
using Library.Models.Enum;
using Library.PagedList;
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
        private readonly IRecipeDetailRepository _recipeDetailRepository;
        private readonly ITypeRepository _typeRepository;

        public BlogService(IBlogRepository blogRepository, IRecipeRepository recipeRepository, IBlogTagRepository blogTagRepository,
            ITagRepository tagRepository, IRecipeDetailRepository recipeDetailRepository, ITypeRepository typeRepository)
        {
            _blogRepository = blogRepository;
            _recipeRepository = recipeRepository;
            _blogTagRepository = blogTagRepository;
            _tagRepository = tagRepository;
            _recipeDetailRepository = recipeDetailRepository;
            _typeRepository = typeRepository;
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
            return _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId.Equals(id) && x.BlogStatus == 1).Result;
        }

        // [6] giá nguyên liệu: 50k-100k
        public async Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsSortByPackagePriceAsc()
        {
            List<GetBlogsForHomePageResponse> result = new List<GetBlogsForHomePageResponse>();

            var listBlog = await _blogRepository.GetBlogsBy(x => x.BlogStatus == 1, includeProperties: "Category");
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
                }).OrderByDescending(x => x.View).Take((int)NumberItem.NumberItemShowEnum.CHEAP_PRICE).Select(x => new GetBlogsForHomePageResponse
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
            var listBlogs = await _blogRepository.GetNItemRandom(b => b.CategoryId.Equals(categoryId) && b.BlogStatus == 1, includeProperties: "Author,Category", numberItem: numberItems);

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
                    AuthorName = b.Author.Displayname,
                    CategoryName = b.Category.Name,
                    RecipePackagePrice = b.Recipe.PackagePrice
                }).ToList();
            return listResponse;
        }

        public async Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsByTagForHomePage(Guid? tagId, int numberOfItems = 0)
        {
            var listBlogTag = await _blogTagRepository.GetBlogTagsBy(x => x.TagId.Equals(tagId), includeProperties: "Tag");

            var listBlogs = await _blogRepository.GetBlogsBy(x => x.BlogStatus == 1, includeProperties: "Category");

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

        public async Task<ICollection<SearchBlogsResponse>> GetBlogAndRecipeByName(String name)
        {

            var Blogs = await _blogRepository.GetBlogsBy(x => x.Title.Contains(name) && x.BlogStatus == 1);
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

        public async Task<PagedResponse<PagedList<BlogsByCateAndTagResponse>>> GetBlogsByCategoryAndTag(BlogFilterByCateAndTagRequest blogFilter)
        {
            var categoryId = blogFilter.CategoryId;
            var tagId = blogFilter.TagId;
            var pageSize = blogFilter.PageSize;
            var pageNumber = blogFilter.PageNumber;
            var sort = blogFilter.sort;
            ICollection<BlogsByCateAndTagResponse> filteredBlogs;
            ICollection<Blog> blogs;

            blogs = categoryId != null
                ? await _blogRepository
                    .GetBlogsBy(b => b.CategoryId.Equals(categoryId) && b.BlogStatus != (int)Status.BlogStatus.DELETED,
                        includeProperties: "Category,Recipe")
                : await _blogRepository
                    .GetBlogsBy(b => b.BlogStatus != (int)Status.BlogStatus.DELETED,
                        includeProperties: "Category,Recipe");

            filteredBlogs = blogs.Select(b => new BlogsByCateAndTagResponse
            {
                BlogId = b.BlogId,
                RecipeName = b.Recipe?.Title,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                CategoryName = b.Category?.Name,
                PackagePrice = b.Recipe?.PackagePrice,
                CreatedDate = b.CreatedDate,
                Reaction = b.Reaction,
                View = b.View
            }).ToList();

            if (tagId != null)
            {
                var blogTags = await _blogTagRepository.GetBlogTagsBy(bt => bt.TagId.Equals(tagId));

                filteredBlogs = blogTags.Join(blogs, bt => bt.BlogId, b => b.BlogId, (bt, b) => new BlogsByCateAndTagResponse
                {
                    BlogId = b.BlogId,
                    RecipeName = b.Recipe?.Title,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    CategoryName = b.Category?.Name,
                    PackagePrice = b.Recipe?.PackagePrice,
                    CreatedDate = b.CreatedDate,
                    Reaction = b.Reaction,
                    View = b.View
                }).ToList();
            }

            switch (sort)
            {
                case (int)Sort.SortBy.CREATEDDATE:
                    filteredBlogs = filteredBlogs.OrderByDescending(r => r.CreatedDate).ToList();
                    break;
                case (int)Sort.SortBy.REACTION:
                    filteredBlogs = filteredBlogs.OrderByDescending(r => r.Reaction).ToList();
                    break;
                case (int)Sort.SortBy.VIEW:
                    filteredBlogs = filteredBlogs.OrderByDescending(r => r.View).ToList();
                    break;
                default:
                    filteredBlogs = filteredBlogs.OrderByDescending(r => r.CreatedDate).ToList();
                    break;
            }

            var response = PagedList<BlogsByCateAndTagResponse>.ToPagedList(source: filteredBlogs, pageNumber: pageNumber, pageSize: pageSize);

            return response.ToPagedResposne();
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

                var listMenuBlogTag = listBlogTagMenu.Join(listBlogTagWithSession, x => x.BlogId, y => y.BlogId, (x, y) => x).ToList();

                if (listMenuBlogTag.Count() == 0)
                {
                    return null;
                }

                var listMenuBlog = listBlogTag.Where(x => x.TagId == listMenuBlogTag.First().TagId).ToList();

                var listBlogs = _blogRepository.GetBlogsBy(x => x.BlogStatus == 1, includeProperties: "Category").Result.Join(listMenuBlog, x => x.BlogId, y => y.BlogId, (x, y) => x).ToList();

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

            if (hour >= 0 && hour < 10)
            {
                Console.WriteLine("Bữa sáng");
                tagString = "Bữa sáng";
            }
            else if (hour >= 10 && hour < 15)
            {
                Console.WriteLine("Bữa trưa");
                tagString = "Bữa trưa";
            }
            else if (hour >= 15 && hour < 24)
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

        public async Task<BlogDetailResponse> GetBlogDetails(Guid blogId)
        {
            BlogDetailResponse result = null;
            try
            {
                var tmp = await _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId == blogId && x.BlogStatus.Value == 1,
                    includeProperties: "Category,Recipe,Author");
                if (tmp != null)
                {
                    result = new BlogDetailResponse()
                    {
                        // Blog information
                        BlogId = tmp.BlogId,
                        Title = tmp.Title,
                        Description = tmp.Description,
                        Preparation = tmp.Preparation,
                        Processing = tmp.Processing,
                        ImageUrl = tmp.ImageUrl,
                        CreatedDate = tmp.CreatedDate.Value,
                        UpdatedDate = tmp.UpdatedDate.Value,
                        Reaction = tmp.Reaction,
                        View = tmp.View,
                        BlogStatus = tmp.BlogStatus,
                        CategoryId = tmp.CategoryId,
                        CategoryName = tmp.Category.Name,
                        AuthorName = tmp.Author.Firstname + " " + tmp.Author.Lastname,

                        // Recipes information
                        RecipeId = tmp.Recipe.RecipeId,
                        RecipeTitle = tmp.Recipe.Title,
                        RecipeImageURL = tmp.Recipe.ImageUrl,
                        RecipePackagePrice = tmp.Recipe.PackagePrice,
                        RecipeCookedPrice = tmp.Recipe.CookedPrice,
                        RecipeSize = tmp.Recipe.Size,
                    };


                    // List Tags
                    var listBlogTag = await _blogTagRepository.GetAll(includeProperties: "Tag");
                    var listTagName = GetListTagName(new List<Blog>() { tmp }, listBlogTag);
                    result.Tags = listTagName;

                    // List RecipeDetails & List Ingredients
                    var recipeDetails = await _recipeDetailRepository.GetRecipeDetailsBy(x => x.RecipeId == result.RecipeId,
                        includeProperties: "Ingredient");
                    result.RecipeDetailss = new List<RecipeDetailsResponse>();
                    result.Ingredients = new List<IngredientResponse>();
                    foreach (var item in recipeDetails)
                    {
                        if (item.Ingredient.Status != null && item.Ingredient.Status.Value)
                        {
                            var type = await _typeRepository.GetFirstOrDefaultAsync(x => x.TypeId == item.Ingredient.TypeId);
                            IngredientResponse ingredient = new IngredientResponse()
                            {
                                IngredientId = item.IngredientId,
                                Name = item.Ingredient.Name,
                                Description = item.Ingredient.Description,
                                Quantitative = item.Ingredient.Quantitative,
                                Picture = item.Ingredient.Picture,
                                CreatedDate = item.Ingredient.CreatedDate,
                                UpdatedDate = item.Ingredient.UpdatedDate,
                                Status = item.Ingredient.Status,
                                Price = item.Ingredient.Price,
                                TypeId = type.TypeId,
                                TypeName = type.Name,
                                TypeDescription = type.Description,
                            };
                            result.Ingredients.Add(ingredient);
                        }
                        RecipeDetailsResponse recipeDetail = new RecipeDetailsResponse()
                        {
                            RecipeId = item.RecipeId,
                            IngredientId = item.IngredientId,
                            Description = item.Description
                        };
                        result.RecipeDetailss.Add(recipeDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogDetails: " + ex.Message);
                throw;
            }
            return result;
        }
    }
}
