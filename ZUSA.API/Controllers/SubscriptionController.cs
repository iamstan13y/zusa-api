using Microsoft.AspNetCore.Mvc;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Repository.IRepository;

namespace ZUSA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _unitOfWork.Subscription.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var subscription = await _unitOfWork.Subscription.FindAsync(id);
            if (subscription == null) return NotFound();
            
            return Ok(subscription);
        }

        [HttpGet("sport/{sportId}")]
        public async Task<IActionResult> GetBySport(int sportId) => Ok(await _unitOfWork.Subscription.GetBySportIdAsync(sportId));

        [HttpGet("school/{schoolId}")]
        public async Task<IActionResult> GetBySchool(int schoolId) => Ok(await _unitOfWork.Subscription.GetBySchoolIdAsync(schoolId));
    }
}