using BE_Homnayangi.Modules.TypeModule.DTO.Request;
using BE_Homnayangi.Modules.TypeModule.Response;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Type = Library.Models.Type;

namespace BE_Homnayangi.Modules.TypeModule.Interface
{
    public interface ITypeService
    {
        public Task AddNewType(Type newType);

        public Task UpdateType(Type TypeUpdate);

        public Task<ICollection<Type>> GetAll();

        public Task<ICollection<Type>> GetTypesBy(
            Expression<Func<Type, bool>> filter = null,
            Func<IQueryable<Type>, ICollection<Type>> options = null,
            string includeProperties = null);

        public Task<Type> GetTypeByID(Guid? id);

        public Task<PagedResponse<PagedList<Type>>> GetAllPaged(TypeFilterRequest request);

        public Task<ICollection<TypeDropdownResponse>> GetTypeDropdown();
    }
}

