using Microsoft.AspNetCore.Mvc;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;

namespace ZUSA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SchoolController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _unitOfWork.School.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var school = await _unitOfWork.School.FindAsync(id);
            if (school == null) return NotFound(school);
            
            return Ok(school);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SchoolRequest request)
        {
            var result = await _unitOfWork.School.AddAsync(new School
            {
                Name = request.Name
            });

            if (!result.Success) return BadRequest(result);
            
            _unitOfWork.SaveChanges();

            return Ok(result);
        }
    }
}