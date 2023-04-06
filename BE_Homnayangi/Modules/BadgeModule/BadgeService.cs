using System;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.PagedList;
using Library.Models.Enum;
using BE_Homnayangi.Modules.BadgeModule.DTO.Request;
using BE_Homnayangi.Modules.RewardModule.DTO.Request;
using AutoMapper;
using BE_Homnayangi.Modules.BadgeModule.Response;
using static Library.Models.Enum.Status;
using Hangfire;
using BE_Homnayangi.Ultils.Quartz;

namespace BE_Homnayangi.Modules.BadgeModule
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _badgeRepository;

        public BadgeService(IBadgeRepository badgeRepository)
        {
            _badgeRepository = badgeRepository;
        }

        public async Task<ICollection<Badge>> GetAll()
        {
            return await _badgeRepository.GetAll();
        }

        public Task<ICollection<Badge>> GetBadgesBy(
                Expression<Func<Badge,
                bool>> filter = null,
                Func<IQueryable<Badge>,
                ICollection<Badge>> options = null,
                string includeProperties = null)
        {
            return _badgeRepository.GetBadgesBy(filter);
        }

        public async Task<ICollection<BadgeDropdownResponse>> GetBadgeDropdown()
        {
            return _badgeRepository.GetBadgesBy(x => x.Status == (int?)BadgeStatus.ACTIVE).Result.Select(x => new BadgeDropdownResponse
            {
                BadgeId = x.BadgeId,
                BadgeName = x.Name
            }).ToList();
        }

        public Task<ICollection<Badge>> GetRandomBadgesBy(
                Expression<Func<Badge, bool>> filter = null,
                Func<IQueryable<Badge>, ICollection<Badge>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _badgeRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewBadge(Badge newBadge)
        {

            newBadge.BadgeId = Guid.NewGuid();
            newBadge.CreateDate = DateTime.Now;
            newBadge.Status = (int)Status.BadgeStatus.ACTIVE;
            await _badgeRepository.AddAsync(newBadge);
        }

        public async Task UpdateBadge(Badge BadgeUpdate)
        {

            await _badgeRepository.UpdateAsync(BadgeUpdate);
        }

        public async Task<Badge> GetBadgeByID(Guid? id)
        {
            return await _badgeRepository.GetFirstOrDefaultAsync(x => x.BadgeId.Equals(id));
        }

        public async Task<PagedResponse<PagedList<Badge>>> GetAllPaged(BadgeFilterRequest request)
        {
            try
            {
                var pageSize = request.PageSize;
                var pageNumber = request.PageNumber;
                var sort = request.sort;
                var sortDesc = request.sortDesc;

                var badges = await _badgeRepository.GetAll();

                switch (sort)
                {
                    case (int)Sort.BadgesSortBy.CREATEDDATE:
                        badges = sortDesc == true
                            ? badges.OrderByDescending(r => r.CreateDate).ToList()
                            : badges.OrderBy(r => r.CreateDate).ToList();
                        break;
                    case (int)Sort.BadgesSortBy.NAME:
                        badges = sortDesc == true
                            ? badges.OrderByDescending(r => r.Name).ToList()
                            : badges.OrderBy(r => r.Name).ToList();
                        break;
                    default:
                        badges = sortDesc == true
                            ? badges.OrderByDescending(r => r.CreateDate).ToList()
                            : badges.OrderBy(r => r.CreateDate).ToList();
                        break;
                }

                var res = PagedList<Badge>.ToPagedList(source: badges, pageSize: pageSize, pageNumber: pageNumber);
                return res.ToPagedResponse();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<bool> CheckExistedName(string name)
        {
            try
            {
                var res = await _badgeRepository.GetBadgesBy(r => r.Name.Trim().ToUpper().Equals(name.Trim().ToUpper()));
                return res.Count > 0;
            }
            catch
            {
                throw new Exception("Name existed");
            }
        }

        public void AwardBadge()
        {
            int minute = 1;
            int hour = 0;
            int date = 0;
            int month = 0;
            RecurringJob.AddOrUpdate<BadgeJob>("awardbadge", x => x.BadgeCondition(), Cron.Yearly(month, date, hour, minute), TimeZoneInfo.Local);

        }
    }
}

