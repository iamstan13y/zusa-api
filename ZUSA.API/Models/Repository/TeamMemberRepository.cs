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
                member.SubscriptionId = request.SubscriptionId;
            });

            await _context.TeamMembers!.AddRangeAsync(teamMembers);
            await _context.SaveChangesAsync();

            return new Result<string>("Team members added successfully");
        }

        public async Task<Result<IEnumerable<TeamMember>>> GetBySchoolAndSportIdAsync(int schoolId, int sportId)
        {
            var teamMembers = await _context.TeamMembers!
               .Where(x => x.Subscription!.SchoolId == schoolId && x.Subscription.SportId == sportId)
               .Include(x => x.Subscription)
               .ThenInclude(y => y!.School)
               .Include(z => z.Subscription!.School)
               .ToListAsync();

            return new Result<IEnumerable<TeamMember>>(teamMembers);
        }

        public async Task<Result<IEnumerable<TeamMember>>> GetBySchoolIdAsync(int schoolId)
        {
            var teamMembers = await _context.TeamMembers!
                .Where(x => x.Subscription!.SchoolId == schoolId)
                .Include(x => x.Subscription)
                .ThenInclude(x => x!.School)
                .Include(x => x.Subscription!.Sport)
                .ToListAsync();

            return new Result<IEnumerable<TeamMember>>(teamMembers);
        }

        public async Task<Result<IEnumerable<TeamMember>>> GetBySportIdAsync(int sportId)
        {
            var teamMembers = await _context.TeamMembers!
                .Include(x => x.Subscription)
                .ThenInclude(x => x!.School)
                .Include(x => x.Subscription!.Sport)
                .ToListAsync();

            return new Result<IEnumerable<TeamMember>>(teamMembers);
        }

        public async Task<Result<IEnumerable<TeamMember>>> GetBySubscriptionIdAsync(int subscriptionId)
        {
            var teamMembers = await _context.TeamMembers!
                .Where(x => x.SubscriptionId == subscriptionId)
                .Include(x => x.Subscription)
                .ThenInclude(x => x!.School)
                .Include(x => x.Subscription!.Sport)
                .ToListAsync();

            return new Result<IEnumerable<TeamMember>>(teamMembers);
        }
    }
}