using BE_Homnayangi.Modules.AccomplishmentReactionModule.Interface;
using Library.Models;
using Library.Models.Enum;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AccomplishmentReactionModule
{
    public class AccomplishmentReactionService : IAccomplishmentReactionService
    {

        private readonly IAccomplishmentReactionRepository _accomplishmentReactionRepository;
        public AccomplishmentReactionService(IAccomplishmentReactionRepository accomplishmentReactionRepository)
        {
            _accomplishmentReactionRepository = accomplishmentReactionRepository;
        }

        public async Task<bool> GetReactionByCustomerId(Guid customerId, Guid accomplishmentId)
        {
            try
            {
                var item = await _accomplishmentReactionRepository.GetFirstOrDefaultAsync(x => x.AccomplishmentId == accomplishmentId
                                                                                         && x.CustomerId == customerId
                                                                                         && x.Status);
                return item != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetReactionByCustomerId:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<AccomplishmentReaction> InteractWithAccomplishment(Guid customerId, Guid accomplishmentId)
        {
            try
            {
                var item = await _accomplishmentReactionRepository.GetFirstOrDefaultAsync(x => x.CustomerId == customerId
                                                                                        && x.AccomplishmentId == accomplishmentId);
                if (item == null) // insert new record
                {
                    item = new AccomplishmentReaction()
                    {
                        AccomplishmentId = accomplishmentId,
                        CustomerId = customerId,
                        CreatedDate = DateTime.Now,
                        Status = true,
                    };
                    await _accomplishmentReactionRepository.AddAsync(item);
                }
                else
                {
                    item.Status = !item.Status;
                    await _accomplishmentReactionRepository.UpdateAsync(item);
                }
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at InteractWithAccomplishment:" + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
