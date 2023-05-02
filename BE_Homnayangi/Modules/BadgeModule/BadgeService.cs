using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Interface;
using BE_Homnayangi.Modules.BadgeModule.DTO.Request;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using BE_Homnayangi.Modules.BadgeModule.Response;
using BE_Homnayangi.Ultils.Quartz;
using Hangfire;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Library.Models.Enum.Status;

namespace BE_Homnayangi.Modules.BadgeModule
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _badgeRepository;
        private readonly IBadgeConditionRepository _badgeConditionRepository;
        private readonly ICronJobTimeConfigRepository _cronJobTimeConfigRepository;

        public BadgeService(IBadgeRepository badgeRepository, IBadgeConditionRepository badgeConditionRepository, ICronJobTimeConfigRepository cronJobTimeConfigRepository)
        {
            _badgeRepository = badgeRepository;
            _badgeConditionRepository = badgeConditionRepository;
            _cronJobTimeConfigRepository = cronJobTimeConfigRepository;
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
            try
            {
                if (!await CheckDuplicatedBadgeNameWhenCreate(newBadge.Name))
                {
                    newBadge.BadgeId = Guid.NewGuid();
                    newBadge.CreateDate = DateTime.Now;
                    newBadge.Status = (int)Status.BadgeStatus.ACTIVE;
                    await _badgeRepository.AddAsync(newBadge);
                }
                else
                {
                    throw new Exception(ErrorMessage.BadgeError.BADGE_NAME_EXISTED);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at AddNewBadge: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateBadge(Badge newBadge)
        {
            try
            {
                var badge = await _badgeRepository.GetFirstOrDefaultAsync(b => b.BadgeId == newBadge.BadgeId);
                if (!await CheckDuplicatedBadgeNameWhenUpdate(newBadge.BadgeId, newBadge.Name))
                {
                    badge.Name = newBadge.Name;
                    badge.Description = newBadge.Description;
                    badge.ImageUrl = newBadge.ImageUrl;
                    badge.VoucherId = newBadge.VoucherId;
                    await _badgeRepository.UpdateAsync(badge);
                }
                else
                    throw new Exception(ErrorMessage.BadgeError.BADGE_NAME_EXISTED);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateBadge: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> CheckDuplicatedBadgeNameWhenUpdate(Guid badgeId, string badgeName)
        {
            try
            {
                var list = await _badgeRepository.GetBadgesBy(b => b.Status.Value == (int)Status.BadgeStatus.ACTIVE);
                int count = list.Where(item => item.Name.Equals(badgeName) && item.BadgeId != badgeId).Count();
                return (count > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CheckDuplicatedBadgeNameWhenUpdate: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> CheckDuplicatedBadgeNameWhenCreate(string badgeName)
        {
            try
            {
                var list = await _badgeRepository.GetBadgesBy(b => b.Status.Value == (int)Status.BadgeStatus.ACTIVE);
                int count = list.Where(item => item.Name.Equals(badgeName)).Count();
                return (count > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CheckDuplicatedBadgeNameWhenCreate: " + ex.Message);
                throw new Exception(ex.Message);
            }
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
            try
            {


                var badgeTime = _cronJobTimeConfigRepository.GetFirstOrDefaultAsync(x => x.TargetObject == (int)CronJobTimeConfigType.CronJobTimeConfig.BADGE).Result;
                var minute = badgeTime.Minute;
                var hour = badgeTime.Hour;
                var date = badgeTime.Day;
                var month = badgeTime.Month;
                if (month == null && date == null && hour == null)
                {
                    RecurringJob.AddOrUpdate<BadgeJob>("awardbadge", x => x.BadgeCondition(), Cron.Hourly((int)minute), TimeZoneInfo.Local);
                }
                else if (month == null && date == null)
                {
                    RecurringJob.AddOrUpdate<BadgeJob>("awardbadge", x => x.BadgeCondition(), Cron.Daily((int)hour, (int)minute), TimeZoneInfo.Local);

                }
                else if (month == null)
                {
                    RecurringJob.AddOrUpdate<BadgeJob>("awardbadge", x => x.BadgeCondition(), Cron.Monthly((int)hour, (int)minute), TimeZoneInfo.Local);
                }
                else
                {
                    RecurringJob.AddOrUpdate<BadgeJob>("awardbadge", x => x.BadgeCondition(), Cron.Yearly((int)month, (int)date, (int)hour, (int)minute), TimeZoneInfo.Local);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at AwardBadge: " + ex.Message);
                throw;
            }

        }

        public async Task DeleteBadge(Guid id)
        {
            try
            {
                var badge = await _badgeRepository.GetFirstOrDefaultAsync(b => b.BadgeId == id
                                                                                && b.Status.Value == (int)Status.BadgeStatus.ACTIVE);
                if (badge == null)
                    throw new Exception(ErrorMessage.BadgeError.BADGE_NOT_FOUND);
                var badgeCondition = await _badgeConditionRepository.GetFirstOrDefaultAsync(bc => bc.BadgeId == id
                                                                                                && bc.Status.Value);
                if (badgeCondition != null)
                {
                    badgeCondition.Status = false;
                    await _badgeConditionRepository.UpdateAsync(badgeCondition);
                }
                badge.Status = (int)Status.BadgeStatus.DELETED;
                await _badgeRepository.UpdateAsync(badge);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteBadge: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

