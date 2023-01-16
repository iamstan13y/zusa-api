using Microsoft.AspNetCore.Mvc;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
using ZUSA.API.Utility;

namespace ZUSA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _unitOfWork.Sport.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _unitOfWork.Sport.FindAsync(id);
            if (!result.Success) return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SportRequest request)
        {
            var result = await _unitOfWork.Sport.AddAsync(new Sport
            {
                Name = request.Name,
                TeamMemberLimit = request.TeamMemberLimit,
                Deadline = request.Deadline
            });

            if (!result.Success) return BadRequest(result);

            _unitOfWork.SaveChanges();

            return Ok(result);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination) => Ok(await _unitOfWork.Sport.GetAllPagedAsync(pagination));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Sport.DeleteAsync(id);
            if (!result.Success) return NotFound(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateSportRequest request)
        {
            var result = await _unitOfWork.Sport.UpdateAsync(new Sport
            {
                Id = request.Id,
                Name = request.Name,
                TeamMemberLimit = request.TeamMemberLimit,
                Deadline = request.Deadline
            });

            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }
    }
}