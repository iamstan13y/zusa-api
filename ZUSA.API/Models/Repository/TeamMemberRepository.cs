using ZUSA.API.Models.Local;

namespace ZUSA.API.Models.Repository
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        public Task<Result<string>> AddBulkAsync(TeamMembersRequest request)
        {
            throw new NotImplementedException();
        }
    }
}