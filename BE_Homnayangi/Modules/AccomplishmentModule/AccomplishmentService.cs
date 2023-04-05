using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AccomplishmentModule.Request;
using BE_Homnayangi.Modules.AccomplishmentModule.Response;
using BE_Homnayangi.Modules.BlogModule.Interface;
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
        private readonly IBlogRepository _blogRepository;
        private readonly IUserRepository _userRepository;

        public AccomplishmentService(IAccomplishmentRepository accomplishmentRepository, IBlogRepository blogRepository,
            IUserRepository userRepository)
        {
            _accomplishmentRepository = accomplishmentRepository;
            _blogRepository = blogRepository;
            _userRepository = userRepository;
        }

        #region Create Accomplishment
        public async Task<bool> CreateANewAccomplishment(Guid authorId, CreatedAccomplishment request)
        {
            bool isInserted = false;
            try
            {
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
                    ListVideoUrl = StringUtils.CompressContents(request.ListVideo),
                    ListImageUrl = StringUtils.CompressContents(request.ListImage)
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
                var accom = await _accomplishmentRepository.GetFirstOrDefaultAsync(a => a.AccomplishmentId == request.AccomplishmentId);
                if (accom == null)
                    throw new Exception(ErrorMessage.AccomplishmentError.ACCOMPLISHMENT_NOT_FOUND);

                if (authorId != accom.AuthorId)
                    throw new Exception(ErrorMessage.AccomplishmentError.NOT_OWNER);


                accom.Content = request.Content == null ? accom.Content : request.Content;
                accom.ListImageUrl = StringUtils.CompressContents(request.ListImage);
                accom.ListVideoUrl = StringUtils.CompressContents(request.ListVideo);
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
                var tmpAccom = await _accomplishmentRepository.GetAccomplishmentsBy(a => a.Status == statusAccom,
                                                                includeProperties: "Author,ConfirmByNavigation");
                if (tmpAccom.Count > 0)
                    result = tmpAccom.Select(a => new OverviewAccomplishment()
                    {
                        AccomplishmentId = a.AccomplishmentId,
                        AuthorId = a.AuthorId.Value,
                        AuthorFullName = a.Author.Firstname + " " + a.Author.Lastname,
                        CreatedDate = a.CreatedDate.Value,
                        Status = a.Status.Value,
                        BlogId = a.BlogId.Value,
                        ConfirmBy = a.ConfirmBy,
                        VerifierFullName = a.ConfirmByNavigation == null
                                        ? null
                                        : a.ConfirmByNavigation.Firstname + " " + a.ConfirmByNavigation.Lastname
                    }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAccomplishmentByStatus:" + ex.Message);
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

        public async Task<ICollection<OverviewAccomplishment>> GetAccomplishmentsByCustomerId(Guid customerId)
        {
            ICollection<OverviewAccomplishment> result = new List<OverviewAccomplishment>();
            try
            {
                var accoms = await _accomplishmentRepository.GetAccomplishmentsBy(a => a.AuthorId == customerId,
                                                                    includeProperties: "Author,ConfirmByNavigation");
                if (accoms.Count > 0)
                    result = accoms.Select(a => new OverviewAccomplishment()
                    {
                        AccomplishmentId = a.AccomplishmentId,
                        AuthorId = a.AuthorId.Value,
                        AuthorFullName = a.Author.Firstname + " " + a.Author.Lastname,
                        CreatedDate = a.CreatedDate.Value,
                        Status = a.Status.Value,
                        BlogId = a.BlogId.Value,
                        ConfirmBy = a.ConfirmBy,
                        VerifierFullName = a.ConfirmByNavigation == null
                                        ? null
                                        : a.ConfirmByNavigation.Firstname + " " + a.ConfirmByNavigation.Lastname
                    }).ToList();
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
            }
            return result;
        }
        #endregion

        #region
        public async Task<bool> RejectAccomplishment(Guid userId, Guid accomplishmentId)
        {
            bool isUpdated = false;
            try
            {
                var tmpUser = await _userRepository.GetFirstOrDefaultAsync(u => u.UserId == userId && !u.IsBlocked.Value);
                if (tmpUser == null)
                    throw new Exception(ErrorMessage.UserError.USER_NOT_EXISTED);
                var accom = await _accomplishmentRepository.GetFirstOrDefaultAsync(a => a.AccomplishmentId == accomplishmentId
                                                                && a.Status == (int)Status.AccomplishmentStatus.PENDING);
                if (accom == null)
                    throw new Exception(ErrorMessage.AccomplishmentError.ACCOMPLISHMENT_NOT_FOUND);

                accom.ConfirmBy = userId;
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
