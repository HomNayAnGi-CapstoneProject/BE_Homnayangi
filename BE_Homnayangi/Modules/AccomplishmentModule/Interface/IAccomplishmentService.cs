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
        public Task<DetailAccomplishment> GetAccomplishmentById(Guid id);
        public Task<ICollection<OverviewAccomplishment>> GetAccomplishmentsByCustomerId(Guid customerId);

        // Delete
        public Task<bool> RejectAccomplishment(Guid userId, Guid accomplishmentId);
        public Task<bool> DeleteHardAccomplishment(Guid accomplishmentId);

    }
}
