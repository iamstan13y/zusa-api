using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get() => Ok( await _unitOfWork.School.GetAllAsync());
         
    }
}