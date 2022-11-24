using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;

namespace ZUSA.API.Models.Repository.IRepository
{
    public interface ITeamMemberRepository
    {
        Task<Result<string>> AddBulkAsync(TeamMembersRequest request);
    }
}