using BE_Homnayangi.Modules.TypeModule.DTO.Request;
using BE_Homnayangi.Modules.TypeModule.Interface;
using BE_Homnayangi.Modules.TypeModule.Response;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Type = Library.Models.Type;

namespace BE_Homnayangi.Modules.TypeModule
{
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _TypeRepository;

        public TypeService(ITypeRepository TypeRepository)
        {
            _TypeRepository = TypeRepository;
        }

        public async Task<ICollection<Type>> GetAll()
        {
            return await _TypeRepository.GetAll();
        }

        public Task<ICollection<Type>> GetTypesBy(
                Expression<Func<Type,
                bool>> filter = null,
                Func<IQueryable<Type>,
                ICollection<Type>> options = null,
                string includeProperties = null)
        {
            return _TypeRepository.GetTypesBy(filter);
        }


        public async Task AddNewType(Type newType)
        {
            newType.TypeId = Guid.NewGuid();
            await _TypeRepository.AddAsync(newType);
        }

        public async Task UpdateType(Type TypeUpdate)
        {
            await _TypeRepository.UpdateAsync(TypeUpdate);
        }

        public async Task<Type> GetTypeByID(Guid? id)
        {
            return await _TypeRepository.GetFirstOrDefaultAsync(x => x.TypeId.Equals(id) && x.Status.Value);
        }

        public async Task<PagedResponse<PagedList<Type>>> GetAllPaged(TypeFilterRequest request)
        {
            int pageNumber = request.PageNumber;
            int pageSize = request.PageSize;
            var user = await _TypeRepository.GetAll();
            var response = PagedList<Type>.ToPagedList(source: user, pageNumber: pageNumber, pageSize: pageSize);
            return response.ToPagedResponse();

        }
        public async Task<ICollection<TypeDropdownResponse>> GetTypeDropdown()
        {
            return _TypeRepository.GetTypesBy(t => t.Status.Value).Result.Select(x => new TypeDropdownResponse
            {
                TypeId = x.TypeId,
                TypeName = x.Name
            }).ToList();
        }

    }
}
