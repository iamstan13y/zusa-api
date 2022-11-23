using ZUSA.API.Models.Local;
using ZUSA.API.Services;

namespace ZUSA.API.Models.Repository
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly AppDbContext _context;
        private readonly IExcelService _excelService;
        public TeamMemberRepository(AppDbContext context, IExcelService excelService)
        {
            _context = context;
            _excelService = excelService;
        }

        public async Task<Result<string>> AddBulkAsync(TeamMembersRequest request)    
        {
            var teamMembers = await _excelService.ExtractRecordsAsync(request.TeamExcelFile!);

            teamMembers.ToList().ForEach(member =>
            {
                member.SportId = request.SportId;
                member.SchoolId = request.SchoolId;
            });
            
            await _context.TeamMembers!.AddRangeAsync(teamMembers);
            await _context.SaveChangesAsync();
            
            return new Result<string>("Team members added successfully");
        }
    }
}