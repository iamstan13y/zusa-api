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

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _unitOfWork.TeamMember.GetByIdAsync(id);
            if (!result.Success) return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.TeamMember.DeleteAsync(id);
            if (!result.Success) return NotFound(result);

            return Ok(result);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> AddBulkAsync([FromForm] TeamMembersRequest request)
        {
            var result = await _teamMemberRepository.AddBulkAsync(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateMemberRequest request)
        {
            var result = await _unitOfWork.TeamMember.UpdateAsync(new TeamMember
            {
                Id = request.Id,
                DOB = request.DOB,
                FirstName = request.FirstName,
                Gender = request.Gender,
                IdNumber = request.IdNumber,
                LastName = request.LastName,
                RegNumber = request.RegNumber,
                SubscriptionId = request.SubscriptionId
            });

            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("school/{schoolId}")]
        public async Task<IActionResult> GetBySchool(int schoolId) => Ok(await _teamMemberRepository.GetBySchoolIdAsync(schoolId));

        [HttpGet("school/{schoolId}/excel")]
        public async Task<IActionResult> GetExcelBySchool(int schoolId) => Ok(await _teamMemberRepository.GetExcelBySchoolIdAsync(schoolId));

        [HttpGet("sport/{sportId}")]
        public async Task<IActionResult> GetBySport(int sportId) => Ok(await _teamMemberRepository.GetBySportIdAsync(sportId));

        [HttpGet("sport/{sportId}/excel")]
        public async Task<IActionResult> GetExcelBySport(int sportId) => Ok(await _teamMemberRepository.GetExcelBySportIdAsync(sportId));

        [HttpGet("{schoolId}/{sportId}")]
        public async Task<IActionResult> GetBySchoolAndSport(int schoolId, int sportId) => Ok(await _teamMemberRepository.GetBySchoolAndSportIdAsync(schoolId, sportId));

        [HttpGet("{schoolId}/{sportId}/excel")]
        public async Task<IActionResult> GetExcelBySchoolAndSport(int schoolId, int sportId) => Ok(await _teamMemberRepository.GetExcelBySchoolAndSportIdAsync(schoolId, sportId));

        [HttpGet("subscription/{subscriptionId}")]
        public async Task<IActionResult> GetBySubscription(int subscriptionId) => Ok(await _teamMemberRepository.GetBySubscriptionIdAsync(subscriptionId));

        [HttpGet("subscription/{subscriptionId}/excel")]
        public async Task<IActionResult> GetExcelBySubscription(int subscriptionId) => Ok(await _teamMemberRepository.GetExcelBySubscriptionIdAsync(subscriptionId));
    }
}