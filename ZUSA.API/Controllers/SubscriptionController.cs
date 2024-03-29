﻿using Microsoft.AspNetCore.Mvc;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
using ZUSA.API.Utility;

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

        [HttpGet("paged")]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination) => Ok(await _unitOfWork.Subscription.GetAllPagedAsync(pagination));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var subscription = await _unitOfWork.Subscription.FindAsync(id);
            if (subscription == null) return NotFound();

            return Ok(subscription);
        }

        [HttpGet("sport/{sportId}")]
        public async Task<IActionResult> GetBySport(int sportId) => Ok(await _unitOfWork.Subscription.GetBySportIdAsync(sportId));

        [HttpGet("sport/{sportId}/paged")]
        public async Task<IActionResult> GetBySportPaged(int sportId, [FromQuery] Pagination pagination) => Ok(await _unitOfWork.Subscription.GetPagedBySportIdAsync(sportId, pagination));

        [HttpGet("school/{schoolId}")]
        public async Task<IActionResult> GetBySchool(int schoolId) => Ok(await _unitOfWork.Subscription.GetBySchoolIdAsync(schoolId));

        [HttpGet("school/{schoolId}/paged")]
        public async Task<IActionResult> GetBySchoolPaged(int schoolId, [FromQuery] Pagination pagination) => Ok(await _unitOfWork.Subscription.GetPagedBySchoolIdAsync(schoolId, pagination));
        
        [HttpPost]
        public async Task<IActionResult> Post(SubscriptionRequest request)
        {
            var result = await _unitOfWork.Subscription.AddAsync(new Subscription
            {
                SportId = request.SportId,
                SchoolId = request.SchoolId,
                Gender = request.Gender
            });

            _unitOfWork.SaveChanges();

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{subscriptionId}/toggle")]
        public async Task<IActionResult> Put(int subscriptionId)
        {
            var result = await _unitOfWork.Subscription.ToggleStatusAsync(subscriptionId);
            if (!result.Success) return BadRequest(result);

            _unitOfWork.SaveChanges();

            return Ok(result);
        }
    }
}