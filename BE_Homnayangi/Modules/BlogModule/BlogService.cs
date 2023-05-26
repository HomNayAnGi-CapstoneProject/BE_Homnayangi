using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Request;
using BE_Homnayangi.Modules.BlogModule.Response;
using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using BE_Homnayangi.Modules.BlogReferenceModule.Interface;
using BE_Homnayangi.Modules.BlogSubCateModule.Interface;
using BE_Homnayangi.Modules.CommentModule.Interface;
using BE_Homnayangi.Modules.NotificationModule.Interface;
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
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
using BE_Homnayangi.Modules.PackageModule.Interface;
using BE_Homnayangi.Modules.PackageDetailModule.Interface;
using GSF;
using BE_Homnayangi.Modules.IngredientModule.Interface;

namespace BE_Homnayangi.Modules.BlogModule
{
    public class BlogService : IBlogService
    {
        #region Define repository + Constructor
        private readonly IBlogRepository _blogRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IBlogSubCateRepository _blogSubCateRepository;
        private readonly IPackageDetailRepository _packageDetailRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBlogReferenceRepository _blogReferenceRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogReactionRepository _blogReactionRepository;
        private readonly IAccomplishmentRepository _accomplishmentRepository;
        private readonly ICaloReferenceRepository _caloReferenceRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IHubContext<SignalRServer> _hubContext;

        public BlogService(IBlogRepository blogRepository, IPackageRepository packageRepository, IBlogSubCateRepository blogSubCateRepository,
            ISubCateRepository subCateRepository, IPackageDetailRepository packageDetailRepository,
            IUserRepository userRepository, IBlogReferenceRepository blogReferenceRepository, ICommentRepository commentRepository,
            IBlogReactionRepository blogReactionRepository, IAccomplishmentRepository accomplishmentRepository,
            ICaloReferenceRepository caloReferenceRepository, IIngredientRepository ingredientRepository,
            INotificationRepository notificationRepository,
            IHubContext<SignalRServer> hubContext)
        {
            _blogRepository = blogRepository;
            _packageRepository = packageRepository;
            _blogSubCateRepository = blogSubCateRepository;
            _packageDetailRepository = packageDetailRepository;
            _userRepository = userRepository;
            _ingredientRepository = ingredientRepository;
            _blogReferenceRepository = blogReferenceRepository;
            _commentRepository = commentRepository;
            _blogReactionRepository = blogReactionRepository;
            _accomplishmentRepository = accomplishmentRepository;
            _caloReferenceRepository = caloReferenceRepository;
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
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
                    if (isEvent == null) // Lấy hết
                    {
                        pendingBlogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus == (int)Status.BlogStatus.PENDING,
                                                                                                    includeProperties: "Author");
                    }
                    else if (isEvent.Value) // Lấy event
                    {
                        pendingBlogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus == (int)Status.BlogStatus.PENDING && b.IsEvent.Value,
                                                                                                    includeProperties: "Author");
                    }
                    else
                    {
                        pendingBlogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus == (int)Status.BlogStatus.PENDING && !(b.IsEvent != null && b.IsEvent.Value),
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
                            TotalKcal = blog.TotalKcal,
                            IsEvent = blog.IsEvent.HasValue ? blog.IsEvent.Value : false,
                            EventExpiredDate = blog.IsEvent.HasValue ? blog.EventExpiredDate : null
                        }).OrderByDescending(b => b.CreatedDate).ToList();
                }
                else if (isPending != null && !isPending.Value) // get !pending blogs
                {
                    ICollection<Blog> blogs = new List<Blog>();

                    if (isEvent == null)    // get all
                        blogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus != (int)Status.BlogStatus.PENDING, includeProperties: "Author");
                    else if (isEvent.Value) // only event
                        blogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus != (int)Status.BlogStatus.PENDING && b.IsEvent != null && b.IsEvent.Value, includeProperties: "Author");
                    else                    // only blog
                        blogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus != (int)Status.BlogStatus.PENDING && !(b.IsEvent != null && b.IsEvent.Value), includeProperties: "Author");

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
                                TotalKcal = blog.TotalKcal,
                                IsEvent = blog.IsEvent.HasValue ? blog.IsEvent.Value : false,
                                EventExpiredDate = blog.IsEvent.HasValue ? blog.EventExpiredDate : null
                            }).OrderByDescending(b => b.CreatedDate).ToList();
                    }
                }
                else // isPending: null > get all
                {

                    ICollection<Blog> blogs = new List<Blog>();

                    if (isEvent == null)    // get all
                        blogs = await _blogRepository.GetBlogsBy(includeProperties: "Author");
                    else if (isEvent.Value) // only event
                        blogs = await _blogRepository.GetBlogsBy(b => b.IsEvent != null && b.IsEvent.Value, includeProperties: "Author");
                    else                    // only blog
                        blogs = await _blogRepository.GetBlogsBy(b => !(b.IsEvent != null && b.IsEvent.Value), includeProperties: "Author");


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
                                TotalKcal = blog.TotalKcal,
                                IsEvent = blog.IsEvent.HasValue ? blog.IsEvent.Value : false,
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
                }).Join(await _packageRepository.GetNItemRandom(x => x.PackagePrice >= ((decimal)Price.PriceEnum.MIN)
                && x.PackagePrice <= ((decimal)Price.PriceEnum.MAX) && x.Size == 2, numberItem: (int)NumberItem.NumberItemRandomEnum.CHEAP_PRICE),
                    x => x.b.BlogId, y => y.BlogId, (x, y) => new
                    {
                        BlogId = x.b.BlogId,
                        Title = x.b.Title,
                        ImageUrl = x.b.ImageUrl,
                        View = x.b.View,
                        Reaction = x.b.BlogReactions,
                        ListSubCateName = x.ListSubCateName,
                        PackagePrice = y.PackagePrice,
                        TotalKcal = x.b.TotalKcal == null ? 0 : x.b.TotalKcal
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
                        PackagePrice = (decimal)x.x.PackagePrice,
                        TotalKcal = x.x.TotalKcal == null ? 0 : (int)x.x.TotalKcal
                    }).ToList();

                return listResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogsSortByPackagePriceAsc: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<SearchBlogsResponse>> GetBlogAndPackageByName(String name)
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

        public async Task<ICollection<OverviewBlogResponse>> GetBlogsBySubCateForHomePage(Guid? Id, bool isRegion, int numberOfItems = 0)
        {
            try
            {
                var listBlogSubCate = await _blogSubCateRepository.GetAll(includeProperties: "SubCate");
                var listBlogs = isRegion == false ? await _blogRepository.GetBlogsBy(x => x.CookingMethodId == Id && x.BlogStatus == 1)
                    : await _blogRepository.GetBlogsBy(x => x.RegionId == Id && x.BlogStatus == 1);

                listBlogs = numberOfItems > 0
                    ? listBlogs.OrderByDescending(x => x.CreatedDate).Take(numberOfItems).ToList()
                    : listBlogs.OrderByDescending(x => x.CreatedDate).ToList();

                var listSubCateName = GetListSubCateName(listBlogs, listBlogSubCate);

                return null;
            }
            catch (Exception ex)
            {
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
                ICollection<Blog> blogs = new List<Blog>();

                if (subCateIds == null)
                {

                    if (request.IsEvent == null)    // get all
                        blogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus == ((int)Status.BlogStatus.ACTIVE), includeProperties: "Author");
                    else if (request.IsEvent.Value) // only event
                        blogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus == (int)Status.BlogStatus.ACTIVE && b.IsEvent != null && b.IsEvent.Value, includeProperties: "Author");
                    else                    // only blog
                        blogs = await _blogRepository.GetBlogsBy(b => b.BlogStatus == (int)Status.BlogStatus.ACTIVE && !(b.IsEvent != null && b.IsEvent.Value), includeProperties: "Author");

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
                                                                                && b.Blog.BlogStatus == (int)Status.BlogStatus.ACTIVE && b.Blog.IsEvent.HasValue && b.Blog.IsEvent.Value).ToList();
                        },
                                                                                includeProperties: "Blog");
                    }
                    else // Lấy blog thôi
                    {
                        filteredBlogs = await _blogSubCateRepository
                        .GetBlogSubCatesBy(options: (bs) =>
                        {
                            return bs.Where(b => subCateIds.Contains(b.SubCateId.ToString())
                                                                                && b.Blog.BlogStatus == (int)Status.BlogStatus.ACTIVE && !(b.Blog.IsEvent.HasValue && b.Blog.IsEvent.Value)).ToList();
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
                        Title = b.Title,
                        Description = y.Html,
                        ImageUrl = b.ImageUrl,
                        CreatedDate = b.CreatedDate,
                        Reaction = b.Reaction,
                        View = b.View,
                        IsEvent = b.IsEvent.HasValue ? b.IsEvent.Value : false,
                        EventExpiredDate = b.IsEvent.HasValue && b.IsEvent.Value ? b.EventExpiredDate.Value : null

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

        public async Task<SuggestBlogResponse> GetSuggestBlogByCalo(SuggestBlogByCaloRequest request)
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
                var listBlog = await _blogRepository.GetBlogsBy(x => x.MaxSize == 2 && x.BlogStatus == ((int)BlogStatus.ACTIVE));
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

                //take 3 blog match the suggest calo
                int count = 0;
                do
                {
                    ++count;
                    var firstBlog = listNormalBlog.ElementAt(rnd.Next(0, listNormalBlog.Count() - 1));
                    var secondBlog = listNormalBlog.ElementAt(rnd.Next(0, listNormalBlog.Count() - 1));
                    var soupBlog = listSoupBlog.ElementAt(rnd.Next(0, listSoupBlog.Count() - 1));
                    if (firstBlog.BlogId != secondBlog.BlogId)
                    {
                        if (!listSoupBlogIdSubCate.Contains(firstBlog.BlogId) && !listSoupBlogIdSubCate.Contains(secondBlog.BlogId))
                        {
                            if (request.IsLoseWeight == true)
                            {
                                if (suggestCalo.Calo > (firstBlog.TotalKcal + secondBlog.TotalKcal + soupBlog.TotalKcal))
                                {
                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = firstBlog.BlogId,
                                        Title = firstBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == firstBlog.BlogId).Html,
                                        ImageUrl = firstBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(firstBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        TotalKcal = (int)firstBlog.TotalKcal
                                    });

                                    result.Add(new OverviewBlogResponse
                                    {

                                        BlogId = secondBlog.BlogId,
                                        Title = secondBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == secondBlog.BlogId).Html,
                                        ImageUrl = secondBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(secondBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        TotalKcal = (int)secondBlog.TotalKcal
                                    });

                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = soupBlog.BlogId,
                                        Title = soupBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == soupBlog.BlogId).Html,
                                        ImageUrl = soupBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(soupBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        TotalKcal = (int)soupBlog.TotalKcal
                                    });

                                }
                            }
                            else
                            {
                                if ((firstBlog.TotalKcal + secondBlog.TotalKcal + soupBlog.TotalKcal) > suggestCalo.Calo)
                                {
                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = firstBlog.BlogId,
                                        Title = firstBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == firstBlog.BlogId).Html,
                                        ImageUrl = firstBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(firstBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        TotalKcal = (int)firstBlog.TotalKcal
                                    });
                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = secondBlog.BlogId,
                                        Title = secondBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == secondBlog.BlogId).Html,
                                        ImageUrl = secondBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(secondBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        TotalKcal = (int)secondBlog.TotalKcal
                                    });
                                    result.Add(new OverviewBlogResponse
                                    {
                                        BlogId = soupBlog.BlogId,
                                        Title = soupBlog.Title,
                                        Description = listBlogDescRef.FirstOrDefault(x => x.BlogId == soupBlog.BlogId).Html,
                                        ImageUrl = soupBlog.ImageUrl,
                                        ListSubCateName = listBlogSubCate.Where(x => x.BlogId.Equals(soupBlog.BlogId)).Select(x => x.SubCate.Name).ToList(),
                                        TotalKcal = (int)soupBlog.TotalKcal
                                    });
                                }
                            }
                        }
                    }
                } while (result.Count() < 3 && count < 150);
                SuggestBlogResponse suggestBlogResponse = new SuggestBlogResponse
                {
                    Calo = suggestCalo.Calo,
                    SuggestBlogs = result
                };
                return suggestBlogResponse;
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
                    var packageDetails = await _packageDetailRepository.GetNItemRandom(rd => rd.IngredientId == ingredientId,
                                                                                                        numberItem: 8);

                    var packages = _packageRepository.GetPackagesBy(b => b.Status == ((int)Status.BlogStatus.ACTIVE),
                                                                 includeProperties: "Blog").Result.ToList();
                    var tmpList = packageDetails.Join(packages, x => x.PackageId, y => y.PackageId, (x, y) => new BlogsByCatesResponse()
                    {
                        BlogId = (Guid)y.BlogId,
                        Title = y.Blog.Title,
                        // Description is below
                        ImageUrl = y.Blog.ImageUrl,
                        Reaction = y.Blog.Reaction,
                        View = y.Blog.View,
                        CreatedDate = y.Blog.CreatedDate,
                    }).ToList();

                    #region Check 8 packages distinct blogId
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
                        Title = b.Title,
                        Description = y.Html,
                        ImageUrl = b.ImageUrl,
                        CreatedDate = b.CreatedDate,
                        Reaction = b.Reaction,
                        View = b.View
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
                                                                includeProperties: "Author");
                }
                else if (isExpired.Value) // lấy hết hạn
                {
                    events = await _blogRepository.GetBlogsBy(b => b.IsEvent.Value
                                                                    && b.EventExpiredDate != null && b.EventExpiredDate <= DateTime.Now,
                                                                    includeProperties: "Author");
                }
                else // lấy chưa hết hạn
                {
                    events = await _blogRepository.GetBlogsBy(b => b.IsEvent.Value
                                                                    && b.EventExpiredDate != null && b.EventExpiredDate > DateTime.Now,
                                                                    includeProperties: "Author");
                }

                if (events.Count > 0)
                {
                    result = events.Select(e => new OverviewBlog()
                    {
                        BlogId = e.BlogId,
                        Title = e.Title,
                        ImageUrl = e.ImageUrl,
                        View = e.View,
                        Reaction = e.Reaction,
                        CreatedDate = e.CreatedDate.Value,
                        TotalKcal = e.TotalKcal,
                        Status = e.BlogStatus,
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
                // add an empty blog
                Blog blog = new Blog()
                {
                    BlogId = Guid.NewGuid(),
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
                        options: (l) => l.AsNoTracking().ToList())
                    .Result
                    .FirstOrDefault();

                if (blog == null)
                    throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);
                #endregion

                #region update packages and package details
                var packages = new List<Package>();
                var packageDetails = new List<PackageDetail>();
                foreach (var item in request.Packages)
                {
                    var packageAddMapFirst = new Package();
                    var packageAddMapSecond = new Package();
                    var packageDetailList = new List<PackageDetail>();
                    packageAddMapFirst.PackageId = item.Item1.PackageId;
                    packageAddMapFirst.Title = item.Item1.Title;
                    packageAddMapFirst.ImageUrl = item.Item1.ImageUrl;
                    packageAddMapFirst.Size = item.Item1.Size;
                    packageAddMapFirst.BlogId = blog.BlogId;
                    packageAddMapFirst.Status = blog.BlogStatus;
                    packageAddMapFirst.CreatedDate = DateTime.Now;

                    packageAddMapSecond.PackageId = item.Item1.CookedId;
                    packageAddMapSecond.Title = item.Item1.Title;
                    packageAddMapSecond.ImageUrl = item.Item1.ImageUrl;
                    packageAddMapSecond.Size = item.Item1.Size;
                    packageAddMapSecond.BlogId = blog.BlogId;
                    packageAddMapSecond.Status = blog.BlogStatus;
                    packageAddMapSecond.CreatedDate = DateTime.Now;

                    packageAddMapFirst.PackagePrice = item.Item1.PackagePrice;
                    packageAddMapFirst.IsCooked = false;

                    packageAddMapSecond.PackagePrice = item.Item1.CookedPrice;
                    packageAddMapSecond.IsCooked = true;

                    packages.Add(packageAddMapFirst);
                    packages.Add(packageAddMapSecond);
                    foreach (var subItem in item.Item2)
                    {
                        packageDetailList.Add(new PackageDetail
                        {
                            PackageId = packageAddMapFirst.PackageId,
                            IngredientId = subItem.IngredientId,
                            Description = subItem.Description,
                            Quantity = subItem.Quantity
                        });
                        packageDetailList.Add(new PackageDetail
                        {
                            PackageId = packageAddMapSecond.PackageId,
                            IngredientId = subItem.IngredientId,
                            Description = subItem.Description,
                            Quantity = subItem.Quantity
                        });
                    }
                    packageDetails.AddRange(packageDetailList);
                }

                var dbAllPackageDetails = await _packageDetailRepository
                    .GetAll(options: (l) => l.AsNoTracking().ToList());

                var dbPackages = await _packageRepository.GetPackagesBy(x => x.BlogId == blog.BlogId, options: (l) => l.AsNoTracking().ToList());
                var dbPackagesId = dbPackages.Select(x => x.PackageId).ToList();
                var dbPackageDetails = dbPackages.Join(dbAllPackageDetails, x => x.PackageId, y => y.PackageId, (x, y) => y).ToList();

                var updatedPackage = new List<Package>();
                foreach (var item in packages)
                {
                    if (dbPackagesId.Contains(item.PackageId))
                    {
                        var changedPackage = dbPackages.First(x => x.PackageId == item.PackageId);
                        changedPackage.Title = item.Title;
                        changedPackage.ImageUrl = item.ImageUrl;
                        changedPackage.Size = item.Size;
                        changedPackage.PackagePrice = item.PackagePrice;
                        changedPackage.IsCooked = item.IsCooked;
                        updatedPackage.Add(changedPackage);
                    }
                }
                if (updatedPackage.Count() > 0) _packageRepository.UpdateRange(updatedPackage);

                var addedPackage = packages.Select(xx => xx.PackageId).Where(x => !dbPackages.Select(y => y.PackageId).Contains(x)).ToList();

                if (addedPackage.Count() > 0)
                {
                    var addedPackageToDb = packages.Where(x => addedPackage.Contains(x.PackageId)).ToList();
                    _packageRepository.AddRange(addedPackageToDb);
                }

                // check if leftover then remove
                var deletedPackage = dbPackages.Select(x => x.PackageId).Where(x => !packages.Select(y => y.PackageId).Contains(x)).ToList();

                if (deletedPackage.Count() > 0)
                {
                    var deletedPackageFromDb = packages.Where(x => deletedPackage.Contains(x.PackageId)).ToList();
                    _packageRepository.RemoveRange(deletedPackageFromDb);
                }

                // check if exist then update, else add
                var updatedPackageDetail = new List<PackageDetail>();
                foreach (var item in packageDetails)
                {
                    if (dbPackageDetails.Contains(item))
                    {
                        var changedPackageDetail = dbPackageDetails.Find(x => x.PackageId == item.PackageId && x.IngredientId == item.IngredientId);
                        changedPackageDetail.Quantity = item.Quantity;
                        changedPackageDetail.Description = item.Description;
                        updatedPackageDetail.Add(changedPackageDetail);
                    }
                }
                if (updatedPackageDetail.Count() > 0) _packageDetailRepository.UpdateRange(updatedPackageDetail);
                // check if leftover then remove

                var deletedPackageDetail = dbPackageDetails.Except(packageDetails).ToList();

                if (deletedPackageDetail.Count() > 0) _packageDetailRepository.RemoveRange(deletedPackageDetail);

                var addedPackageDetail = packageDetails.Except(dbPackageDetails).ToList();

                if (addedPackageDetail.Count() > 0) _packageDetailRepository.AddRange(addedPackageDetail);

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
                        var newBlogSubCate = new BlogSubCate
                        {
                            BlogId = (Guid)b.BlogId,
                            SubCateId = (Guid)b.SubCateId,
                            CreatedDate = DateTime.Now,
                            Status = true
                        };
                        await _blogSubCateRepository.AddAsync(newBlogSubCate);
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
                blog.UpdatedDate = DateTime.Now;
                blog.Title = request.Blog.Title;
                blog.ImageUrl = request.Blog.ImageUrl;
                blog.BlogStatus = request.Blog.BlogStatus;
                blog.VideoUrl = request.Blog.VideoUrl;
                blog.MinutesToCook = request.Blog.MinutesToCook;
                blog.IsEvent = request.Blog.IsEvent;
                blog.EventExpiredDate = request.Blog.EventExpiredDate;
                blog.CookingMethodId = request.Blog.CookingMethodId;
                blog.RegionId = request.Blog.RegionId;
                await _blogRepository.UpdateAsync(blog);
                #endregion

                if (request.Blog.BlogStatus == (int)Status.BlogStatus.PENDING)
                {
                    #region Create notification
                    var notiId = Guid.NewGuid();
                    var noti = new Library.Models.Notification
                    {
                        NotificationId = notiId,
                        Description = $"Bài blog - {request.Blog.BlogId} đang được chờ duyệt",
                        CreatedDate = DateTime.Now,
                        Status = false,
                        ReceiverId = _userRepository.GetUsersBy(u => u.Role == 1).Result.FirstOrDefault().UserId
                    };
                    await _notificationRepository.AddAsync(noti);

                    await _hubContext.Clients.All.SendAsync("BlogPending", JsonConvert.SerializeObject(noti));
                    #endregion
                }

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

                #region update Package status into 0 > throw Error if not existed
                var removedPackages = await _packageRepository.GetPackagesBy(x => x.BlogId == id && x.Status == 1);
                if (removedPackages.Count() == 0)
                    throw new Exception(ErrorMessage.PackageError.PACKAGE_NOT_FOUND);

                var allPackageDetails = await _packageDetailRepository.GetAll();
                var removedPackageDetails = new List<PackageDetail>();
                foreach (var item in removedPackages)
                {
                    item.Status = 0;
                    removedPackageDetails.AddRange(allPackageDetails.Where(x => x.PackageId == item.PackageId && x.Status == 1).ToList());
                }
                foreach (var item in removedPackageDetails)
                {
                    item.Status = 0;
                }
                await _packageDetailRepository.UpdateRangeAsync(removedPackageDetails);
                await _packageRepository.UpdateRangeAsync(removedPackages);
                await _blogRepository.UpdateAsync(removedBlog); // update ở đây thì sure là ko có bị throw exception rồi
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
                var restoredPackages = await _packageRepository.GetPackagesBy(x => x.BlogId == id && x.Status == 0);
                if (restoredPackages.Count() == 0)
                    throw new Exception(ErrorMessage.PackageError.PACKAGE_NOT_FOUND);

                var allPackageDetails = await _packageDetailRepository.GetAll();
                var restoredPackageDetails = new List<PackageDetail>();
                foreach (var item in restoredPackages)
                {
                    item.Status = 1;
                    restoredPackageDetails.AddRange(allPackageDetails.Where(x => x.PackageId == item.PackageId && x.Status == 1).ToList());
                }
                foreach (var item in restoredPackageDetails)
                {
                    item.Status = 1;
                }
                await _packageDetailRepository.UpdateRangeAsync(restoredPackageDetails);
                await _packageRepository.UpdateRangeAsync(restoredPackages);
                await _blogRepository.UpdateAsync(restoredBlog);
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
                var listBlogReferences = await _blogReferenceRepository.GetBlogReferencesBy(x => x.BlogId == blogDraftRemove.BlogId);
                var listBlogSubCates = await _blogSubCateRepository.GetBlogSubCatesBy(x => x.BlogId == blogDraftRemove.BlogId);
                var packages = await _packageRepository.GetPackagesBy(x => x.BlogId == blogDraftRemove.BlogId);
                var allPackageDetails = await _packageDetailRepository.GetAll();
                var packageDetails = new List<PackageDetail>();
                foreach (var item in packages)
                {
                    packageDetails.AddRange(allPackageDetails.Where(x => x.PackageId == item.PackageId && x.Status == 1).ToList());
                }
                await _packageDetailRepository.RemoveRangeAsync(packageDetails);
                await _packageRepository.RemoveRangeAsync(packages);
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

        #endregion

        #region Blog Detail
        public async Task<BlogDetailResponse> GetBlogDetail(Guid blogId)
        {
            BlogDetailResponse result = null;
            try
            {
                var blog = await _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId == blogId);

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
                    TotalKcal = blog.TotalKcal,
                    BlogStatus = blog.BlogStatus,
                    MaxSize = blog.MaxSize,
                    MinSize = blog.MinSize,
                    MinutesToCook = blog.MinutesToCook,
                    IsEvent = blog.IsEvent.HasValue ? blog.IsEvent.Value : false,
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

                //List Packages
                var listPackages = await _packageRepository.GetPackagesBy(x => x.BlogId == result.BlogId);
                var test = listPackages.GroupBy(x => x.Size).Select(xx => new
                {
                    Size = xx.Key,
                    packageResponse = xx.Select(xx => xx.PackageId)
                });
                var allPackageDetail = await _packageDetailRepository.GetPackageDetailsBy(x => x.Package.BlogId == result.BlogId, includeProperties: "Ingredient");
                for (int i = 0; i < listPackages.Count(); i = i + 2)
                {
                    var packageResponse = new PackagesResponse
                    {
                        PackageId = listPackages.ElementAt(i).PackageId,
                        CookedPrice = listPackages.ElementAt(i + 1).PackagePrice,
                        PackagePrice = listPackages.ElementAt(i).PackagePrice,
                        PackageImageURL = listPackages.ElementAt(i).ImageUrl,
                        PackageTitle = listPackages.ElementAt(i).Title,
                        Size = (int)listPackages.ElementAt(i).Size
                    };
                    var listPackageDetailResponse = allPackageDetail.Where(x => x.PackageId == listPackages.ElementAt(i).PackageId).Select(x => new PackageDetailResponse
                    {
                        Description = x.Description,
                        IngredientId = x.IngredientId,
                        IngredientName = x.Ingredient.Name,
                        Quantity = x.Quantity,
                        Kcal = x.Ingredient.Kcal,
                        Price = x.Ingredient.Price,
                        Image = x.Ingredient.Picture
                    }).ToList();
                }

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
                var blog = await _blogRepository.GetFirstOrDefaultAsync(x => x.BlogId == blogId);

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
                    TotalKcal = blog.TotalKcal,
                    BlogStatus = blog.BlogStatus,
                    MaxSize = blog.MaxSize,
                    MinSize = blog.MinSize,
                    MinutesToCook = blog.MinutesToCook,
                    IsEvent = blog.IsEvent.HasValue ? blog.IsEvent.Value : false,
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

                var author = await _userRepository.GetFirstOrDefaultAsync(x => x.UserId == blog.AuthorId);
                result.AuthorName = author.Firstname + " " + author.Lastname;

                // List SubCates
                result.SubCates = _blogSubCateRepository.GetBlogSubCatesBy(x => x.BlogId == blog.BlogId, includeProperties: "SubCate").Result.Select(x => new SubCateResponse
                {
                    SubCateId = x.SubCateId,
                    Name = x.SubCate.Name
                }).ToList();

                //List Packages
                var listPackages = await _packageRepository.GetPackagesBy(x => x.BlogId == result.BlogId);
                var groupPackage = listPackages.GroupBy(x => x.Size).Select(xx => new
                {
                    Size = xx.Key,
                    packageResponse = xx.Select(xx => new { xx.PackageId, xx.IsCooked, xx.PackagePrice, xx.ImageUrl, xx.Title })
                });
                var allPackageDetail = await _packageDetailRepository.GetPackageDetailsBy(x => x.Package.BlogId == result.BlogId, includeProperties: "Ingredient");
                var ingredients = await _ingredientRepository.GetAll(includeProperties: "Type");
                foreach (var item in groupPackage)
                {
                    var packageResponse = new PackagesResponse
                    {
                        PackageId = item.packageResponse.First(x => x.IsCooked == false).PackageId,
                        CookedId = item.packageResponse.First(x => x.IsCooked == true).PackageId,
                        PackagePrice = item.packageResponse.First(x => x.IsCooked == false).PackagePrice,
                        CookedPrice = item.packageResponse.First(x => x.IsCooked == true).PackagePrice,
                        PackageImageURL = item.packageResponse.First(x => x.IsCooked == false).ImageUrl,
                        PackageTitle = item.packageResponse.First(x => x.IsCooked == false).Title,
                        Size = (int)item.Size
                    };
                    var listPackageDetailResponse = allPackageDetail.Where(x => x.PackageId == item.packageResponse.First(x => x.IsCooked == false).PackageId)
                        .Select(x => new
                        {
                            Description = x.Description,
                            IngredientId = x.IngredientId,
                            IngredientName = x.Ingredient.Name,
                            Quantity = x.Quantity,
                            Kcal = x.Ingredient.Kcal,
                            Price = x.Ingredient.Price,
                            Image = x.Ingredient.Picture
                        }).ToList().Join(ingredients, x => x.IngredientId, y => y.IngredientId, (x, y) => new PackageDetailResponse
                        {
                            Description = x.Description,
                            IngredientId = x.IngredientId,
                            IngredientName = x.IngredientName,
                            Quantity = x.Quantity,
                            Kcal = x.Kcal,
                            Price = x.Price,
                            Image = x.Image,
                            UnitName = y.Type.UnitName
                        }).ToList();
                    result.Packages.Add(new Tuple<PackagesResponse, List<PackageDetailResponse>>(packageResponse, listPackageDetailResponse));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetBlogDetailPreview: " + ex.Message);
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
                var blog = await _blogRepository.GetFirstOrDefaultAsync(b => b.BlogId == blogId && b.BlogStatus == (int)Status.BlogStatus.PENDING, includeProperties: "Author,BlogReferences");
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
                        await UpdateStatusWhenApproveRejectBlog(blog.BlogId, (int)Status.BlogStatus.PENDING, (int)Status.BlogStatus.ACTIVE);
                    }
                    else
                    {
                        blog.BlogStatus = (int)Status.BlogStatus.DRAFTED;
                        await UpdateStatusWhenApproveRejectBlog(blog.BlogId, (int)Status.BlogStatus.PENDING, (int)Status.BlogStatus.DRAFTED);
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

        private async Task UpdateStatusWhenApproveRejectBlog(Guid blogId, int oldStatus, int newStatus)
        {
            try
            {
                #region update Recipe status old into new > throw Error if not existed
                var approvedPackages = await _packageRepository.GetPackagesBy(x => x.BlogId == blogId
                                                                                        && x.Status == oldStatus);
                if (approvedPackages.Count() == 0)
                    throw new Exception(ErrorMessage.PackageError.PACKAGE_NOT_FOUND);
                #endregion

                #region update PackageDetails status old into new
                var packageDetails = await _packageDetailRepository.GetPackageDetailsBy(x => x.Status == oldStatus);
                var packageDetailsChange = new List<PackageDetail>();
                foreach (var item in approvedPackages)
                {
                    item.Status = newStatus;
                    packageDetailsChange.AddRange(packageDetails.Where(x => x.PackageId == item.PackageId).ToList());
                }
                await _packageRepository.UpdateRangeAsync(approvedPackages);
                foreach (var item in packageDetailsChange)
                {
                    item.Status = newStatus;
                }
                await _packageDetailRepository.UpdateRangeAsync(packageDetailsChange);
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
