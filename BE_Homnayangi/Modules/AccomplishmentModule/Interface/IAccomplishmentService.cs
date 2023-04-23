using BE_Homnayangi.Modules.AccomplishmentModule.Request;
using BE_Homnayangi.Modules.AccomplishmentModule.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AccomplishmentModule.Interface
{
    public interface IAccomplishmentService
    {
        // Create 
        public Task<bool> CreateANewAccomplishment(Guid authorId, CreatedAccomplishment request);

        // Update
        public Task<bool> ApproveRejectAccomplishment(VerifiedAccomplishment request, Guid userId);
        public Task<bool> UpdateAccomplishmentDetail(Guid authorId, UpdatedAccomplishment request);

        // Get
        public Task<ICollection<OverviewAccomplishment>> GetAccomplishmentByStatus(string status);
        public Task<ICollection<AccomplishmentResponse>> GetAccomplishmentsByBlogId(Guid blogId);
        public Task<DetailAccomplishment> GetAccomplishmentById(Guid id);
        public Task<ICollection<AccomplishmentResponse>> GetAccomplishmentsByCustomerId(Guid customerId);
        public Task<ICollection<OverviewAccomplishment>> GetTop3AccomplishmentsByEventId(Guid eventId);
        
        // Delete
        public Task<bool> RejectAccomplishment(Guid customerId, Guid accomplishmentId);
        public Task<bool> DeleteHardAccomplishment(Guid accomplishmentId);

    }
}
