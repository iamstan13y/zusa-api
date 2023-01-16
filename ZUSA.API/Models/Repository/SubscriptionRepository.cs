﻿using Microsoft.EntityFrameworkCore;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
using ZUSA.API.Utility;

namespace ZUSA.API.Models.Repository
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext context) : base(context)
        {

        }

        public async new Task<Result<IEnumerable<Subscription>>> GetAllAsync()
        {
            var subscriptions = await _dbSet!
               .Include(s => s.School)
               .Include(s => s.Sport)
               .ToListAsync();

            return new Result<IEnumerable<Subscription>>(subscriptions);
        }

        public async new Task<Result<Pageable<Subscription>>> GetAllPagedAsync(Pagination pagination)
        {
            var subscriptions = await _dbSet
                .Include(s => s.School)
                .Include(s => s.Sport)
                .ToListAsync();

            var pagedSubscriptions = new Pageable<Subscription>(subscriptions, pagination.Page, pagination.Size);

            return new Result<Pageable<Subscription>>(pagedSubscriptions);
        }

        public async new Task<Result<Subscription>> AddAsync(Subscription sub)
        {
            var subscription = await _dbSet.Where(x => x.SportId == sub.SportId && x.SchoolId == sub.SchoolId).FirstOrDefaultAsync();
            if (subscription != null) return new Result<Subscription>(false, "Sorry! You've already subscribed for this sport.");

            await _dbSet.AddAsync(sub);

            return new Result<Subscription>(sub);
        }

        public async Task<Result<IEnumerable<Subscription>>> GetBySchoolIdAsync(int schoolId)
        {
            var subscriptions = await _dbSet!
                .Include(s => s.School)
                .Include(s => s.Sport)
                .Where(s => s.SchoolId == schoolId)
                .ToListAsync();

            return new Result<IEnumerable<Subscription>>(subscriptions);
        }

        public async Task<Result<IEnumerable<Subscription>>> GetBySportIdAsync(int sportId)
        {
            var subscriptions = await _dbSet!
                .Where(s => s.SportId == sportId)
                .Include(x => x.Sport)
                .Include(x => x.School)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();

            return new Result<IEnumerable<Subscription>>(subscriptions);
        }

        public async Task<Result<bool>> ToggleStatusAsync(int subscriptionId)
        {
            var subscription = await _dbSet.FindAsync(subscriptionId);
            if (subscription == null) return new Result<bool>(false, "Subscription not found.");

            if ((DateTime.Now - subscription.DateCreated).Hours > 72)
                return new Result<bool>(false, "Your permissible time to edit this subscription has expired");

            subscription.IsActive = !subscription.IsActive;

            return new Result<bool>(true, "Subscription updated successfully.");
        }

        public async Task<Result<Pageable<Subscription>>> GetPagedBySportIdAsync(int sportId, Pagination pagination)
        {
            var subscriptions = await _dbSet
                .Where(s => s.SportId == sportId)
                .Include(s => s.School)
                .Include(s => s.Sport)
                .ToListAsync();

            var pagedSubscriptions = new Pageable<Subscription>(subscriptions, pagination.Page, pagination.Size);

            return new Result<Pageable<Subscription>>(pagedSubscriptions);
        }

        public async Task<Result<Pageable<Subscription>>> GetPagedBySchoolIdAsync(int schoolId, Pagination pagination)
        {
            var subscriptions = await _dbSet
                .Where(s => s.SchoolId == schoolId)
                .Include(s => s.School)
                .Include(s => s.Sport)
                .ToListAsync();

            var pagedSubscriptions = new Pageable<Subscription>(subscriptions, pagination.Page, pagination.Size);

            return new Result<Pageable<Subscription>>(pagedSubscriptions);
        }
    }
}