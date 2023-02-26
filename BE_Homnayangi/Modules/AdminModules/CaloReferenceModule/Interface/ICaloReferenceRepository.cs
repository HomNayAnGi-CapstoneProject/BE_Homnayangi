using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface
{
    public interface ICaloReferenceRepository : IRepository<CaloReference>
    {
        public Task<ICollection<CaloReference>> GetCaloReferencesBy(
            Expression<Func<CaloReference, bool>> filter = null,
            Func<IQueryable<CaloReference>, ICollection<CaloReference>> options = null,
            string includeProperties = null
        );
    }
}
