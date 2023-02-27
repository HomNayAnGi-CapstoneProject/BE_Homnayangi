using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Interface
{
    public interface ISeasonReferenceRepository : IRepository<SeasonReference>
    {
        public Task<ICollection<SeasonReference>> GetSeasonReferencesBy(
               Expression<Func<SeasonReference, bool>> filter = null,
               Func<IQueryable<SeasonReference>, ICollection<SeasonReference>> options = null,
               string includeProperties = null
           );
    }
}
