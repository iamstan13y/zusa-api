using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;

namespace ZUSA.API.Models.Repository.IRepository
{
    public interface ITeamMemberRepository : IRepository<TeamMember>
    {
        Task<Result<string>> AddBulkAsync(TeamMembersRequest request);
        Task<Result<IEnumerable<TeamMember>>> GetBySubscriptionIdAsync(int subscriptionId);
        Task<Result<TeamMember>> GetByIdAsync(int id);
        Task<Result<IEnumerable<TeamMember>>> GetBySchoolIdAsync(int schoolId);
        Task<Result<IEnumerable<TeamMember>>> GetBySportIdAsync(int sportId);
        Task<Result<IEnumerable<TeamMember>>> GetBySchoolAndSportIdAsync(int schoolId, int sportId);
    }
}