using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AccomplishmentModule.Request;
using BE_Homnayangi.Modules.AccomplishmentModule.Response;
using BE_Homnayangi.Modules.AccomplishmentReactionModule.Interface;
using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.Utils;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AccomplishmentModule
{
    public class AccomplishmentService : IAccomplishmentService
    {
        private readonly IAccomplishmentRepository _accomplishmentRepository;
        private readonly IAccomplishmentReactionRepository _accomplishmentReactionRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;

        public AccomplishmentService(IAccomplishmentRepository accomplishmentRepository, IBlogRepository blogRepository,
            IUserRepository userRepository, IAccomplishmentReactionRepository accomplishmentReactionRepository, ICustomerRepository customerRepository)
        {
            _accomplishmentRepository = accomplishmentRepository;
            _accomplishmentReactionRepository = accomplishmentReactionRepository;
            _blogRepository = blogRepository;
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        #region Create Accomplishment
        public async Task<bool> CreateANewAccomplishment(Guid authorId, CreatedAccomplishment request)
        {
            bool isInserted = false;
            try
            {
                // Check valid image and video
                if (request.ListVideo == null && request.ListImage == null)
                    throw new Exception(ErrorMessage.AccomplishmentError.NOT_VALID_DATA);

                // Check accomplishment existed or not
                var tmpAccom = await _accomplishmentRepository.GetFirstOrDefaultAsync(a =>
                                                                a.AuthorId == authorId
                                                                && a.BlogId == request.BlogId);
                if (tmpAccom != null)
                    throw new Exception(ErrorMessage.AccomplishmentError.ACCOMPLISHMENT_EXISTED);

                var tmpBlog = await _blogRepository.GetFirstOrDefaultAsync(b =>
                                                                b.BlogId == request.BlogId
                                                                && b.BlogStatus == (int)Status.BlogStatus.ACTIVE);
                if (tmpBlog == null)
                    throw new Exception(ErrorMessage.BlogError.BLOG_NOT_FOUND);

                Accomplishment accom = new Accomplishment()
                {
                    AccomplishmentId = Guid.NewGuid(),
                    Content = request.Content,
                    AuthorId = authorId,
                    CreatedDate = DateTime.Now,
                    Status = (int)Status.AccomplishmentStatus.PENDING,
                    BlogId = request.BlogId,
                    ConfirmBy = null,
                    ListVideoUrl = request.ListVideo != null ? StringUtils.CompressContents(request.ListVideo) : null,
                    ListImageUrl = request.ListImage != null ? StringUtils.CompressContents(request.ListImage) : null
                };
                await _accomplishmentRepository.AddAsync(accom);
                isInserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateANewAccomplishment: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isInserted;
        }
        #endregion 

        #region Update Accomplishment
        public async Task<bool> ApproveRejectAccomplishment(VerifiedAccomplishment request, Guid userId)
        {
            bool isUpdated = false;
            try
            {
                if (!(request.Type.Equals("APPROVE") || request.Type.Equals("REJECT")))
                    throw new Exception(ErrorMessage.AccomplishmentError.NOT_VALID_TYPE);
                var tmpUser = await _userRepository.GetFirstOrDefaultAsync(u => u.UserId == userId && !u.IsBlocked.Value);
                if (tmpUser == null)
                    throw new Exception(ErrorMessage.UserError.USER_NOT_EXISTED);

                var accom = await _accomplishmentRepository.GetFirstOrDefaultAsync(a => a.AccomplishmentId == request.AccomplishmentId
                                                                && a.Status == (int)Status.AccomplishmentStatus.PENDING);
                if (accom == null)
                    throw new Exception(ErrorMessage.AccomplishmentError.ACCOMPLISHMENT_NOT_FOUND);

                accom.ConfirmBy = userId;
                accom.Status = request.Type.Equals("APPROVE") ? (int)Status.AccomplishmentStatus.ACTIVE : (int)Status.AccomplishmentStatus.DRAFTED;
                await _accomplishmentRepository.UpdateAsync(accom);
                isUpdated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at ApproveRejectAccomplishment: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isUpdated;
        }

        public async Task<bool> UpdateAccomplishmentDetail(Guid authorId, UpdatedAccomplishment request)
        {
            bool isUpdated = false;
            try
            {
                // Check valid image and video
                if (request.ListVideo == null && request.ListImage == null)
                    throw new Exception(ErrorMessage.AccomplishmentError.NOT_VALID_DATA);

                var accom = await _accomplishmentRepository.GetFirstOrDefaultAsync(a => a.AccomplishmentId == request.AccomplishmentId);
                if (accom == null)
                    throw new Exception(ErrorMessage.AccomplishmentError.ACCOMPLISHMENT_NOT_FOUND);

                if (authorId != accom.AuthorId)
                    throw new Exception(ErrorMessage.AccomplishmentError.NOT_OWNER);

                accom.Content = request.Content == null ? accom.Content : request.Content;
                accom.ListImageUrl = request.ListImage != null ? StringUtils.CompressContents(request.ListImage) : null;
                accom.ListVideoUrl = request.ListVideo != null ? StringUtils.CompressContents(request.ListVideo) : null;
                accom.Status = (int)Status.AccomplishmentStatus.PENDING;
                accom.ConfirmBy = null;
                await _accomplishmentRepository.UpdateAsync(accom);
                isUpdated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateAccomplishmentDetail: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isUpdated;
        }
        #endregion

        #region Get Accomplishment
        public async Task<ICollection<OverviewAccomplishment>> GetAccomplishmentByStatus(string status)
        {
            ICollection<OverviewAccomplishment> result = new List<OverviewAccomplishment>();
            try
            {
                int statusAccom = ConvertAccomplishmentStatus(status);
                ICollection<Accomplishment> tmpAccoms = null;
                if (statusAccom == -1) // Get all Accoms
                {
                    tmpAccoms = await _accomplishmentRepository.GetAll(includeProperties: "Author,ConfirmByNavigation");
                }
                else // get accoms by status
                {
                    tmpAccoms = await _accomplishmentRepository.GetAccomplishmentsBy(a => a.Status == statusAccom,
                                                                includeProperties: "Author,ConfirmByNavigation");
                }
                var reactions = await _accomplishmentReactionRepository.GetAccomplishmentReactionsBy(r => r.Status);
                foreach (var a in tmpAccoms)
                {
                    OverviewAccomplishment tmp = new OverviewAccomplishment()
                    {
                        AccomplishmentId = a.AccomplishmentId,
                        AuthorId = a.AuthorId.Value,
                        AuthorFullName = a.Author.Firstname != null && a.Author.Lastname != null
                                            ? a.Author.Firstname + " " + a.Author.Lastname
                                            : a.Author.Displayname,
                        Avatar = a.Author.Avatar,
                        CreatedDate = a.CreatedDate.Value,
                        Status = a.Status.Value,
                        Reaction = reactions.Where(r => r.AccomplishmentId == a.AccomplishmentId).Count(),
                        BlogId = a.BlogId.Value,
                        ConfirmBy = a.ConfirmBy,
                        VerifierFullName = a.ConfirmByNavigation == null
                                        ? null
                                        : a.ConfirmByNavigation.Firstname + " " + a.ConfirmByNavigation.Lastname
                    };
                    result.Add(tmp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAccomplishmentByStatus:" + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<ICollection<AccomplishmentResponse>> GetAccomplishmentsByBlogId(Guid blogId)
        {
            ICollection<AccomplishmentResponse> result = new List<AccomplishmentResponse>();
            try
            {
                var tmpAccoms = await _accomplishmentRepository.GetAccomplishmentsBy(a => a.BlogId == blogId && a.Status == (int)Status.AccomplishmentStatus.ACTIVE,
                                                                includeProperties: "Author,ConfirmByNavigation,Blog");
                var reactions = await _accomplishmentReactionRepository.GetAccomplishmentReactionsBy(r => r.Status);
                foreach (var a in tmpAccoms)
                {
                    AccomplishmentResponse tmp = new AccomplishmentResponse()
                    {
                        AuthorId = a.AuthorId.Value,
                        AccomplishmentId = a.AccomplishmentId,
                        Status = a.Status.Value,
                        Content = a.Content,
                        ListImage = a.ListImageUrl != null ? StringUtils.ExtractContents(a.ListImageUrl) : null,
                        ListVideo = a.ListVideoUrl != null ? StringUtils.ExtractContents(a.ListVideoUrl) : null,
                        CreatedDate = a.CreatedDate.Value,
                        AuthorFullName = a.Author.Firstname + " " + a.Author.Lastname,
                        Avatar = a.Author.Avatar,
                        Reaction = reactions.Where(r => r.AccomplishmentId == a.AccomplishmentId).Count(),
                        BlogId = a.BlogId.Value,
                        BlogTitle = a.Blog.Title,
                    };
                    result.Add(tmp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAccomplishmentsByBlogId:" + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<DetailAccomplishment> GetAccomplishmentById(Guid id)
        {
            DetailAccomplishment result = null;
            try
            {
                var tmpAccom = await _accomplishmentRepository.GetFirstOrDefaultAsync(a => a.AccomplishmentId == id,
                    includeProperties: "Author,ConfirmByNavigation");
                if (tmpAccom == null)
                    throw new Exception(ErrorMessage.AccomplishmentError.ACCOMPLISHMENT_NOT_FOUND);
                var reactions = await _accomplishmentReactionRepository.GetAccomplishmentReactionsBy(r => r.Status);

                result = new DetailAccomplishment()
                {
                    AccomplishmentId = tmpAccom.AccomplishmentId,
                    Content = tmpAccom.Content,
                    AuthorId = tmpAccom.AuthorId.Value,
                    AuthorImage = tmpAccom.Author.Avatar,
                    BlogId = tmpAccom.BlogId.Value,
                    ListImage = tmpAccom.ListImageUrl != null ? StringUtils.ExtractContents(tmpAccom.ListImageUrl) : null,
                    ListVideo = tmpAccom.ListVideoUrl != null ? StringUtils.ExtractContents(tmpAccom.ListVideoUrl) : null,
                    CreatedDate = tmpAccom.CreatedDate.Value,
                    Reaction = reactions.Where(r => r.AccomplishmentId == tmpAccom.AccomplishmentId).Count(),
                    Status = tmpAccom.Status.Value,
                    AuthorFullName = tmpAccom.Author.Firstname + " " + tmpAccom.Author.Lastname,
                    VerifierFullName = tmpAccom.ConfirmByNavigation == null
                                        ? null
                                        : tmpAccom.ConfirmByNavigation.Firstname + " " + tmpAccom.ConfirmByNavigation.Lastname,

                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAccomplishmentById:" + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<ICollection<AccomplishmentResponse>> GetAccomplishmentsByCustomerId(Guid customerId)
        {
            ICollection<AccomplishmentResponse> result = new List<AccomplishmentResponse>();
            try
            {
                var accoms = await _accomplishmentRepository.GetAccomplishmentsBy(a => a.AuthorId == customerId,
                                                                    includeProperties: "Author,ConfirmByNavigation,Blog");
                var reactions = await _accomplishmentReactionRepository.GetAccomplishmentReactionsBy(r => r.Status);

                if (accoms.Count > 0)
                    foreach (var a in accoms)
                    {
                        AccomplishmentResponse tmp = new AccomplishmentResponse()
                        {
                            AuthorId = a.AuthorId.Value,
                            AccomplishmentId = a.AccomplishmentId,
                            Status = a.Status.Value,
                            Content = a.Content,
                            ListImage = a.ListImageUrl != null ? StringUtils.ExtractContents(a.ListImageUrl) : null,
                            ListVideo = a.ListVideoUrl != null ? StringUtils.ExtractContents(a.ListVideoUrl) : null,
                            CreatedDate = a.CreatedDate.Value,
                            AuthorFullName = a.Author.Firstname + " " + a.Author.Lastname,
                            Avatar = a.Author.Avatar,
                            Reaction = reactions.Where(r => r.AccomplishmentId == a.AccomplishmentId).Count(),
                            BlogId = a.BlogId.Value,
                            BlogTitle = a.Blog.Title,
                        };
                        result.Add(tmp);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAccomplishmentById:" + ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }

        private int ConvertAccomplishmentStatus(string status)
        {
            int result = -1;
            switch (status)
            {
                case "DEACTIVE":
                    {
                        result = 0;
                        break;
                    }
                case "ACTIVE":
                    {
                        result = 1;
                        break;
                    }
                case "DRAFTED":
                    {
                        result = 2;
                        break;
                    }
                case "PENDING":
                    {
                        result = 3;
                        break;
                    }
                case "ALL":
                    {
                        result = -1;
                        break;
                    }
            }
            return result;
        }

        public async Task<ICollection<OverviewAccomplishment>> GetTop3AccomplishmentsByEventId(Guid eventId)
        {
            var result = new List<OverviewAccomplishment>();
            try
            {
                var tmpEvent = await _blogRepository.GetFirstOrDefaultAsync(e => e.BlogId == eventId && e.IsEvent.Value);
                if (tmpEvent == null)
                    throw new Exception(ErrorMessage.EventError.EVENT_NOT_FOUND);
                var accoms = await _accomplishmentRepository.GetAccomplishmentsBy(a => a.BlogId == eventId,
                                                                                            includeProperties: "Author,ConfirmByNavigation");

                var accomReactions = await _accomplishmentReactionRepository.GetAccomplishmentReactionsBy(r => r.Status);

                if (accoms.Count > 0)
                    result = accoms.Select(a => new OverviewAccomplishment()
                    {
                        AccomplishmentId = a.AccomplishmentId,
                        AuthorId = a.AuthorId.Value,
                        AuthorFullName = a.Author.Firstname != null && a.Author.Lastname != null
                                            ? a.Author.Firstname + " " + a.Author.Lastname
                                            : a.Author.Displayname,
                        Avatar = a.Author.Avatar,
                        CreatedDate = a.CreatedDate.Value,
                        Status = a.Status.Value,
                        Reaction = accomReactions.Where(r => r.AccomplishmentId == a.AccomplishmentId && r.Status).Count(),
                        BlogId = a.BlogId.Value,
                        ConfirmBy = a.ConfirmBy,
                        VerifierFullName = a.ConfirmByNavigation == null
                                        ? null
                                        : a.ConfirmByNavigation.Firstname + " " + a.ConfirmByNavigation.Lastname
                    }).OrderByDescending(a => a.Reaction).Take(3).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetTop3AccomplishmentsByEventId: " + ex.Message);
            }
            return result;
        }

        #endregion

        #region Delete Accomplishment
        public async Task<bool> RejectAccomplishment(Guid customerId, Guid accomplishmentId)
        {
            bool isUpdated = false;
            try
            {
                var tmpCustomer = await _customerRepository.GetFirstOrDefaultAsync(u => u.CustomerId == customerId && !u.IsBlocked.Value);
                if (tmpCustomer == null)
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_FOUND);
                var accom = await _accomplishmentRepository.GetFirstOrDefaultAsync(a => a.AccomplishmentId == accomplishmentId
                                                                && (a.Status == (int)Status.AccomplishmentStatus.ACTIVE ||
                                                                    a.Status == (int)Status.AccomplishmentStatus.PENDING ||
                                                                    a.Status == (int)Status.AccomplishmentStatus.DRAFTED)
                                                                && a.AuthorId == customerId);
                if (accom == null)
                    throw new Exception(ErrorMessage.AccomplishmentError.ACCOMPLISHMENT_NOT_FOUND);

                accom.Status = (int)Status.AccomplishmentStatus.DEACTIVE;
                await _accomplishmentRepository.UpdateAsync(accom);
                isUpdated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at RejectAccomplishment: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isUpdated;
        }

        public async Task<bool> DeleteHardAccomplishment(Guid accomplishmentId)
        {
            bool isUpdated = false;
            try
            {
                var accom = await _accomplishmentRepository.GetFirstOrDefaultAsync(a => a.AccomplishmentId == accomplishmentId
                                                                && (a.Status == (int)Status.AccomplishmentStatus.DEACTIVE
                                                                    || a.Status == (int)Status.AccomplishmentStatus.DRAFTED
                                                                    || a.Status == (int)Status.AccomplishmentStatus.PENDING));
                if (accom == null)
                    throw new Exception(ErrorMessage.AccomplishmentError.ACCOMPLISHMENT_NOT_FOUND);

                await _accomplishmentRepository.RemoveAsync(accom);
                isUpdated = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteHardAccomplishment: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isUpdated;
        }

        #endregion
    }
}
