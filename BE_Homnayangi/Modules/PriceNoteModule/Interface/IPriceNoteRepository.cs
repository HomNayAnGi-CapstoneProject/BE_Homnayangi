using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.PriceNoteModule.Interface
{
    public interface IPriceNoteRepository: IRepository<PriceNote>
    {
        public Task<ICollection<PriceNote>> GetPriceNotesBy(
           Expression<Func<PriceNote, bool>> filter = null,
           Func<IQueryable<PriceNote>, ICollection<PriceNote>> options = null,
           string includeProperties = null
       );
    }
}
