using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;

namespace ZUSA.API.Models.Repository.IRepository
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<Result<IEnumerable<Subscription>>> GetBySportIdAsync(int sportId);
        Task<Result<IEnumerable<Subscription>>> GetBySchoolIdAsync(int schoolId);
    }
}