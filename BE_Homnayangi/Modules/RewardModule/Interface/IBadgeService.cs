﻿using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.PagedList;
using BE_Homnayangi.Modules.BadgeModule.DTO.Request;

namespace BE_Homnayangi.Modules.BadgeModule.Interface
{
    public interface IBadgeService
    {
        public Task AddNewBadge(Badge newBadge);

        public Task UpdateBadge(Badge rewardUpdate);

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
    }
}

