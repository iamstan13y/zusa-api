using Microsoft.AspNetCore.Mvc;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;

namespace ZUSA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberRepository _teamMemberRepository;

        public TeamMemberController(ITeamMemberRepository teamMemberRepository)
        {
            _teamMemberRepository = teamMemberRepository;
        }

        [HttpPost("add-bulk")]
        public async Task<IActionResult> AddBulkAsync([FromForm] TeamMembersRequest request)
        {
            var result = await _teamMemberRepository.AddBulkAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("school/{schoolId}")]
        public async Task<IActionResult> GetBySchool(int schoolId) => Ok(await _teamMemberRepository.GetBySchoolIdAsync(schoolId));

        [HttpGet("sport/{sportId}")]
        public async Task<IActionResult> GetBySport(int sportId) => Ok(await _teamMemberRepository.GetBySportIdAsync(sportId));

        [HttpGet("{schoolId}/{sportId}")]
        public async Task<IActionResult> GetBySchoolAndSport(int schoolId, int sportId) => Ok(await _teamMemberRepository.GetBySchoolAndSportIdAsync(schoolId, sportId));

        [HttpGet("subscription/{subscriptionId}")]
        public async Task<IActionResult> GetBySubscription(int subscriptionId) => Ok(await _teamMemberRepository.GetBySubscriptionIdAsync(subscriptionId));
    }
}