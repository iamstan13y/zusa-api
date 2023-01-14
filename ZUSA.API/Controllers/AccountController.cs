using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZUSA.API.Enums;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
using ZUSA.API.Utility;

namespace ZUSA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository) => _accountRepository = accountRepository;

        [HttpPost("create-account")]
        [ProducesResponseType(typeof(Result<Account>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest request)
        {
            var result = await _accountRepository.AddAsync(new Account
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                Email = request.Email,
                Role = Role.USER,
                PhoneNumber = request.PhoneNumber,
                SchoolId = request.SchoolId,
                DateCreated = DateTime.Now
            });

            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll() => Ok(await _accountRepository.GetAllAsync());

        [HttpGet("paged")]
        public async Task<IActionResult> GetAllPaged([FromQuery] Pagination pagination) => Ok(await _accountRepository.GetAllPagedAsync(pagination));

        //[HttpGet("sign-up/resend-otp/{email}")]
        //[ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        //public async Task<IActionResult> ResendOtp(string email)
        //{
        //    var result = await _accountRepository.ResendOtpAsync(email);
        //    if (!result.Success) return NotFound(result);

        //    return Ok(result);
        //}

        [HttpPost("login")]
        [ProducesResponseType(typeof(Result<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<Account>), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _accountRepository.LoginAsync(request);

            if (!result.Success)
                return StatusCode(StatusCodes.Status403Forbidden, result);

            return Ok(result);
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> ConfirmAccount(VerifyOtpRequest request)
        {
            var result = await _accountRepository.VerifyOtpAsync(request);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("change-password")]
        [Authorize]
        [ProducesResponseType(typeof(Result<Account>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<Account>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var result = await _accountRepository.ChangePasswordAsync(request);
            if (!result.Success) return BadRequest(result);

            return Ok(result);
        }

        //[HttpGet("reset-password/verification-code/{email}")]
        //[ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Result<string>), StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(typeof(Result<string>), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> ResetPassword(string email)
        //{
        //    var result = await _accountRepository.GetResetPasswordCodeAsync(email);
        //    if (!result.Success) return BadRequest(result);

        //    return Ok(result);
        //}

        //[HttpPost("reset-password")]
        //[ProducesResponseType(typeof(Result<Account>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(Result<Account>), StatusCodes.Status403Forbidden)]
        //public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        //{
        //    var result = await _accountRepository.ResetPasswordAsync(request);
        //    if (!result.Success) return BadRequest(result);

        //    return Ok(result);
        //}
    }
}