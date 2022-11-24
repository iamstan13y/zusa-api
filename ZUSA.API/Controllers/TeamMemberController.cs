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
    }
}