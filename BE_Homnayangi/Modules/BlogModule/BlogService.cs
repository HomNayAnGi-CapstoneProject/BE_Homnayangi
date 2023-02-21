using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.BlogReferenceModule.Interface;
using BE_Homnayangi.Modules.BlogSubCateModule.Interface;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Response;
using BE_Homnayangi.Modules.TypeModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.Utils;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using Library.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Library.Models.Enum.ReferenceType;

namespace BE_Homnayangi.Modules.BlogModule
{
    public class BlogService : IBlogService
    {
        #region Define repository + Constructor
        private readonly IBlogRepository _blogRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IBlogSubCateRepository _blogSubCateRepository;
        private readonly ISubCateRepository _subCateRepository;
        private readonly IRecipeDetailRepository _recipeDetailRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBlogReferenceRepository _blogReferenceRepository;

        public BlogService(IBlogRepository blogRepository, IRecipeRepository recipeRepository, IBlogSubCateRepository blogSubCateRepository,
            ISubCateRepository subCateRepository, IRecipeDetailRepository recipeDetailRepository, ITypeRepository typeRepository, IUserRepository userRepository,
            IBlogReferenceRepository blogReferenceRepository)
        {
            _blogRepository = blogRepository;
            _recipeRepository = recipeRepository;
            _blogSubCateRepository = blogSubCateRepository;
            _subCateRepository = subCateRepository;
            _recipeDetailRepository = recipeDetailRepository;
            _typeRepository = typeRepository;
            _userRepository = userRepository;
            _blogReferenceRepository = blogReferenceRepository;
        }
        #endregion

        #region Get Blog
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
        #endregion

        #region CUD Blog
        public async Task<Guid> CreateEmptyBlog(Guid authorId)
        {
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
                    CreatedDate = DateTime.Now,
                    BlogStatus = 2, // drafted
                    AuthorId = authorId
                };
                await _blogRepository.AddAsync(blog);

                //add blog reference
                var listBlogRef = new List<BlogReference>();
                for(int i =0; i<=3; i++)
                {
                    BlogReference blogReference = new BlogReference()
                    {
                        BlogReferenceId = Guid.NewGuid(),
                        BlogId = blog.BlogId,
                        Status = 2,
                        Type = i
                    };
                    listBlogRef.Add(blogReference);
                }
                await _blogReferenceRepository.AddRangeAsync(listBlogRef);

                return blog.BlogId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateEmptyBlog: " + ex.Message);
                throw new Exception("Error at CreateEmptyBlog BlogService!!! Details: " + ex.Message);
            }
        }

        public async Task UpdateBlog(BlogUpdateRequest request, Guid currentUserId)
        {
            try
            {
                #region validation
                // sub cates exceed limit
                if (request.BlogSubCates.Count > 5)
                    throw new Exception(ErrorMessage.BlogError.BLOG_SUBCATES_LIMIT);

                // blog not existed
                var blog = _blogRepository
                    .GetBlogsBy(b => b.BlogId.Equals(request.Blog.BlogId),
                        options: (l) => l.AsNoTracking().ToList(),
                        includeProperties: "Recipe")
                    .Result
                    .FirstOrDefault();

                if (blog == null)
                    throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);
                #endregion

                #region update recipe and recipe details
                // get ingredients of recipe
                var recipeDetails = await _recipeDetailRepository
                    .GetRecipeDetailsBy(r => r.RecipeId.Equals(blog.RecipeId),
                        options: (l) => l.AsNoTracking().ToList());

                // check if exist then update, else add
                var joinRecipeDetail = request.RecipeDetails
                    .Join(recipeDetails, l => l.IngredientId, r => r.IngredientId,
                    (l, r) => l).ToList();

                foreach (var r in request.RecipeDetails)
                {
                    if (!joinRecipeDetail.Contains(r))
                    {
                        r.RecipeId = blog.Recipe.RecipeId;
                        await _recipeDetailRepository.AddAsync(r);
                    }
                }
                // check if leftover then remove
                foreach (var rd in recipeDetails)
                {
                    var r = joinRecipeDetail.Find(r => r.IngredientId.Equals(rd.IngredientId));
                    if (r == null)
                        await _recipeDetailRepository.RemoveAsync(rd);
                    else
                    {
                        rd.Quantity = r.Quantity;
                        await _recipeDetailRepository.UpdateAsync(rd);
                    }
                }

                // update recipe
                request.Recipe.RecipeId = blog.RecipeId == null
                    ? throw new Exception(ErrorMessage.BlogError.BLOG_NOT_BINDING_TO_RECIPE)
                    : blog.RecipeId.GetValueOrDefault();
                request.Recipe.Status = blog.Recipe.Status;
                request.Recipe.Title = blog.Title;
                await _recipeRepository.UpdateAsync(request.Recipe);
                #endregion

                #region update blog and subcates
                // get sub cates of blog 
                var subCates = await _blogSubCateRepository
                    .GetBlogSubCatesBy(b => b.BlogId.Equals(request.Blog.BlogId),
                        options: (l) => l.AsNoTracking().ToList());

                // check if not exist then add
                var joinSubCate = request.BlogSubCates
                    .Join(subCates, l => l.SubCateId, r => r.SubCateId,
                    (l, r) => l).ToList();

                foreach (var b in request.BlogSubCates)
                {
                    if (!joinSubCate.Contains(b))
                    {
                        b.CreatedDate = DateTime.Now;
                        await _blogSubCateRepository.AddAsync(b);
                    }
                }
                // check if leftover then remove
                foreach (var s in subCates)
                {
                    if (!joinSubCate.Select(j => j.SubCateId).ToList().Contains(s.SubCateId))
                    {
                        await _blogSubCateRepository.RemoveAsync(s);
                    }
                }

                #region Update blog reference
                var listBlogRefUpdate = _blogReferenceRepository.GetBlogReferencesBy(x => x.BlogId == blog.BlogId).Result.ToList().Join(request.BlogReferences, x => x.Type, y => y.Type, (x,y) => new BlogReference
                {
                    BlogId = blog.BlogId,
                    Type = x.Type,
                    BlogReferenceId = x.BlogReferenceId,
                    Text = y.Text,
                    Html = y.Html,
                    Status = x.Status
                });
                #endregion

                // update blog
                request.Blog.UpdatedDate = DateTime.Now;
                request.Blog.AuthorId = currentUserId;
                request.Blog.RecipeId = blog.RecipeId;
                request.Blog.CreatedDate = blog.CreatedDate;
                request.Blog.Reaction = blog.Reaction;
                request.Blog.View = blog.View;
                await _blogRepository.UpdateAsync(request.Blog);
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateBlog: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteBlog(Guid? id)
        {
            Blog blogDelete = _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId.Equals(id) && x.BlogStatus == 1).Result;
            if (blogDelete == null) return;
            blogDelete.BlogStatus = 0;
            await _blogRepository.UpdateAsync(blogDelete);
        }
        public async Task RemoveBlogDraft(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }
                Blog blogDraftRemove = _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId == id && x.BlogStatus == 2).Result;

                if (blogDraftRemove == null)
                {
                    throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);
                }
                var listBlogReferences = _blogReferenceRepository.GetBlogReferencesBy(x => x.BlogId == blogDraftRemove.BlogId).Result.ToList();
                var listBlogSubCates = _blogSubCateRepository.GetBlogSubCatesBy(x => x.BlogId == blogDraftRemove.BlogId).Result.ToList();
                var recipe = _recipeRepository.GetFirstOrDefaultAsync(x => x.RecipeId == blogDraftRemove.RecipeId).Result;
                var listRecipeDetails = _recipeDetailRepository.GetRecipeDetailsBy(x => x.RecipeId == blogDraftRemove.RecipeId).Result.ToList();

                await _recipeDetailRepository.RemoveRangeAsync(listRecipeDetails);
                await _recipeRepository.RemoveAsync(recipe);
                await _blogSubCateRepository.RemoveRangeAsync(listBlogSubCates);
                await _blogReferenceRepository.RemoveRangeAsync(listBlogReferences);

                await _blogRepository.RemoveAsync(blogDraftRemove);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at Remove blog draft: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        #endregion

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
            }).Join(await _recipeRepository.GetNItemRandom(x => x.PackagePrice >= ((decimal)Price.PriceEnum.MIN)
            && x.PackagePrice <= ((decimal)Price.PriceEnum.MAX), numberItem: (int)NumberItem.NumberItemRandomEnum.CHEAP_PRICE),
                x => x.b.RecipeId, y => y.RecipeId, (x, y) => new
                {
                    BlogId = x.b.BlogId,
                    Title = x.b.Title,
                    //Description = x.b.Description,
                    ImageUrl = x.b.ImageUrl,
                    View = x.b.View,
                    Reaction = x.b.Reaction,
                    ListSubCateName = x.ListSubCateName,
                    PackagePrice = y.PackagePrice
                }).OrderByDescending(x => x.View).Take((int)NumberItem.NumberItemShowEnum.CHEAP_PRICE).Select(x => new GetBlogsForHomePageResponse
                {
                    BlogId = x.BlogId,
                    Title = x.Title,
                    //Description = x.Description,
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
                    //Description = b.Description,
                    ImageUrl = b.ImageUrl,
                    ListSubCateName = y.Value,
                    Reaction = b.Reaction.HasValue ? b.Reaction.Value : 0,
                    View = b.View.HasValue ? b.View.Value : 0
                }).ToList();

            return listResponse;
        }

        public async Task<ICollection<SearchBlogsResponse>> GetBlogAndRecipeByName(String name)
        {
            var Blogs = await _blogRepository.GetBlogsBy(x =>
  x.BlogStatus == 1);
            var blogResponse = Blogs.Where(x => ConvertToUnSign(x.Title).Contains(name, StringComparison.CurrentCultureIgnoreCase) || x.Title.Contains(name)).ToList().Select(x => new SearchBlogsResponse
            {
                Title = x.Title,
                BlogId = x.BlogId
            }
            ).ToList();

            return blogResponse;
        }

        #region Supported functions
        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
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
        #endregion

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
                //Description = b.Description,
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
                    //Description = b.Description,
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
                   //Description = b.Description,
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

        #region Blog Detail
        //public async Task<BlogDetailResponse> GetBlogDetails(Guid blogId)
        //{
        //    BlogDetailResponse result = null;
        //    try
        //    {
        //        var blog = await _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId == blogId && x.BlogStatus.Value == 1,
        //            includeProperties: "Recipe,Author");
        //        if (blog != null)
        //        {
        //            blog.View = blog.View.Value + 1;
        //            await _blogRepository.UpdateAsync(blog);
        //            result = new BlogDetailResponse()
        //            {
        //                 Blog information
        //                BlogId = blog.BlogId,
        //                Title = blog.Title,
        //                Description = blog.Description,
        //                Preparation = blog.Preparation,
        //                Finished = blog.Finished,
        //                Processing = blog.Processing,
        //                ImageUrl = blog.ImageUrl,
        //                CreatedDate = blog.CreatedDate.Value,
        //                UpdatedDate = blog.UpdatedDate.Value,
        //                Reaction = blog.Reaction,
        //                View = blog.View,
        //                BlogStatus = blog.BlogStatus,
        //                AuthorName = blog.Author.Firstname + " " + blog.Author.Lastname
        //            };


        //             List SubCates
        //            var listBlogSubCate = await _blogSubCateRepository.GetAll(includeProperties: "SubCate");
        //            var listSubCateName = GetListSubCateName(new List<Blog>() { blog }, listBlogSubCate);
        //            result.SubCates = listSubCateName;

        //             List RecipeDetails & List Ingredients
        //            var recipeDetails = await _recipeDetailRepository.GetRecipeDetailsBy(x => x.RecipeId == result.RecipeId,
        //                includeProperties: "Ingredient");
        //            result.RecipeDetailss = new List<RecipeDetailsResponse>();
        //            result.Ingredients = new List<IngredientResponse>();
        //            foreach (var item in recipeDetails)
        //            {
        //                if (item.Ingredient.Status != null && item.Ingredient.Status.Value)
        //                {
        //                    var type = await _typeRepository.GetFirstOrDefaultAsync(x => x.TypeId == item.Ingredient.TypeId);
        //                    IngredientResponse ingredient = new IngredientResponse()
        //                    {
        //                        IngredientId = item.IngredientId,
        //                        Name = item.Ingredient.Name,
        //                        Description = item.Ingredient.Description,
        //                        Quantity = item.Ingredient.Quantity,
        //                        UnitName = item.Ingredient.Unit.Name,
        //                        Picture = item.Ingredient.Picture,
        //                        ListImage = StringUtils.ExtractContents(item.Ingredient.ListImage),
        //                        CreatedDate = item.Ingredient.CreatedDate,
        //                        UpdatedDate = item.Ingredient.UpdatedDate,
        //                        Status = item.Ingredient.Status,
        //                        Price = item.Ingredient.Price,
        //                        TypeId = type.TypeId,
        //                        TypeName = type.Name,
        //                        TypeDescription = type.Description,
        //                    };
        //                    result.Ingredients.Add(ingredient);
        //                }
        //                RecipeDetailsResponse recipeDetail = new RecipeDetailsResponse()
        //                {
        //                    RecipeId = item.RecipeId,
        //                    IngredientId = item.IngredientId,
        //                    Description = item.Description,
        //                    Quantity = (int)item.Quantity
        //                };
        //                result.RecipeDetailss.Add(recipeDetail);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error at GetBlogDetails: " + ex.Message);
        //        throw;
        //    }
        //    return result;
        //}

        public async Task<BlogDetailResponse> GetBlogDetailPreview(Guid blogId)
        {
            BlogDetailResponse result = null;
            try
            {
                var blog = await _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId == blogId && x.BlogStatus.Value == 2,
                    includeProperties: "Recipe");

                if (blog == null) throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);

                var blogReferences = _blogReferenceRepository.GetBlogReferencesBy(x => x.BlogId == blog.BlogId).Result.ToList();

                result = new BlogDetailResponse()
                {
                    // Blog information
                    BlogId = blog.BlogId,
                    Title = blog.Title,
                    ImageUrl = blog.ImageUrl,
                    VideoUrl = blog.VideoUrl,
                    CreatedDate = blog.CreatedDate.Value,
                    UpdatedDate = blog.UpdatedDate.Value,
                    Reaction = blog.Reaction,
                    View = blog.View,
                    BlogStatus = blog.BlogStatus,
                    RecipeId = (Guid)blog.RecipeId,
                    RecipeTitle = blog.Recipe.Title,
                    RecipeImageURL = blog.Recipe.ImageUrl,
                    MaxSize = blog.Recipe.MaxSize,
                    MinSize = blog.Recipe.MinSize,
                    PackagePrice = blog.Recipe.PackagePrice,
                    CookedPrice = blog.Recipe.CookedPrice,
                };

                foreach (var item in blogReferences)
                {
                    switch (item.Type)
                    {
                        case (int)BlogReferenceType.DESCRIPTION:
                            result.DescriptionText = item.Text;
                            result.DescriptionHTML = item.Html;
                            break;
                        case (int)BlogReferenceType.PREPARATION:
                            result.PreparationText = item.Text;
                            result.PreparationHTML = item.Html;
                            break;
                        case (int)BlogReferenceType.PROCESSING:
                            result.ProcessingText = item.Text;
                            result.ProcessingHTML = item.Html;
                            break;
                        case (int)BlogReferenceType.FINISHED:
                            result.FinishedText = item.Text;
                            result.FinishedHTML = item.Html;
                            break;
                    }
                }

                result.AuthorName = _userRepository.GetFirstOrDefaultAsync(x => x.UserId == blog.AuthorId).Result.Displayname;

                // List SubCates
                result.SubCates = _blogSubCateRepository.GetBlogSubCatesBy(x => x.BlogId == blog.BlogId, includeProperties: "SubCate").Result.Select(x => new SubCateResponse
                {
                    SubCateId = x.SubCateId,
                    Name = x.SubCate.Name
                }).ToList();

                // List RecipeDetails & List Ingredients
                result.RecipeDetails = _recipeDetailRepository.GetRecipeDetailsBy(x => x.RecipeId == result.RecipeId, includeProperties: "Ingredient").Result.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogDetails: " + ex.Message);
                throw;
            }
            return result;
        }

        #endregion

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
                    //Description = b.Description,
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

        public async Task<ICollection<OverviewBlog>> GetBlogsByCustomer()
        {
            List<OverviewBlog> list = null;
            try
            {
                var blogs = await _blogRepository.GetBlogsBy(blog => blog.BlogStatus == 1, includeProperties: "Recipe");
                if (blogs != null && blogs.Count > 0)
                {
                    var listBlogSubCate = await _blogSubCateRepository.GetAll(includeProperties: "SubCate");
                    var listTagNames = GetListSubCateName(blogs, listBlogSubCate);
                    list = blogs.Select(blog => new OverviewBlog()
                    {
                        BlogId = blog.BlogId,
                        Title = blog?.Title,
                        //Description = blog?.Description,
                        ImageUrl = blog?.ImageUrl,
                        CreatedDate = blog.CreatedDate.Value,
                        View = blog?.View,
                        Reaction = blog?.Reaction,
                        ListSubCateName = listTagNames[blog.BlogId],
                        RecipeName = blog.Recipe?.Title,
                        CookedPrice = blog.Recipe?.CookedPrice,
                        TotalKcal = blog.Recipe?.TotalKcal,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        public async Task<ICollection<OverviewBlog>> GetBlogsByUser()
        {
            List<OverviewBlog> list = null;
            try
            {
                var blogs = await _blogRepository.GetBlogsBy(includeProperties: "Recipe");
                if (blogs != null && blogs.Count > 0)
                {
                    list = blogs.Join(_userRepository.GetAll().Result, x => x.AuthorId, y => y.UserId, (x, y) => new OverviewBlog()
                    {
                        BlogId = x.BlogId,
                        AuthorName = y.Displayname,
                        ImageUrl = x?.ImageUrl,
                        Title = x?.Title,
                        CreatedDate = x.CreatedDate.Value,
                        View = x?.View,
                        Reaction = x?.Reaction,
                        Status = x.BlogStatus,
                        TotalKcal = x.Recipe?.TotalKcal,
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
    }
}
