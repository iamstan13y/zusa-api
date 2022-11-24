using Microsoft.EntityFrameworkCore;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;

namespace ZUSA.API.Models.Repository
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        private readonly AppDbContext _context;
        public SubscriptionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Subscription>>> GetBySchoolIdAsync(int schoolId)
        {
            var subscriptions = await _context.Subscriptions!
                .Include(s => s.School)
                .Include(s => s.Sport)
                .Where(s => s.SchoolId == schoolId)
                .ToListAsync();
            
            return new Result<IEnumerable<Subscription>>(subscriptions);
        }

        public async Task<Result<IEnumerable<Subscription>>> GetBySportIdAsync(int sportId)
        {
            var subscriptions = await _context.Subscriptions!
                .Where(s => s.SportId == sportId)
                .Include(x => x.Sport)
                .Include(x => x.School)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();

            return new Result<IEnumerable<Subscription>>(subscriptions);
        }
    }
}