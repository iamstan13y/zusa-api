using Microsoft.EntityFrameworkCore;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
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

        public async Task<Result<IEnumerable<TeamMember>>> GetBySchoolAndSportIdAsync(int schoolId, int sportId)
        {
            var teamMembers = await _context.TeamMembers!
               .Where(x => x.SchoolId == schoolId && x.SportId == sportId)
               .Include(x => x.Sport)
               .Include(x => x.School)
               .ToListAsync();

            return new Result<IEnumerable<TeamMember>>(teamMembers);
        }

        public async Task<Result<IEnumerable<TeamMember>>> GetBySchoolIdAsync(int schoolId)
        {
            var teamMembers = await _context.TeamMembers!
                .Where(x => x.SchoolId == schoolId)
                .Include(x => x.Sport)
                .Include(x => x.School)
                .ToListAsync();

            return new Result<IEnumerable<TeamMember>>(teamMembers);
        }

        public async Task<Result<IEnumerable<TeamMember>>> GetBySportIdAsync(int sportId)
        {
            var teamMembers = await _context.TeamMembers!
                .Where(x => x.SportId == sportId)
                .Include(x => x.Sport)
                .Include(x => x.School)
                .ToListAsync();

            return new Result<IEnumerable<TeamMember>>(teamMembers);
        }
    }
}