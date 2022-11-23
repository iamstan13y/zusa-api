using ZUSA.API.Models.Data;

namespace ZUSA.API.Models.Local
{
    public interface ITeamMemberRepository
    {
        Task<Result<string>> AddBulkAsync(TeamMembersRequest request);
    }
}