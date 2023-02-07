using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.BlogSubCateModule.Interface;
using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.DTO.RecipeDetailsDTO;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.TypeModule.Interface;
using BE_Homnayangi.Modules.Utils;
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
        private readonly IBlogSubCateRepository _blogSubCateRepository;
        private readonly ISubCateRepository _subCateRepository;
        private readonly IRecipeDetailRepository _recipeDetailRepository;
        private readonly ITypeRepository _typeRepository;

        public BlogService(IBlogRepository blogRepository, IRecipeRepository recipeRepository, IBlogSubCateRepository blogSubCateRepository,
            ISubCateRepository subCateRepository, IRecipeDetailRepository recipeDetailRepository, ITypeRepository typeRepository)
        {
            _blogRepository = blogRepository;
            _recipeRepository = recipeRepository;
            _blogSubCateRepository = blogSubCateRepository;
            _subCateRepository = subCateRepository;
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

            var listBlog = await _blogRepository.GetBlogsBy(x => x.BlogStatus == 1);
            var listBlogSubCate = await _blogSubCateRepository.GetAll(includeProperties: "SubCate");

            var listTagName = GetListSubCateName(listBlog, listBlogSubCate);

            var listResponse = listBlog.Join(listTagName, b => b.BlogId, y => y.Key, (b, y) => new
            {
                b,
                ListSubCateName = y.Value
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
                    ListSubCateName = x.ListSubCateName,
                    PackagePrice = y.PackagePrice
                }).OrderByDescending(x => x.View).Take((int)NumberItem.NumberItemShowEnum.CHEAP_PRICE).Select(x => new GetBlogsForHomePageResponse
                {
                    BlogId = x.BlogId,
                    Title = x.Title,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    ListSubCateName = x.ListSubCateName,
                    PackagePrice = x.PackagePrice,
                    View = x.View,
                    Reaction = x.Reaction
                }).ToList();

            return listResponse;
        }

        public async Task<ICollection<GetBlogsForHomePageResponse>> GetBlogsBySubCateForHomePage(Guid? subCateId, int numberOfItems = 0)
        {
            var listBlogSubCate = await _blogSubCateRepository.GetBlogSubCatesBy(x => x.SubCateId.Equals(subCateId), includeProperties: "SubCate");

            var listBlogs = await _blogRepository.GetBlogsBy(x => x.BlogStatus == 1);

            listBlogs = numberOfItems > 0
                ? listBlogs.Join(listBlogSubCate, x => x.BlogId, y => y.BlogId, (x, y) => x).OrderByDescending(x => x.CreatedDate).Take(numberOfItems).ToList()
                : listBlogs.Join(listBlogSubCate, x => x.BlogId, y => y.BlogId, (x, y) => x).OrderByDescending(x => x.CreatedDate).ToList();

            var listSubCateName = GetListSubCateName(listBlogs, listBlogSubCate);

            var listResponse = listBlogs
                .Join(listSubCateName, b => b.BlogId, y => y.Key, (b, y) => new GetBlogsForHomePageResponse
                {
                    BlogId = b.BlogId,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    ListSubCateName = y.Value,
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


        internal IDictionary<Guid, List<string>> GetListSubCateName(ICollection<Blog> blogs, ICollection<BlogSubCate> blogSubCates)
        {

            var listSubCateName = new Dictionary<Guid, List<string>>();

            foreach (var blog in blogs)
            {
                listSubCateName.Add(blog.BlogId, blogSubCates.Where(x => x.BlogId.Equals(blog.BlogId)).Select(x => x.SubCate.Name).ToList());
            }
            return listSubCateName;
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
                    .GetBlogsBy(b => b.BlogStatus == (int)Status.BlogStatus.ACTIVE,
                        includeProperties: "Category,Recipe")
                : await _blogRepository
                    .GetBlogsBy(b => b.BlogStatus == (int)Status.BlogStatus.ACTIVE,
                        includeProperties: "Category,Recipe");

            filteredBlogs = blogs.Select(b => new BlogsByCateAndTagResponse
            {
                BlogId = b.BlogId,
                RecipeName = b.Recipe?.Title,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                PackagePrice = b.Recipe?.PackagePrice,
                CreatedDate = b.CreatedDate,
                Reaction = b.Reaction,
                View = b.View
            }).ToList();

            if (tagId != null)
            {
                var blogTags = await _blogSubCateRepository.GetBlogSubCatesBy(bt => bt.SubCateId.Equals(tagId));

                filteredBlogs = blogTags.Join(blogs, bt => bt.BlogId, b => b.BlogId, (bt, b) => new BlogsByCateAndTagResponse
                {
                    BlogId = b.BlogId,
                    RecipeName = b.Recipe?.Title,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    PackagePrice = b.Recipe?.PackagePrice,
                    CreatedDate = b.CreatedDate,
                    Reaction = b.Reaction,
                    View = b.View
                }).ToList();
            }

            switch (sort)
            {
                case (int)Sort.BlogsSortBy.CREATEDDATE:
                    filteredBlogs = filteredBlogs.OrderByDescending(r => r.CreatedDate).ToList();
                    break;
                case (int)Sort.BlogsSortBy.REACTION:
                    filteredBlogs = filteredBlogs.OrderByDescending(r => r.Reaction).ToList();
                    break;
                case (int)Sort.BlogsSortBy.VIEW:
                    filteredBlogs = filteredBlogs.OrderByDescending(r => r.View).ToList();
                    break;
                default:
                    filteredBlogs = filteredBlogs.OrderByDescending(r => r.CreatedDate).ToList();
                    break;
            }

            var response = PagedList<BlogsByCateAndTagResponse>.ToPagedList(source: filteredBlogs, pageNumber: pageNumber, pageSize: pageSize);

            return response.ToPagedResponse();
        }

        public async Task<ICollection<GetBlogsForHomePageResponse>> GetSoupAndNormalBlogs(Guid? categoryId, Guid? subCateId)
        {
            string subCateName = GetSubCateNameByCurrentTime();
            if (subCateName.Equals(""))
            {
                return null;
            }

            var listResponse = new List<GetBlogsForHomePageResponse>();
            try
            {
                var result = await _subCateRepository.GetFirstOrDefaultAsync(x => x.Name == subCateName);
                var listSubCateMenu = await _subCateRepository.GetSubCatesBy(x => x.CategoryId == categoryId && x.Status == true);
                var listBlogSubCate = await _blogSubCateRepository.GetAll(includeProperties: "SubCate");

                var listBlogSubCateMenu = listBlogSubCate.Join(listSubCateMenu, x => x.SubCateId, y => y.SubCategoryId, (x, y) => x).ToList();

                var listBlogSubCateWithSession = listBlogSubCate.Where(x => x.SubCateId == result.SubCategoryId).ToList();

                var listMenuBlogSubCate = listBlogSubCateMenu.Join(listBlogSubCateWithSession, x => x.BlogId, y => y.BlogId, (x, y) => x).ToList();

                if (listMenuBlogSubCate.Count() == 0)
                {
                    return null;
                }

                var listMenuBlogSubCateEnd = new List<BlogSubCate>();

                if (subCateId != null)
                {
                    foreach (var item in listMenuBlogSubCate)
                    {
                        if (item.SubCateId != subCateId) listMenuBlogSubCateEnd.Add(item);
                    }
                    listMenuBlogSubCateEnd = listMenuBlogSubCateEnd.OrderByDescending(x => x.CreatedDate).ToList();
                }
                else
                {
                    listMenuBlogSubCateEnd = listMenuBlogSubCate.OrderByDescending(x => x.CreatedDate).ToList();
                }

                var listMenuBlog = listBlogSubCate.Where(x => x.SubCateId == listMenuBlogSubCateEnd.First().SubCateId).ToList();

                var listBlogs = _blogRepository.GetBlogsBy(x => x.BlogStatus == 1).Result.Join(listMenuBlog, x => x.BlogId, y => y.BlogId, (x, y) => x).ToList();

                var listTagName = GetListSubCateName(listBlogs, listBlogSubCate);

                subCateId = listSubCateMenu.Join(listMenuBlogSubCateEnd, x => x.SubCategoryId, y => y.SubCateId, (x, y) => x.SubCategoryId).ToList().First();

                listResponse = listBlogs
               .Join(listTagName, b => b.BlogId, y => y.Key, (b, y) => new GetBlogsForHomePageResponse
               {
                   BlogId = b.BlogId,
                   Title = b.Title,
                   Description = b.Description,
                   ImageUrl = b.ImageUrl,
                   ListSubCateName = y.Value,
                   Reaction = b.Reaction.HasValue ? b.Reaction.Value : 0,
                   View = b.View.HasValue ? b.View.Value : 0,
                   SubCateId = subCateId
               }).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetSoupAndNormalBlogs: " + ex.Message);
            }
            return listResponse;
        }

        private string GetSubCateNameByCurrentTime()
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
                var blog = await _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId == blogId && x.BlogStatus.Value == 1,
                    includeProperties: "Recipe,Author");
                if (blog != null)
                {
                    blog.View = blog.View.Value + 1;
                    await _blogRepository.UpdateAsync(blog);
                    result = new BlogDetailResponse()
                    {
                        // Blog information
                        BlogId = blog.BlogId,
                        Title = blog.Title,
                        Description = blog.Description,
                        Preparation = blog.Preparation,
                        Finished = blog.Finished,
                        Processing = blog.Processing,
                        ImageUrl = blog.ImageUrl,
                        CreatedDate = blog.CreatedDate.Value,
                        UpdatedDate = blog.UpdatedDate.Value,
                        Reaction = blog.Reaction,
                        View = blog.View,
                        BlogStatus = blog.BlogStatus,
                        AuthorName = blog.Author.Firstname + " " + blog.Author.Lastname
                    };


                    // List SubCates
                    var listBlogSubCate = await _blogSubCateRepository.GetAll(includeProperties: "SubCate");
                    var listSubCateName = GetListSubCateName(new List<Blog>() { blog }, listBlogSubCate);
                    result.SubCates = listSubCateName;

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
                                Quantity = item.Ingredient.Quantity,
                                UnitName = item.Ingredient.Unit.Name,
                                Picture = item.Ingredient.Picture,
                                ListImage = StringUtils.ExtractContents(item.Ingredient.ListImage),
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
                            Description = item.Description,
                            Quantity = item.Quantity
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

        public async Task<PagedResponse<PagedList<BlogsByCatesResponse>>> GetBlogsBySubCates(BlogsBySubCatesRequest request)
        {
            var subCateIds = request.subCateIds != null ? StringUtils.ExtractContents(request.subCateIds) : null;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;
            var sort = request.sort;
            var sortDesc = request.sortDesc;
            try
            {
                List<Blog> blogs = new();

                if (subCateIds == null)
                {
                    blogs = _blogRepository.GetBlogsBy(b => b.BlogStatus > 0).Result.ToList();
                }
                else
                {
                    var filteredBlogs = await _blogSubCateRepository
                        .GetBlogSubCatesBy(options: (bs) => { return bs.Where(b => subCateIds.Contains(b.SubCateId.ToString())).ToList(); },
                            includeProperties: "Blog");

                    blogs = filteredBlogs.Select(f => f.Blog).ToList();
                }

                switch (sort)
                {
                    case (int)Sort.BlogsSortBy.CREATEDDATE:
                        blogs = sortDesc ?
                            blogs.OrderByDescending(r => r.CreatedDate).ToList() :
                            blogs.OrderBy(r => r.CreatedDate).ToList();
                        break;
                    case (int)Sort.BlogsSortBy.REACTION:
                        blogs = sortDesc ?
                            blogs.OrderByDescending(r => r.Reaction).ToList() :
                            blogs.OrderBy(r => r.Reaction).ToList();
                        break;
                    case (int)Sort.BlogsSortBy.VIEW:
                        blogs = sortDesc ?
                            blogs.OrderByDescending(r => r.View).ToList() :
                            blogs.OrderBy(r => r.View).ToList();
                        break;
                    default:
                        blogs = sortDesc ?
                            blogs.OrderByDescending(r => r.CreatedDate).ToList() :
                            blogs.OrderBy(r => r.CreatedDate).ToList();
                        break;
                }

                var blogsByCatesResponse = blogs.Select(b => new BlogsByCatesResponse
                {
                    BlogId = b.BlogId,
                    RecipeName = b.Recipe?.Title,
                    Title = b.Title,
                    Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    PackagePrice = b.Recipe?.PackagePrice,
                    CreatedDate = b.CreatedDate,
                    Reaction = b.Reaction,
                    View = b.View
                }).ToList();

                var response = PagedList<BlogsByCatesResponse>.ToPagedList(source: blogsByCatesResponse, pageNumber: pageNumber, pageSize: pageSize);
                return response.ToPagedResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogsBySubCates: " + ex.Message);
                throw;
            }
        }

        public async Task<Guid> CreateEmptyBlog()
        {
            Guid id = new Guid();
            try
            {
                // add an empty recipe
                Guid recipeId = Guid.NewGuid();
                Recipe recipe = new Recipe()
                {
                    RecipeId = recipeId,
                    Status = 2 // drafted
                };
                await _recipeRepository.AddAsync(recipe);

                // add an empty blog
                Blog blog = new Blog()
                {
                    BlogId = recipeId,
                    RecipeId = recipeId,
                    CreatedDate =DateTime.Now,
                    BlogStatus = 2 // drafted
                };
                await _blogRepository.AddAsync(blog);
                id = blog.BlogId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateEmptyBlog: " + ex.Message);
                throw;
            }
            return id;
        }
    }
}
