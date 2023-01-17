using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZUSA.API.Mappers;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
using ZUSA.API.Services;

namespace ZUSA.API.Models.Repository
{
    public class TeamMemberRepository : Repository<TeamMember>, ITeamMemberRepository
    {
        private readonly AppDbContext _context;
        private readonly IExcelService _excelService;

        public TeamMemberRepository(AppDbContext context, IExcelService excelService) : base(context)
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

        public async new Task<Result<TeamMember>> AddAsync(TeamMember teamMember)
        {
            var teamMembers = await _context.Subscriptions!
                .Where(x => x.Id == teamMember.SubscriptionId)
                .Include(x => x.Sport)
                .ToListAsync();

            if (teamMembers.Any())
                if (teamMembers.Count >= teamMembers[0].Sport!.TeamMemberLimit)
                    return new Result<TeamMember>(false, "You've reached the limit for maximum team members.");

            _dbSet.Add(teamMember);
            await _context.SaveChangesAsync();

            return new Result<TeamMember>(teamMember);
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

        public async Task<Result<TeamMember>> GetByIdAsync(int id)
        {
            var subscription = await _dbSet
                .Where(x => x.Id == id)
                .Include(x => x.Subscription)
                .Include(x => x.Subscription!.School)
                .Include(x => x.Subscription!.Sport)
                .FirstOrDefaultAsync();

            if (subscription == null) return new Result<TeamMember>(false, "Team member not found.");

            return new Result<TeamMember>(subscription);
        }

        public async Task<Result<string>> GetExcelBySubscriptionIdAsync(int subscriptionId)
        {
            var teamMembers = await _dbSet
               .Where(x => x.SubscriptionId == subscriptionId)
               .Include(x => x.Subscription)
               .Include(x => x.Subscription!.School)
               .Include(x => x.Subscription!.Sport)
               .ToListAsync();

            if (!teamMembers.Any()) return new Result<string>(false, "No team members found.");

            var excelFile = await _excelService.GenerateExcelAsync(teamMembers.ToExcelRequest());

            return new Result<string>(excelFile);
        }

        public async Task<Result<string>> GetExcelBySchoolIdAsync(int schoolId)
        {
            var teamMembers = await _dbSet
               .Where(x => x.Subscription!.SchoolId == schoolId)
               .Include(x => x.Subscription)
               .Include(x => x.Subscription!.School)
               .Include(x => x.Subscription!.Sport)
               .ToListAsync();

            if (!teamMembers.Any()) return new Result<string>(false, "No team members found.");

            var excelFile = await _excelService.GenerateExcelAsync(teamMembers.ToExcelRequest());

            return new Result<string>(excelFile);
        }

        public async Task<Result<string>> GetExcelBySportIdAsync(int sportId)
        {
            var teamMembers = await _dbSet
               .Where(x => x.Subscription!.SportId == sportId)
               .Include(x => x.Subscription)
               .Include(x => x.Subscription!.School)
               .Include(x => x.Subscription!.Sport)
               .ToListAsync();

            if (!teamMembers.Any()) return new Result<string>(false, "No team members found.");

            var excelFile = await _excelService.GenerateExcelAsync(teamMembers.ToExcelRequest());

            return new Result<string>(excelFile);
        }

        public Task<Result<string>> GetExcelBySchoolAndSportIdAsync(int schoolId, int sportId)
        {
            throw new NotImplementedException();
        }
    }
}