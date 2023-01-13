using Microsoft.AspNetCore.Mvc;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;

namespace ZUSA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TeamMemberController(ITeamMemberRepository teamMemberRepository, IUnitOfWork unitOfWork)
        {
            _teamMemberRepository = teamMemberRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post(TeamMemberRequest request)
        {
            var result = await _unitOfWork.TeamMember.AddAsync(new TeamMember
            {
                DOB = request.DOB,
                FirstName = request.FirstName,
                Gender = request.Gender,
                IdNumber = request.IdNumber,
                LastName = request.LastName,
                RegNumber = request.RegNumber,
                SubscriptionId = request.SubscriptionId
            });

            _unitOfWork.SaveChanges();

            if (!result.Success) return BadRequest();

            return Ok(result);
        }

        [HttpPost("bulk")]
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