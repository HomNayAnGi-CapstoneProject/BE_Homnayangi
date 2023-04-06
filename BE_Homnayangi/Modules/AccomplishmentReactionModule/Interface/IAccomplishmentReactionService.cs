using Library.Models;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AccomplishmentReactionModule.Interface
{
    public interface IAccomplishmentReactionService
    {
        public Task<bool> GetReactionByCustomerId(Guid customerId, Guid accomplishmentId);
        public Task<AccomplishmentReaction> InteractWithAccomplishment(Guid customerId, Guid accomplishmentId);
    }
}
