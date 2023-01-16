using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Utility;

namespace ZUSA.API.Models.Repository.IRepository
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<Result<IEnumerable<Subscription>>> GetBySportIdAsync(int sportId);
        Task<Result<Pageable<Subscription>>> GetPagedBySportIdAsync(int sportId, Pagination pagination);
        Task<Result<IEnumerable<Subscription>>> GetBySchoolIdAsync(int schoolId);
        Task<Result<Pageable<Subscription>>> GetPagedBySchoolIdAsync(int schoolId, Pagination pagination);
        Task<Result<bool>> ToggleStatusAsync(int subscriptionId);
    }
}