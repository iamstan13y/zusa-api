using Microsoft.AspNetCore.Mvc;
using ZUSA.API.Models.Repository.IRepository;

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
    }
}