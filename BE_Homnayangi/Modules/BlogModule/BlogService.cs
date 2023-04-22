using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface;
using BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using BE_Homnayangi.Modules.BlogReferenceModule.Interface;
using BE_Homnayangi.Modules.BlogSubCateModule.Interface;
using BE_Homnayangi.Modules.CommentModule.Interface;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Response;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.Utils;
using FluentValidation.Results;
using GSF.Collections;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using Library.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Library.Models.Enum.ReferenceType;
using static Library.Models.Enum.Status;

namespace BE_Homnayangi.Modules.BlogModule
{
    public class BlogService : IBlogService
    {
        #region Define repository + Constructor
        private readonly IBlogRepository _blogRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IBlogSubCateRepository _blogSubCateRepository;
        private readonly IRecipeDetailRepository _recipeDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBlogReferenceRepository _blogReferenceRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogReactionRepository _blogReactionRepository;
        private readonly IAccomplishmentRepository _accomplishmentRepository;
        private readonly ICaloReferenceRepository _caloReferenceRepository;
        private readonly ISeasonReferenceRepository _seasonReferenceRepository;

        public BlogService(IBlogRepository blogRepository, IRecipeRepository recipeRepository, IBlogSubCateRepository blogSubCateRepository,
            ISubCateRepository subCateRepository, IRecipeDetailRepository recipeDetailRepository,
            IUserRepository userRepository, IBlogReferenceRepository blogReferenceRepository, ICommentRepository commentRepository,
            IBlogReactionRepository blogReactionRepository, IAccomplishmentRepository accomplishmentRepository,
            ICaloReferenceRepository caloReferenceRepository, ISeasonReferenceRepository seasonReferenceRepository)
        {
            _blogRepository = blogRepository;
            _recipeRepository = recipeRepository;
            _blogSubCateRepository = blogSubCateRepository;
            _recipeDetailRepository = recipeDetailRepository;
            _userRepository = userRepository;
            _blogReferenceRepository = blogReferenceRepository;
            _commentRepository = commentRepository;
            _blogReactionRepository = blogReactionRepository;
            _accomplishmentRepository = accomplishmentRepository;
            _caloReferenceRepository = caloReferenceRepository;
            _seasonReferenceRepository = seasonReferenceRepository;
        }
        #endregion

        #region Get Blog

        public async Task<ICollection<OverviewBlog>> GetBlogsByUser(string role, bool? isPending, bool? isEvent)
        {
            List<OverviewBlog> list = null;
            try
            {
                if (isPending != null && isPending.Value) // get pending blogs
                {
                    ICollection<Blog> pendingBlogs = new List<Blog>();
                    if (isEvent == null || isEvent == false) // Lấy hết
                    {
                        pendingBlogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus == (int)Status.BlogStatus.PENDING,
                                                                                                    includeProperties: "Author");
                    }
                    else if (isEvent.Value) // Lấy event
                    {
                        pendingBlogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus == (int)Status.BlogStatus.PENDING && b.IsEvent.Value,
                                                                                                    includeProperties: "Author");
                    }

                    if (pendingBlogs.Count == 0)
                    {
                        return null;
                    }
                    list = pendingBlogs.Select(
                        blog => new OverviewBlog()
                        {
                            BlogId = blog.BlogId,
                            AuthorName = blog.Author.Firstname + " " + blog.Author.Lastname,
                            ImageUrl = blog.ImageUrl,
                            Title = blog.Title,
                            CreatedDate = blog.CreatedDate.Value,
                            View = blog.View,
                            Reaction = blog.Reaction,
                            Status = blog.BlogStatus,
                            TotalKcal = blog.Recipe?.TotalKcal,
                            IsEvent = blog.IsEvent.HasValue ? blog.IsEvent.Value : null,
                            EventExpiredDate = blog.IsEvent.HasValue ? blog.EventExpiredDate : null
                        }).OrderByDescending(b => b.CreatedDate).ToList();
                }
                else if (isPending != null && !isPending.Value) // get !pending blogs
                {
                    var blogs = isEvent != null && isEvent.Value
                        ? await _blogRepository.GetBlogsBy(b => b.BlogStatus != (int)Status.BlogStatus.PENDING && b.IsEvent.Value, includeProperties: "Recipe,Author")
                        : await _blogRepository.GetBlogsBy(b => b.BlogStatus != (int)Status.BlogStatus.PENDING, includeProperties: "Recipe,Author");
                    if (blogs != null && blogs.Count > 0)
                    {
                        list = blogs.Select(
                            blog => new OverviewBlog()
                            {
                                BlogId = blog.BlogId,
                                AuthorName = blog.Author.Firstname + " " + blog.Author.Lastname,
                                ImageUrl = blog.ImageUrl,
                                Title = blog.Title,
                                CreatedDate = blog.CreatedDate.Value,
                                View = blog.View,
                                Reaction = blog.Reaction,
                                Status = blog.BlogStatus,
                                TotalKcal = blog.Recipe?.TotalKcal,
                                IsEvent = blog.IsEvent.HasValue ? blog.IsEvent.Value : null,
                                EventExpiredDate = blog.IsEvent.HasValue ? blog.EventExpiredDate : null
                            }).OrderByDescending(b => b.CreatedDate).ToList();
                    }
                }
                else // isPending: null > get all
                {
                    var blogs = isEvent != null && isEvent.Value
                       ? await _blogRepository.GetBlogsBy(b => b.IsEvent.Value && b.IsEvent.Value, includeProperties: "Recipe,Author")
                       : await _blogRepository.GetBlogsBy(includeProperties: "Recipe,Author");
                    if (blogs != null && blogs.Count > 0)
                    {
                        list = blogs.Select(
                            blog => new OverviewBlog()
                            {
                                BlogId = blog.BlogId,
                                AuthorName = blog.Author.Firstname + " " + blog.Author.Lastname,
                                ImageUrl = blog.ImageUrl,
                                Title = blog.Title,
                                CreatedDate = blog.CreatedDate.Value,
                                View = blog.View,
                                Reaction = blog.Reaction,
                                Status = blog.BlogStatus,
                                TotalKcal = blog.Recipe?.TotalKcal,
                                IsEvent = blog.IsEvent.HasValue ? blog.IsEvent.Value : null,
                                EventExpiredDate = blog.IsEvent.HasValue ? blog.EventExpiredDate : null
                            }).OrderByDescending(b => b.CreatedDate).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogsByUser:" + ex.Message);
                throw new Exception(ex.Message);
            }
            return list;
        }

        // [6] giá nguyên liệu: 50k-100k
        public async Task<ICollection<OverviewBlogResponse>> GetBlogsSortByPackagePriceAsc()
        {
            try
            {
                List<OverviewBlogResponse> result = new List<OverviewBlogResponse>();

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
                        ImageUrl = x.b.ImageUrl,
                        View = x.b.View,
                        Reaction = x.b.BlogReactions,
                        ListSubCateName = x.ListSubCateName,
                        RecipeTitle = x.b.Recipe.Title,
                        RecipeId = x.b.RecipeId,
                        PackagePrice = y.PackagePrice,
                        CookedPrice = y.CookedPrice,
                        TotalKcal = y.TotalKcal
                    }).Join(_blogReferenceRepository.GetBlogReferencesBy(x => x.Type == (int)BlogReferenceType.DESCRIPTION).Result, x => x.BlogId, y => y.BlogId, (x, y) => new
                    {
                        x,
                        Description = y.Html


                    }).OrderByDescending(x => x.x.View).Take((int)NumberItem.NumberItemShowEnum.CHEAP_PRICE).Select(x => new OverviewBlogResponse
                    {
                        BlogId = x.x.BlogId,
                        Title = x.x.Title,
                        Description = x.Description,
                        ImageUrl = x.x.ImageUrl,
                        ListSubCateName = x.x.ListSubCateName,
                        RecipeTitle = x.x.RecipeTitle,
                        RecipeId = x.x.RecipeId,
                        PackagePrice = (decimal)x.x.PackagePrice,
                        CookedPrice = (decimal)x.x.CookedPrice,
                        TotalKcal = (int)x.x.TotalKcal
                    }).ToList();
                var listRecipeDetails = await _recipeDetailRepository.GetAll(includeProperties: "Ingredient");
                foreach (var blog in listResponse)
                {
                    blog.RecipeDetails = ConvertToRecipeDetailResponse(blog.BlogId, listRecipeDetails.ToList());
                }

                return listResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogsSortByPackagePriceAsc: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<SearchBlogsResponse>> GetBlogAndRecipeByName(String name)
        {
            var Blogs = await _blogRepository.GetBlogsBy(x => x.BlogStatus == 1);
            var blogResponse = Blogs.Where(x => ConvertToUnSign(x.Title)
                .Contains(ConvertToUnSign(name), StringComparison.CurrentCultureIgnoreCase) || x.Title.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                .ToList()
                .Select(x => new SearchBlogsResponse
                {
                    Title = x.Title,
                    BlogId = x.BlogId
                }
                )
                .ToList();

            return blogResponse;
        }

        public async Task<ICollection<OverviewBlogResponse>> GetBlogsBySubCateForHomePage(Guid? subCateId, int numberOfItems = 0)
        {
            try
            {
                var listBlogSubCate = await _blogSubCateRepository.GetBlogSubCatesBy(x => x.SubCateId.Equals(subCateId), includeProperties: "SubCate");

                var listBlogs = await _blogRepository.GetBlogsBy(x => x.BlogStatus == 1, includeProperties: "Recipe");

                listBlogs = numberOfItems > 0
                    ? listBlogs.Join(listBlogSubCate, x => x.BlogId, y => y.BlogId, (x, y) => x).OrderByDescending(x => x.CreatedDate).Take(numberOfItems).ToList()
                    : listBlogs.Join(listBlogSubCate, x => x.BlogId, y => y.BlogId, (x, y) => x).OrderByDescending(x => x.CreatedDate).ToList();

                var listSubCateName = GetListSubCateName(listBlogs, listBlogSubCate);
                var listRecipeDetails = await _recipeDetailRepository.GetAll(includeProperties: "Ingredient");


                var listResponse = listBlogs
                    .Join(listSubCateName, b => b.BlogId, y => y.Key, (b, y) => new
                    {
                        b,
                        ListSubCateName = y.Value,

                    })
                    .Join(_blogReferenceRepository.GetBlogReferencesBy(x => x.Type == (int)BlogReferenceType.DESCRIPTION).Result, x => x.b.BlogId, y => y.BlogId,
                    (x, y) => new OverviewBlogResponse
                    {
                        BlogId = x.b.BlogId,
                        Title = x.b.Title,
                        ImageUrl = x.b.ImageUrl,
                        ListSubCateName = x.ListSubCateName,
                        Description = y.Html,
                        RecipeTitle = x.b.Recipe.Title,
                        RecipeId = x.b.RecipeId,
                        PackagePrice = (decimal)x.b.Recipe.PackagePrice,
                        CookedPrice = (decimal)x.b.Recipe.CookedPrice,
                        TotalKcal = (int)x.b.Recipe.TotalKcal,
                    }).ToList();
                foreach (var blog in listResponse)
                {
                    blog.RecipeDetails = ConvertToRecipeDetailResponse(blog.BlogId, listRecipeDetails.ToList());
                }
                return listResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogsBySubCateForHomePage: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagedResponse<Library.PagedList.PagedList<BlogsByCatesResponse>>> GetBlogsBySubCates(BlogsBySubCatesRequest request)
        {
            var subCateIds = request.SubCateIds != null ? StringUtils.ExtractContents(request.SubCateIds) : null;
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;
            var sort = request.sort;
            var sortDesc = request.sortDesc;
            var searchString = request.SearchString;
            try
            {
                List<Blog> blogs = new();

                if (subCateIds == null)
                {
                    blogs = request.IsEvent != null && request.IsEvent.Value
                       ? _blogRepository.GetBlogsBy(b => b.BlogStatus == ((int)Status.BlogStatus.ACTIVE) && b.IsEvent.Value).Result.ToList()
                       : _blogRepository.GetBlogsBy(b => b.BlogStatus == ((int)Status.BlogStatus.ACTIVE)).Result.ToList();
                    if (searchString != null)
                    {
                        searchString = Regex.Replace(request.SearchString, @"\s+", " ").Trim();
                        blogs = blogs.Where(x => ConvertToUnSign(x.Title)
                                                .Contains(ConvertToUnSign(searchString), StringComparison.CurrentCultureIgnoreCase)
                                                || x.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                                                .ToList();
                    }
                }
                else
                {
                    ICollection<BlogSubCate> filteredBlogs = new List<BlogSubCate>();
                    if (request.IsEvent == null) // Lấy hết
                    {
                        filteredBlogs = await _blogSubCateRepository
                        .GetBlogSubCatesBy(options: (bs) =>
                        {
                            return bs.Where(b => subCateIds.Contains(b.SubCateId.ToString())
                                                                                && b.Blog.BlogStatus == (int)Status.BlogStatus.ACTIVE).ToList();
                        },
                                                                                includeProperties: "Blog");
                    }
                    else if (request.IsEvent.Value) // Lấy event
                    {
                        filteredBlogs = await _blogSubCateRepository
                        .GetBlogSubCatesBy(options: (bs) =>
                        {
                            return bs.Where(b => subCateIds.Contains(b.SubCateId.ToString())
                                                                                && b.Blog.BlogStatus == (int)Status.BlogStatus.ACTIVE && b.Blog.IsEvent.Value).ToList();
                        },
                                                                                includeProperties: "Blog");
                    }
                    else // Lấy blog thôi
                    {
                        filteredBlogs = await _blogSubCateRepository
                        .GetBlogSubCatesBy(options: (bs) =>
                        {
                            return bs.Where(b => subCateIds.Contains(b.SubCateId.ToString())
                                                                                && b.Blog.BlogStatus == (int)Status.BlogStatus.ACTIVE && !b.Blog.IsEvent.Value).ToList();
                        },
                                                                                includeProperties: "Blog");
                    }

                    blogs = filteredBlogs.Select(f => f.Blog).ToList();
                    if (searchString != null)
                    {
                        searchString = Regex.Replace(request.SearchString, @"\s+", " ").Trim();
                        blogs = blogs.Where(x => ConvertToUnSign(x.Title)
                                                .Contains(ConvertToUnSign(searchString), StringComparison.CurrentCultureIgnoreCase)
                                                || x.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                                                .ToList();
                    }
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

                var blogsByCatesResponse = blogs.Join(_blogReferenceRepository.GetBlogReferencesBy(x => x.Type == (int)BlogReferenceType.DESCRIPTION).Result,
                    b => b.BlogId, y => y.BlogId, (b, y) => new BlogsByCatesResponse
                    {
                        BlogId = b.BlogId,
                        RecipeName = b.Recipe?.Title,
                        Title = b.Title,
                        Description = y.Html,
                        ImageUrl = b.ImageUrl,
                        PackagePrice = b.Recipe?.PackagePrice,
                        CreatedDate = b.CreatedDate,
                        Reaction = b.Reaction,
                        View = b.View,
                        IsEvent = b.IsEvent.Value,
                        EventExpiredDate = b.IsEvent.Value ? b.EventExpiredDate.Value : null

                    }).ToList();

                var response = Library.PagedList.PagedList<BlogsByCatesResponse>.ToPagedList(source: blogsByCatesResponse, pageNumber: pageNumber, pageSize: pageSize);
                return response.ToPagedResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogsBySubCates: " + ex.Message);
                throw;
            }
        }

        public async Task<ICollection<OverviewBlogResponse>> GetSuggestBlogByCalo(SuggestBlogByCaloRequest request)
        {
            try
            {
                ValidationResult validationResult = new SuggestBlogByCaloRequestValidator().Validate(request);
                if (!validationResult.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                var result = new List<OverviewBlogResponse>();

                //get the suggest calo for user by request data
                var suggestCalo = _caloReferenceRepository.GetFirstOrDefaultAsync(x => x.IsMale == request.IsMale &&
                ((x.FromAge <= request.Age && x.ToAge > request.Age) || (x.FromAge < request.Age && x.ToAge >= request.Age))).Result;
                if (suggestCalo == null)
                {
                    throw new Exception(ErrorMessage.CaloRefError.CALO_REF_NOT_FOUND);
                }
                //get all blog
                var listBlog = _blogRepository.GetBlogsBy(x => x.BlogStatus == ((int)BlogStatus.ACTIVE), includeProperties: "Recipe").Result
                    .Where(x => x.Recipe.MaxSize == 2).ToList();
                if (listBlog.Count() == 0)
                {
                    throw new Exception(ErrorMessage.CommonError.LIST_IS_NULL);
                }
                //get list blogSubCate
                var listBlogSubCate = _blogSubCateRepository.GetBlogSubCatesBy(x => x.Status != false, includeProperties: "SubCate").Result;
                //get list blogId by blogSubCate of soup blog
                var listSoupBlogIdSubCate = listBlogSubCate.Where(x => x.SubCate.Name.Equals("Món canh")).Select(x => x.BlogId).ToList();
                //get list soup blog
                var listSoupBlog = listBlog.Join(listSoupBlogIdSubCate, x => x.BlogId, y => y, (x, y) => x).ToList();
                //get list normal blog
                var listNormalBlog = listBlog.Except(listSoupBlog);
                //create random variable
                Random rnd = new Random();
                //list blog reference description
                var listBlogDescRef = _blogReferenceRepository.GetBlogReferencesBy(x => x.Type == (int)BlogReferenceType.DESCRIPTION).Result.Select(x => new
                {
                    x.Html,
                    x.BlogId
                });
                //divide suggest calo to 1 of 3 brunch
                suggestCalo.Calo = suggestCalo.Calo / 3;
                //check season reference
                var checkSeasonRef = _seasonReferenceRepository.GetFirstOrDefaultAsync(x => x.Status == true).Result;
                if (checkSeasonRef != null)
                {
                    var listSeasonRef = listBlogSubCate.Where(x => x.SubCate.Name.Equals(checkSeasonRef.Name)).Select(x => x.BlogId).ToList();
                    listSoupBlog = listSoupBlog.Join(listSeasonRef, x => x.BlogId, y => y, (x, y) => x).ToList();
                    listNormalBlog = listNormalBlog.Join(listSeasonRef, x => x.BlogId, y => y, (x, y) => x).ToList();
                }
                //take 3 blog match the suggest calo
                do
                {
                    var firstBlog = listNormalBlog.ElementAt(rnd.Next(0, listNormalBlog.Count() - 1));
                    var secondBlog = listNormalBlog.ElementAt(rnd.Next(0, listNormalBlog.Count() - 1));
                    var soupBlog = listSoupBlog.ElementAt(rnd.Next(0, listSoupBlog.Count() - 1));
                    var listRecipeDetails = await _recipeDetailRepository.GetAll(includeProperties: "Ingredient");
                    if (firstBlog.BlogId != secondBlog.BlogId)
                    {
                        if (!listSoupBlogIdSubCate.Contains(firstBlog.BlogId) && !listSoupBlogIdSubCate.Contains(secondBlog.BlogId))
                        {
                            if (request.IsLoseWeight == true)
                            {
                                if (suggestCalo.Calo > (firstBlog.Recipe.TotalKcal + secondBlog.Recipe.TotalKcal + soupBlog.Recipe.TotalKcal))
                                {
                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = firstBlog.BlogId,
                                        Title = firstBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == firstBlog.BlogId).Html,
                                        ImageUrl = firstBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(firstBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        PackagePrice = (decimal)firstBlog.Recipe.PackagePrice,
                                        CookedPrice = (decimal)firstBlog.Recipe.CookedPrice,
                                        RecipeTitle = firstBlog.Recipe.Title,
                                        RecipeId = firstBlog.Recipe.RecipeId,
                                        RecipeDetails = ConvertToRecipeDetailResponse(firstBlog.BlogId, listRecipeDetails.ToList()),
                                        TotalKcal = (int)firstBlog.Recipe.TotalKcal,
                                        IsEvent = firstBlog.IsEvent.Value,
                                        EventExpiredDate = firstBlog.IsEvent.Value ? firstBlog.EventExpiredDate.Value : null,
                                    });

                                    result.Add(new OverviewBlogResponse
                                    {

                                        BlogId = secondBlog.BlogId,
                                        Title = secondBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == secondBlog.BlogId).Html,
                                        ImageUrl = secondBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(secondBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        PackagePrice = (decimal)secondBlog.Recipe.PackagePrice,
                                        CookedPrice = (decimal)secondBlog.Recipe.CookedPrice,
                                        TotalKcal = (int)secondBlog.Recipe.TotalKcal,
                                        RecipeTitle = secondBlog.Recipe.Title,
                                        RecipeId = secondBlog.Recipe.RecipeId,
                                        RecipeDetails = ConvertToRecipeDetailResponse(secondBlog.BlogId, listRecipeDetails.ToList()),
                                        IsEvent = secondBlog.IsEvent.Value,
                                        EventExpiredDate = secondBlog.IsEvent.Value ? secondBlog.EventExpiredDate.Value : null,
                                    });

                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = soupBlog.BlogId,
                                        Title = soupBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == soupBlog.BlogId).Html,
                                        ImageUrl = soupBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(soupBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        PackagePrice = (decimal)soupBlog.Recipe.PackagePrice,
                                        CookedPrice = (decimal)soupBlog.Recipe.CookedPrice,
                                        TotalKcal = (int)soupBlog.Recipe.TotalKcal,
                                        RecipeTitle = soupBlog.Recipe.Title,
                                        RecipeId = soupBlog.Recipe.RecipeId,
                                        RecipeDetails = ConvertToRecipeDetailResponse(soupBlog.BlogId, listRecipeDetails.ToList()),
                                        IsEvent = soupBlog.IsEvent.Value,
                                        EventExpiredDate = soupBlog.IsEvent.Value ? soupBlog.EventExpiredDate.Value : null,
                                    });

                                }
                            }
                            else
                            {
                                if ((firstBlog.Recipe.TotalKcal + secondBlog.Recipe.TotalKcal + soupBlog.Recipe.TotalKcal) > suggestCalo.Calo)
                                {
                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = firstBlog.BlogId,
                                        Title = firstBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == firstBlog.BlogId).Html,
                                        ImageUrl = firstBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(firstBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        PackagePrice = (decimal)firstBlog.Recipe.PackagePrice,
                                        CookedPrice = (decimal)firstBlog.Recipe.CookedPrice,
                                        RecipeTitle = firstBlog.Recipe.Title,
                                        RecipeId = firstBlog.Recipe.RecipeId,
                                        RecipeDetails = ConvertToRecipeDetailResponse(firstBlog.BlogId, listRecipeDetails.ToList()),
                                        TotalKcal = (int)firstBlog.Recipe.TotalKcal,
                                        IsEvent = firstBlog.IsEvent.Value,
                                        EventExpiredDate = firstBlog.IsEvent.Value ? firstBlog.EventExpiredDate.Value : null,
                                    });
                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = secondBlog.BlogId,
                                        Title = secondBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == secondBlog.BlogId).Html,
                                        ImageUrl = secondBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(secondBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        PackagePrice = (decimal)secondBlog.Recipe.PackagePrice,
                                        CookedPrice = (decimal)secondBlog.Recipe.CookedPrice,
                                        TotalKcal = (int)secondBlog.Recipe.TotalKcal,
                                        RecipeTitle = secondBlog.Recipe.Title,
                                        RecipeId = secondBlog.Recipe.RecipeId,
                                        RecipeDetails = ConvertToRecipeDetailResponse(secondBlog.BlogId, listRecipeDetails.ToList()),
                                        IsEvent = secondBlog.IsEvent.Value,
                                        EventExpiredDate = secondBlog.IsEvent.Value ? secondBlog.EventExpiredDate.Value : null,
                                    });
                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = soupBlog.BlogId,
                                        Title = soupBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == soupBlog.BlogId).Html,
                                        ImageUrl = soupBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(soupBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        PackagePrice = (decimal)soupBlog.Recipe.PackagePrice,
                                        CookedPrice = (decimal)soupBlog.Recipe.CookedPrice,
                                        TotalKcal = (int)soupBlog.Recipe.TotalKcal,
                                        RecipeTitle = soupBlog.Recipe.Title,
                                        RecipeId = soupBlog.Recipe.RecipeId,
                                        RecipeDetails = ConvertToRecipeDetailResponse(soupBlog.BlogId, listRecipeDetails.ToList()),
                                        IsEvent = soupBlog.IsEvent.Value,
                                        EventExpiredDate = soupBlog.IsEvent.Value ? soupBlog.EventExpiredDate.Value : null,
                                    });
                                }
                            }
                        }
                    }
                } while (result.Count() < 3);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<BlogsByCatesResponse>> GetBlogsByIngredientId(Guid ingredientId)
        {
            try
            {
                List<BlogsByCatesResponse> filteredBlogs = new List<BlogsByCatesResponse>();
                int retry = 0;
                while (retry < 8)
                {
                    ++retry;
                    var recipeDetails = await _recipeDetailRepository.GetNItemRandom(rd => rd.IngredientId == ingredientId,
                                                                                                        numberItem: 8);

                    var blogs = _blogRepository.GetBlogsBy(b => b.BlogStatus == ((int)Status.BlogStatus.ACTIVE),
                                                                 includeProperties: "Recipe").Result.ToList();
                    var tmpList = recipeDetails.Join(blogs, rd => rd.RecipeId, b => b.RecipeId, (rd, b) => new BlogsByCatesResponse()
                    {
                        BlogId = b.BlogId,
                        RecipeName = b.Recipe.Title,
                        Title = b.Title,
                        // Description is below
                        ImageUrl = b.ImageUrl,
                        PackagePrice = b.Recipe.PackagePrice,
                        Reaction = b.Reaction,
                        View = b.View,
                        CreatedDate = b.CreatedDate,
                    }).ToList();

                    #region Check 8 blogs distinct blogId
                    if (filteredBlogs.Count == 0)
                    {
                        filteredBlogs = tmpList;
                    }
                    else
                    {
                        filteredBlogs.AddRange(tmpList);
                    }
                    filteredBlogs = filteredBlogs.DistinctBy(b => b.BlogId).ToList();

                    if (filteredBlogs.Count == 8)
                    {
                        break;
                    }
                    else if (filteredBlogs.Count > 8)
                    {
                        filteredBlogs = filteredBlogs.Take(8).ToList();
                        break;
                    }
                    #endregion
                }

                var result = filteredBlogs.Join(_blogReferenceRepository.GetBlogReferencesBy(x => x.Type == (int)BlogReferenceType.DESCRIPTION).Result,
                    b => b.BlogId, y => y.BlogId, (b, y) => new BlogsByCatesResponse
                    {
                        BlogId = b.BlogId,
                        RecipeName = b.RecipeName,
                        Title = b.Title,
                        Description = y.Html,
                        ImageUrl = b.ImageUrl,
                        PackagePrice = b.PackagePrice,
                        CreatedDate = b.CreatedDate,
                        Reaction = b.Reaction,
                        View = b.View,
                        IsEvent = b.IsEvent,
                        EventExpiredDate = b.IsEvent ? b.EventExpiredDate.Value : null
                    }).OrderByDescending(b => b.CreatedDate).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ICollection<OverviewBlog>> GetAllEvent(bool? isExpired)
        {
            ICollection<Blog> events = new List<Blog>();
            ICollection<OverviewBlog> result = new List<OverviewBlog>();
            try
            {
                if (isExpired == null) // lấy hết
                {
                    events = await _blogRepository.GetBlogsBy(b => b.IsEvent.Value,
                                                                includeProperties: "Recipe,Author");
                }
                else if (isExpired.Value) // lấy hết hạn
                {
                    events = await _blogRepository.GetBlogsBy(b => b.IsEvent.Value
                                                                    && b.EventExpiredDate != null && b.EventExpiredDate <= DateTime.Now,
                                                                    includeProperties: "Recipe,Author");
                }
                else // lấy chưa hết hạn
                {
                    events = await _blogRepository.GetBlogsBy(b => b.IsEvent.Value
                                                                    && b.EventExpiredDate != null && b.EventExpiredDate > DateTime.Now,
                                                                    includeProperties: "Recipe,Author");
                }

                if (events.Count > 0)
                {
                    result = events.Select(e => new OverviewBlog()
                    {
                        BlogId = e.BlogId,
                        RecipeName = e.Recipe.Title,
                        Title = e.Title,
                        ImageUrl = e.ImageUrl,
                        View = e.View,
                        Reaction = e.Reaction,
                        CreatedDate = e.CreatedDate.Value,
                        TotalKcal = e.Recipe.TotalKcal,
                        AuthorName = e.Author.Firstname + " " + e.Author.Lastname,
                        IsEvent = e.IsEvent.Value,
                        EventExpiredDate = e.IsEvent.Value ? e.EventExpiredDate.Value : null,
                    }).OrderByDescending(b => b.CreatedDate).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllEvent: " + ex.Message);
                throw new Exception(ex.Message);
            }
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
                    AuthorId = authorId,
                    Reaction = 0,
                    View = 0,
                    MinutesToCook = 0
                };
                await _blogRepository.AddAsync(blog);

                //add blog reference
                var listBlogRef = new List<BlogReference>();
                for (int i = 0; i <= 3; i++)
                {
                    BlogReference blogReference = new BlogReference()
                    {
                        BlogReferenceId = Guid.NewGuid(),
                        BlogId = blog.BlogId,
                        Status = 2, //drafted
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
                        rd.Description = r.Description;
                        await _recipeDetailRepository.UpdateAsync(rd);
                    }
                }

                // update recipe
                request.Recipe.RecipeId = blog.RecipeId == null
                    ? throw new Exception(ErrorMessage.BlogError.BLOG_NOT_BINDING_TO_RECIPE)
                    : blog.RecipeId.GetValueOrDefault();
                request.Recipe.Status = request.Blog.BlogStatus;
                request.Recipe.Title = blog.Title;
                request.Recipe.ImageUrl = blog.ImageUrl;
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
                var listBlogRef = _blogReferenceRepository.GetBlogReferencesBy(x => x.BlogId == blog.BlogId, options: x => x.AsNoTracking().ToList()).Result.ToList();
                var listBlogRefUpdate = listBlogRef.Join(request.BlogReferences, x => x.Type, y => y.Type, (x, y) => new BlogReference
                {
                    BlogId = blog.BlogId,
                    Type = x.Type,
                    BlogReferenceId = x.BlogReferenceId,
                    Text = y.Text,
                    Html = y.HTML,
                    Status = request.Blog.BlogStatus
                });

                await _blogReferenceRepository.UpdateRangeAsync(listBlogRefUpdate);
                #endregion

                // update blog
                request.Blog.UpdatedDate = DateTime.Now;
                request.Blog.AuthorId = currentUserId;
                request.Blog.RecipeId = blog.RecipeId;
                request.Blog.CreatedDate = blog.CreatedDate;
                request.Blog.Reaction = blog.Reaction;
                request.Blog.View = blog.View;
                request.Blog.MinutesToCook = request.Blog.MinutesToCook == null ? blog.MinutesToCook : request.Blog.MinutesToCook;
                request.Blog.IsEvent = request.Blog.IsEvent == null ? false : request.Blog.IsEvent;
                request.Blog.EventExpiredDate = request.Blog.IsEvent.Value ? request.Blog.EventExpiredDate : null;
                await _blogRepository.UpdateAsync(request.Blog);
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateBlog: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #region Delete and Restore blog
        // blog, recipe, blogsubcate, comment, blogReaction, Accomplishment, blogReference: OFF
        // 2 api delete: blog và recipe
        // 2 api restore: blog và recipe
        // bỏ chung recipe và blog vô 1 region
        // logic: tắt hết những status các bảng liên quan!!!
        public async Task DeleteBlog(Guid id)
        {
            try
            {
                #region update Blog status into 0 > throw Error if not existed
                Blog removedBlog = null;

                removedBlog = await _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId.Equals(id) && x.BlogStatus == 1);
                if (removedBlog == null)
                    throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);

                removedBlog.BlogStatus = 0;
                #endregion

                #region update Recipe status into 0 > throw Error if not existed
                Recipe removedRecipe = await _recipeRepository.GetFirstOrDefaultAsync(recipe => recipe.RecipeId == id && recipe.Status == 1);
                if (removedRecipe == null)
                    throw new Exception(ErrorMessage.RecipeError.RECIPE_NOT_FOUND);

                removedRecipe.Status = 0;
                await _blogRepository.UpdateAsync(removedBlog); // update ở đây thì sure là ko có bị throw exception rồi
                await _recipeRepository.UpdateAsync(removedRecipe);
                #endregion

                #region update RecipeDetails status into 0
                ICollection<RecipeDetail> recipeDetails = await _recipeDetailRepository.GetRecipeDetailsBy(item => item.RecipeId == id && item.Status == 1);
                if (recipeDetails != null)
                {
                    foreach (var item in recipeDetails.ToList())
                    {
                        item.Status = 0;
                    }
                    await _recipeDetailRepository.UpdateRangeAsync(recipeDetails);
                }
                #endregion

                #region update BlogSubCates status into 0
                ICollection<BlogSubCate> blogSubCates = await _blogSubCateRepository.GetBlogSubCatesBy(item => item.BlogId == id && item.Status.Value);
                if (blogSubCates != null)
                {
                    foreach (var item in blogSubCates.ToList())
                    {
                        item.Status = false;
                    }
                    await _blogSubCateRepository.UpdateRangeAsync(blogSubCates);
                }
                #endregion

                #region update Comments status into 0
                ICollection<Comment> comments = await _commentRepository.GetCommentsBy(c => c.BlogId == id && c.Status.Value);
                if (comments != null)
                {
                    foreach (var item in comments.ToList())
                    {
                        item.Status = false;
                    }
                    await _commentRepository.UpdateRangeAsync(comments);
                }
                #endregion

                #region update BlogReactions status into 0
                ICollection<BlogReaction> blogReactions = await _blogReactionRepository.GetBlogReactionsBy(b => b.BlogId == id && b.Status.Value);
                if (blogReactions != null)
                {
                    foreach (var item in blogReactions.ToList())
                    {
                        item.Status = false;
                    }
                    await _blogReactionRepository.UpdateRangeAsync(blogReactions);
                }
                #endregion

                #region update Accomplishments status into 0
                ICollection<Accomplishment> accomplishments = await _accomplishmentRepository.GetAccomplishmentsBy(ac => ac.BlogId == id && ac.Status == 1);
                if (accomplishments != null)
                {
                    foreach (var item in accomplishments.ToList())
                    {
                        item.Status = 0;
                    }
                    await _accomplishmentRepository.UpdateRangeAsync(accomplishments);
                }
                #endregion

                #region update BlogReferences status into 0
                ICollection<BlogReference> blogReferences = await _blogReferenceRepository.GetBlogReferencesBy(br => br.BlogId == id && br.Status == 1);
                if (blogReferences != null)
                {
                    foreach (var item in blogReferences.ToList())
                    {
                        item.Status = 0;
                    }
                    await _blogReferenceRepository.UpdateRangeAsync(blogReferences);
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteBlog: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task RestoreBlog(Guid id)
        {
            try
            {
                #region update Blog status into 1 > throw Error if not existed
                Blog restoredBlog = null;
                restoredBlog = await _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId.Equals(id) && x.BlogStatus == 0);
                if (restoredBlog == null)
                    throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);

                restoredBlog.BlogStatus = 1;
                #endregion 

                #region update Recipe status into 1 > throw Error if not existed
                Recipe restoredRecipe = await _recipeRepository.GetFirstOrDefaultAsync(recipe => recipe.RecipeId == id && recipe.Status == 0);
                if (restoredRecipe == null)
                    throw new Exception(ErrorMessage.RecipeError.RECIPE_NOT_FOUND);

                restoredRecipe.Status = 1;
                await _blogRepository.UpdateAsync(restoredBlog);
                await _recipeRepository.UpdateAsync(restoredRecipe);
                #endregion

                #region update RecipeDetails status into 1
                ICollection<RecipeDetail> recipeDetails = await _recipeDetailRepository.GetRecipeDetailsBy(item => item.RecipeId == id && item.Status == 0);
                if (recipeDetails != null)
                {
                    foreach (var item in recipeDetails.ToList())
                    {
                        item.Status = 1;
                    }
                    await _recipeDetailRepository.UpdateRangeAsync(recipeDetails);
                }
                #endregion

                #region update BlogSubCate status into 1
                ICollection<BlogSubCate> blogSubCates = await _blogSubCateRepository.GetBlogSubCatesBy(item => item.BlogId == id && !item.Status.Value);
                if (blogSubCates != null)
                {
                    foreach (var item in blogSubCates.ToList())
                    {
                        item.Status = true;
                    }
                    await _blogSubCateRepository.UpdateRangeAsync(blogSubCates);
                }
                #endregion

                #region update Comments status into 1
                ICollection<Comment> comments = await _commentRepository.GetCommentsBy(c => c.BlogId == id && !c.Status.Value);
                if (comments != null)
                {
                    foreach (var item in comments.ToList())
                    {
                        item.Status = true;
                    }
                    await _commentRepository.UpdateRangeAsync(comments);
                }
                #endregion

                #region update BlogReaction status into 1
                ICollection<BlogReaction> blogReactions = await _blogReactionRepository.GetBlogReactionsBy(b => b.BlogId == id && !b.Status.Value);
                if (blogReactions != null)
                {
                    foreach (var item in blogReactions.ToList())
                    {
                        item.Status = true;
                    }
                    await _blogReactionRepository.UpdateRangeAsync(blogReactions);
                }
                #endregion

                #region update Accomplishment status into 1
                ICollection<Accomplishment> accomplishments = await _accomplishmentRepository.GetAccomplishmentsBy(ac => ac.BlogId == id && ac.Status == 0);
                if (accomplishments != null)
                {
                    foreach (var item in accomplishments.ToList())
                    {
                        item.Status = 1;
                    }
                    await _accomplishmentRepository.UpdateRangeAsync(accomplishments);
                }
                #endregion

                #region update BlogReference status into 1
                ICollection<BlogReference> blogReferences = await _blogReferenceRepository.GetBlogReferencesBy(br => br.BlogId == id && br.Status == 0);
                if (blogReferences != null)
                {
                    foreach (var item in blogReferences.ToList())
                    {
                        item.Status = 1;
                    }
                    await _blogReferenceRepository.UpdateRangeAsync(blogReferences);
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at RestoreBlog: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        #endregion

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

        private List<RecipeDetailResponse> ConvertToRecipeDetailResponse(Guid blogId, List<RecipeDetail> recipeDetails)
        {
            List<RecipeDetailResponse> list = new List<RecipeDetailResponse>();

            foreach (var recipeDetail in recipeDetails)
            {
                if (recipeDetail.RecipeId == blogId)
                {
                    RecipeDetailResponse tmp = new RecipeDetailResponse()
                    {
                        IngredientId = recipeDetail.IngredientId,
                        IngredientName = recipeDetail.Ingredient.Name,
                        Description = recipeDetail.Description,
                        Quantity = recipeDetail.Quantity,
                        Kcal = recipeDetail.Ingredient.Kcal,
                        Price = recipeDetail.Ingredient.Price,
                        Image = recipeDetail.Ingredient.Picture
                    };
                    list.Add(tmp);
                }
            }
            return list;
        }

        #endregion

        #region Blog Detail
        public async Task<BlogDetailResponse> GetBlogDetail(Guid blogId)
        {
            BlogDetailResponse result = null;
            try
            {
                var blog = await _blogRepository.GetFirstOrDefaultAsync(includeProperties: "Recipe");

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
                    View = ++blog.View,
                    TotalKcal = blog.Recipe.TotalKcal,
                    BlogStatus = blog.BlogStatus,
                    RecipeId = (Guid)blog.RecipeId,
                    RecipeTitle = blog.Recipe.Title,
                    RecipeImageURL = blog.Recipe.ImageUrl,
                    MaxSize = blog.Recipe.MaxSize,
                    MinSize = blog.Recipe.MinSize,
                    MinutesToCook = blog.MinutesToCook,
                    PackagePrice = blog.Recipe.PackagePrice,
                    CookedPrice = blog.Recipe.CookedPrice,
                    IsEvent = blog.IsEvent.HasValue ? blog.IsEvent.Value : null,
                    EventExpiredDate = blog.IsEvent.HasValue ? blog.EventExpiredDate : null
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
                result.RecipeDetails = _recipeDetailRepository.GetRecipeDetailsBy(x => x.RecipeId == result.RecipeId, includeProperties: "Ingredient").Result
                    .Select(x => new RecipeDetailResponse
                    {
                        IngredientId = x.IngredientId,
                        IngredientName = x.Ingredient.Name,
                        Description = x.Description,
                        Quantity = x.Quantity,
                        Kcal = x.Ingredient.Kcal,
                        Price = x.Ingredient.Price,
                        Image = x.Ingredient.Picture
                    }).ToList();
                result.RelatedBlogs = await GetRelatedBlogs(result.BlogId);
                await _blogRepository.UpdateAsync(blog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogDetails: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<BlogDetailResponse> GetBlogDetailPreview(Guid blogId)
        {
            BlogDetailResponse result = null;
            try
            {
                var blog = await _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId == blogId, includeProperties: "Recipe");

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
                    TotalKcal = blog.Recipe.TotalKcal,
                    BlogStatus = blog.BlogStatus,
                    RecipeId = (Guid)blog.RecipeId,
                    RecipeTitle = blog.Recipe.Title,
                    RecipeImageURL = blog.Recipe.ImageUrl,
                    MaxSize = blog.Recipe.MaxSize,
                    MinSize = blog.Recipe.MinSize,
                    MinutesToCook = blog.MinutesToCook,
                    PackagePrice = blog.Recipe.PackagePrice,
                    CookedPrice = blog.Recipe.CookedPrice,
                    IsEvent = blog.IsEvent.Value,
                    EventExpiredDate = blog.IsEvent.Value ? blog.EventExpiredDate : null
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

                var author = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId == blog.AuthorId);
                result.AuthorName = author.Firstname + " " + author.Lastname;

                // List SubCates
                result.SubCates = _blogSubCateRepository.GetBlogSubCatesBy(x => x.BlogId == blog.BlogId, includeProperties: "SubCate").Result.Select(x => new SubCateResponse
                {
                    SubCateId = x.SubCateId,
                    Name = x.SubCate.Name
                }).ToList();

                // List RecipeDetails & List Ingredients
                result.RecipeDetails = _recipeDetailRepository.GetRecipeDetailsBy(x => x.RecipeId == result.RecipeId, includeProperties: "Ingredient").Result
                    .Select(x => new RecipeDetailResponse
                    {
                        IngredientId = x.IngredientId,
                        IngredientName = x.Ingredient.Name,
                        Description = x.Description,
                        Quantity = x.Quantity,
                        Kcal = x.Ingredient.Kcal,
                        Price = x.Ingredient.Price
                    }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogDetails: " + ex.Message);
                throw;
            }
            return result;
        }

        // Lấy ra 3 bài blog liên quan tới bài blog hiện tại đang xem (Trang BlogDetail)
        private async Task<List<BlogsByCatesResponse>> GetRelatedBlogs(Guid blogId)
        {
            List<BlogsByCatesResponse> list = new List<BlogsByCatesResponse>();
            try
            {
                var currentBlog = await _blogRepository.GetFirstOrDefaultAsync(b => b.BlogId == blogId
                                                                             && b.BlogStatus.Value == (int)Status.BlogStatus.ACTIVE,
                                                                             includeProperties: "BlogSubCates");
                if (currentBlog == null)
                    throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);
                var relatedBlogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus == 1 && b.BlogId != currentBlog.BlogId);
                var listBlogSubCates = await _blogSubCateRepository.GetAll(includeProperties: "SubCate");

                // get description text
                var listBlogDescRef = _blogReferenceRepository.
                                                GetBlogReferencesBy(x => x.Type == (int)BlogReferenceType.DESCRIPTION)
                                                .Result
                                                .Select(x => new
                                                {
                                                    x.Text,
                                                    x.BlogId
                                                });

                Random random = new Random();
                list = relatedBlogs
                            .Join(listBlogSubCates, blog => blog.BlogId, bsc => bsc.BlogId,
                            (blog, bsc) => new
                            {
                                blog,
                                bsc
                            })
                            .Where(x => x.bsc.SubCateId == currentBlog.BlogSubCates.ElementAt(0).SubCateId && x.bsc.Status != false).ToList()
                            .Join(listBlogDescRef, b => b.blog.BlogId, y => y.BlogId,
                            (b, y) => new BlogsByCatesResponse
                            {
                                BlogId = b.blog.BlogId,
                                CreatedDate = b.blog.CreatedDate,
                                Title = b.blog.Title,
                                ImageUrl = b.blog.ImageUrl,
                                Reaction = b.blog.Reaction,
                                View = b.blog.View,
                                Description = y.Text
                            }).OrderBy(b => random.Next()).Take(3).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetRelatedBlogs: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return list;
        }
        #endregion

        #region Approve - Reject blog
        public async Task<bool> ApproveRejectBlog(string type, Guid blogId)
        {
            bool isChecked = false;
            try
            {
                var blog = await _blogRepository.GetFirstOrDefaultAsync(b => b.BlogId == blogId && b.BlogStatus == (int)Status.BlogStatus.PENDING, includeProperties: "Author,Recipe,BlogReferences");
                var properties = blog.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (blog != null)
                {


                    if (type.Equals("APPROVE"))
                    {
                        foreach (var property in properties)
                        {
                            if (property.Name != "VideoUrl" && property.Name != "IsEvent" && property.Name != "EventExpiredDate" && property.GetValue(blog) == null || blog.BlogReferences.Count == 0)
                            {
                                return isChecked = false;
                            }

                        }


                        blog.BlogStatus = (int)Status.BlogStatus.ACTIVE;
                        await UpdateStatusWhenApproveRejectBlog(blog.BlogId, blog.RecipeId.Value, (int)Status.BlogStatus.PENDING, (int)Status.BlogStatus.DRAFTED);
                    }
                    else
                    {
                        blog.BlogStatus = (int)Status.BlogStatus.DRAFTED;
                        await UpdateStatusWhenApproveRejectBlog(blog.BlogId, blog.RecipeId.Value, (int)Status.BlogStatus.PENDING, (int)Status.BlogStatus.DRAFTED);
                    }
                    await _blogRepository.UpdateAsync(blog);
                    isChecked = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at ApproveRejectBlog: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isChecked;
        }

        #endregion

        private async Task UpdateStatusWhenApproveRejectBlog(Guid blogId, Guid recipeId, int oldStatus, int newStatus)
        {
            try
            {
                #region update Recipe status old into new > throw Error if not existed
                Recipe approvedRecipe = await _recipeRepository.GetFirstOrDefaultAsync(recipe => recipe.RecipeId == blogId
                                                                                        && recipe.Status == oldStatus);
                if (approvedRecipe == null)
                    throw new Exception(ErrorMessage.RecipeError.RECIPE_NOT_FOUND);

                approvedRecipe.Status = newStatus;
                await _recipeRepository.UpdateAsync(approvedRecipe);
                #endregion

                #region update RecipeDetails status old into new
                ICollection<RecipeDetail> recipeDetails = await _recipeDetailRepository.GetRecipeDetailsBy(
                                                                                            item => item.RecipeId == recipeId
                                                                                                && item.Status == oldStatus);
                if (recipeDetails != null)
                {
                    foreach (var item in recipeDetails.ToList())
                    {
                        item.Status = newStatus;
                    }
                    await _recipeDetailRepository.UpdateRangeAsync(recipeDetails);
                }
                #endregion

                #region update BlogReferences status old into new
                ICollection<BlogReference> blogReferences = await _blogReferenceRepository.GetBlogReferencesBy(br => br.BlogId == blogId
                                                                                                                && br.Status == oldStatus);
                if (blogReferences != null)
                {
                    foreach (var item in blogReferences.ToList())
                    {
                        item.Status = newStatus;
                    }
                    await _blogReferenceRepository.UpdateRangeAsync(blogReferences);
                }
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateStatusWhenApproveRejectBlog: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
