using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;

namespace ZUSA.API.Models.Repository
{
    public class SubscriptionRepository : Repository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext context) : base(context)
        {
        }

        public Task<Result<IEnumerable<Subscription>>> GetBySchoolIdAsync(int schoolId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Subscription>>> GetBySportIdAsync(int sportId)
        {
            throw new NotImplementedException();
        }
    }
}