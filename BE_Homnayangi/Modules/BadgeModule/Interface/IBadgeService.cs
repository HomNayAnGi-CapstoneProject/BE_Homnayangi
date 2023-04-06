using BE_Homnayangi.Modules.BadgeModule.DTO.Request;
using BE_Homnayangi.Modules.BadgeModule.Response;
using Library.Models;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BadgeModule.Interface
{
    public interface IBadgeService
    {
        public Task AddNewBadge(Badge newBadge);

        public Task UpdateBadge(Badge badgeUpdate);

        public Task<ICollection<Badge>> GetAll();

        public Task<ICollection<Badge>> GetBadgesBy(
            Expression<Func<Badge, bool>> filter = null,
            Func<IQueryable<Badge>, ICollection<Badge>> options = null,
            string includeProperties = null);

        public Task<ICollection<Badge>> GetRandomBadgesBy(Expression<Func<Badge, bool>> filter = null,
            Func<IQueryable<Badge>, ICollection<Badge>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public Task<Badge> GetBadgeByID(Guid? id);

        public Task<PagedResponse<PagedList<Badge>>> GetAllPaged(BadgeFilterRequest request);

        public Task<bool> CheckExistedName(string name);
        public Task<ICollection<BadgeDropdownResponse>> GetBadgeDropdown();
    }
}

